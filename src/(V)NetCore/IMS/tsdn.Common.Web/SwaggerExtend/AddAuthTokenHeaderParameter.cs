/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authorization;

namespace tsdn.Common.SwaggerExtend
{
    /// <summary>
    /// Swagger添加Token认证头
    /// </summary>
    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }
            var anonymousType = typeof(AllowAnonymousAttribute);
            if (context.ApiDescription.ControllerAttributes().FirstOrDefault(e => e.GetType() == anonymousType) == null
                && context.ApiDescription.ActionAttributes().FirstOrDefault(e => e.GetType() == anonymousType) == null)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = true,
                    Description= "认证Token(Basic +Token)"
                });
            }
        }
    }
}
