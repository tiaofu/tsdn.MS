using System.Data;
using Dapper;
using static Dapper.SqlMapper;
using System.Threading.Tasks;

namespace tsdn.Common.Dapper.DbContext
{
    /// <summary>
    ///     Class is helper for use and close IDbConnection
    /// </summary>
    public class DapperDbContext : IDapperDbContext
    {
        /// <summary>
        ///     DB Connection for internal use
        /// </summary>
        protected readonly IDbConnection InnerConnection;

        /// <summary>
        ///     Constructor
        /// </summary>
        protected DapperDbContext(IDbConnection connection)
        {
            InnerConnection = connection;
        }

        /// <summary>
        ///     Get opened DB Connection
        /// </summary>
        public virtual IDbConnection Connection
        {
            get
            {
                OpenConnection();
                return InnerConnection;
            }
        }

        /// <summary>
        ///     Open DB connection
        /// </summary>
        public void OpenConnection()
        {
            if (InnerConnection.State != ConnectionState.Open && InnerConnection.State != ConnectionState.Connecting)
                InnerConnection.Open();
        }

        /// <summary>
        ///     Open DB connection and Begin transaction
        /// </summary>
        public virtual IDbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction();
        }

        /// <summary>
        /// 执行查询SQL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数 {Id=1}</param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public GridReader SqlQuery(string sql, object param = null, IDbTransaction dbTransaction = null)
        {
            return InnerConnection.QueryMultiple(sql, param, dbTransaction);
        }

        /// <summary>
        /// 执行查询SQL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数 {Id=1}</param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public Task<GridReader> SqlQueryAsync(string sql, object param = null, IDbTransaction dbTransaction = null)
        {
            return InnerConnection.QueryMultipleAsync(sql, param, dbTransaction);
        }

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数 {Id=1}</param>
        /// <param name="dbTransaction">事物</param>
        /// <returns>影响条数</returns>
        public int ExecuteSqlCommand(string sql, object param = null, IDbTransaction dbTransaction = null)
        {
            return InnerConnection.Execute(sql, param, dbTransaction);
        }

        /// <summary>
        /// 异步执行非查询SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数 {Id=1}</param>
        /// <param name="dbTransaction">事物</param>
        /// <returns>影响条数</returns>
        public Task<int> ExecuteSqlCommandAsync(string sql, object param = null, IDbTransaction dbTransaction = null)
        {
            return InnerConnection.ExecuteAsync(sql, param, dbTransaction);
        }

        /// <summary>
        ///     Close DB connection
        /// </summary>
        public virtual void Dispose()
        {
            if (InnerConnection != null && InnerConnection.State != ConnectionState.Closed)
                InnerConnection.Close();
        }
    }
}