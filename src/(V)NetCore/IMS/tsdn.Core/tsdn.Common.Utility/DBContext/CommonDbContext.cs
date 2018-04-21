/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Core;
using tsdn.Common.Dapper;
using tsdn.Common.Dapper.DbContext;
using tsdn.Common.Dapper.SqlGenerator;
using Dapper;
using System;
using System.Collections.Concurrent;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;

namespace tsdn.Common.Utility
{
    public class CommonDbContext : DapperDbContext
    {
        private ConcurrentDictionary<Type, object> _dict = new ConcurrentDictionary<Type, object>();

        private static IDbConnection CreateConnection(string connectionString)
        {
            OracleConnection con = new OracleConnection(connectionString);
            return con;
        }

        public CommonDbContext() : base(CreateConnection(SysConfig.DefaultConnection))
        {
        }

        public CommonDbContext(IDbConnection connection) : base(connection)
        {
        }

        public virtual IDapperRepository<TEntity> Set<TEntity>() where TEntity : class
        {
            IDapperRepository<TEntity> repository = null;
            object obj = null;
            Type type = typeof(TEntity);
            if (!_dict.TryGetValue(typeof(TEntity), out obj))
            {
                repository = new DapperRepository<TEntity>(InnerConnection, Dapper.SqlGenerator.SqlProvider.Oracle);
                _dict.TryAdd(type, repository);
            }
            else
            {
                repository = obj as IDapperRepository<TEntity>;
            }
            return repository;
        }        

        /// <summary>
        /// 执行查询SQL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数 {Id=1}</param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public PaginationResult<TEntity> PageQuery<TEntity>(QueryCondition condition, IDbTransaction dbTransaction = null) where TEntity : class
        {
            try
            {
                var SqlGenerator = Set<TEntity>().SqlGenerator;
                var queryResult = SqlGenerator.GetSelectAll(null);
                IQueryable<TEntity> Query = Connection.Query<TEntity>(queryResult.GetSql(), queryResult.Param, null).AsQueryable<TEntity>();
                return Query.PageQuery<TEntity>(condition);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual int SaveChanges()
        {
            return 0;
        }

        /// <summary>
        ///     Close DB connection
        /// </summary>
        public override void Dispose()
        {
            if (InnerConnection != null && InnerConnection.State != ConnectionState.Closed)
            {
                InnerConnection.Close();
            }
            if (_dict != null)
            {
                _dict = null;
            }
        }
    }
}
