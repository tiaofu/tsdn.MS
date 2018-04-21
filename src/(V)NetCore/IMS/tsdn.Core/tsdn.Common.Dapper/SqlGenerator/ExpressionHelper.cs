﻿using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using tsdn.Common.Dapper.Extensions;

[assembly: InternalsVisibleTo("tsdn.Common.Dapper.Tests")]

namespace tsdn.Common.Dapper.SqlGenerator
{

    internal static class ExpressionHelper
    {
        public static string GetPropertyName<TSource, TField>(Expression<Func<TSource, TField>> field)
        {
            if (Equals(field, null))
                throw new ArgumentNullException(nameof(field), "field can't be null");

            MemberExpression expr;

            switch (field.Body)
            {
                case MemberExpression body:
                    expr = body;
                    break;
                case UnaryExpression expression:
                    expr = (MemberExpression)expression.Operand;
                    break;
                default:
                    throw new ArgumentException("Expression field isn't supported", nameof(field));
            }

            return expr.Member.Name;
        }

        public static object GetValue(Expression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }

        public static string GetSqlOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                case ExpressionType.Not:
                case ExpressionType.MemberAccess:
                    return "=";

                case ExpressionType.NotEqual:
                    return "!=";

                case ExpressionType.LessThan:
                    return "<";

                case ExpressionType.LessThanOrEqual:
                    return "<=";

                case ExpressionType.GreaterThan:
                    return ">";

                case ExpressionType.GreaterThanOrEqual:
                    return ">=";

                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    return "AND";

                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return "OR";

                case ExpressionType.Default:
                    return string.Empty;

                default:
                    throw new NotSupportedException(type + " isn't supported");
            }
        }

        public static string GetMethodCallSqlOperator(string methodName)
        {
            switch (methodName)
            {
                case "Contains":
                    return "IN";

                case "Any":
                case "All":
                    return methodName.ToUpperInvariant();

                default:
                    throw new NotSupportedException(methodName + " isn't supported");
            }
        }

        public static BinaryExpression GetBinaryExpression(Expression expression)
        {
            var binaryExpression = expression as BinaryExpression;
            var body = binaryExpression ?? Expression.MakeBinary(ExpressionType.Equal, expression, expression.NodeType == ExpressionType.Not ? Expression.Constant(false) : Expression.Constant(true));
            return body;
        }

        public static Func<PropertyInfo, bool> GetPrimitivePropertiesPredicate()
        {
            return p => p.CanWrite && (p.PropertyType.IsValueType() || p.PropertyType == typeof(string) || p.PropertyType == typeof(byte[]));
        }

        public static object GetValuesFromCollection(MethodCallExpression callExpr)
        {
            var expr = callExpr.Object as MemberExpression;

            if (!(expr?.Expression is ConstantExpression))
                throw new NotSupportedException(callExpr.Method.Name + " isn't supported");

            var constExpr = (ConstantExpression)expr.Expression;

            var constExprType = constExpr.Value.GetType();
            return constExprType.GetField(expr.Member.Name).GetValue(constExpr.Value);
        }


        public static MemberExpression GetMemberExpression(Expression expression)
        {
            switch (expression)
            {
                case MethodCallExpression expr:
                    return (MemberExpression)expr.Arguments[0];
                    
                case MemberExpression memberExpression:
                    return memberExpression;
                    
                case UnaryExpression unaryExpression:
                    return (MemberExpression)unaryExpression.Operand;
                    
                case BinaryExpression binaryExpression:
                    var binaryExpr = binaryExpression;

                    if (binaryExpr.Left is UnaryExpression left)
                        return (MemberExpression)left.Operand;

                    //should we take care if right operation is memberaccess, not left?
                    return (MemberExpression)binaryExpr.Left;
                    
                case LambdaExpression expression1:
                    var lambdaExpression = expression1;

                    switch (lambdaExpression.Body)
                    {
                        case MemberExpression body:
                            return body;
                        case UnaryExpression expressionBody:
                            return (MemberExpression)expressionBody.Operand;
                    }
                    break;
            }

            return null;
        }

        /// <summary>
        ///     Gets the name of the property.
        /// </summary>
        /// <param name="expr">The Expression.</param>
        /// <param name="nested">Out. Is nested property.</param>
        /// <returns>The property name for the property expression.</returns>
        public static string GetPropertyNamePath(Expression expr, out bool nested)
        {
            var path = new StringBuilder();
            var memberExpression = GetMemberExpression(expr);
            var count = 0;
            do
            {
                count++;
                if (path.Length > 0)
                    path.Insert(0, "");
                path.Insert(0, memberExpression.Member.Name);
                memberExpression = GetMemberExpression(memberExpression.Expression);
            } while (memberExpression != null);

            if (count > 2)
                throw new ArgumentException("Only one degree of nesting is supported");

            nested = count == 2;

            return path.ToString();
        }
    }
}