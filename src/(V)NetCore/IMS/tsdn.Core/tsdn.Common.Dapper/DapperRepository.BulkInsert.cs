using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace tsdn.Common.Dapper
{
    /// <summary>
    ///     Base Repository
    /// </summary>
    public partial class DapperRepository<TEntity>
        where TEntity : class
    {
        /// <inheritdoc />
        public virtual int BulkInsert(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            if(instances==null || instances.Count() == 0)
            {
                return 0;
            }
            var queryResult = SqlGenerator.GetBulkInsert(instances);
            var count = Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction);
            return count;
        }

        /// <inheritdoc />
        public virtual async Task<int> BulkInsertAsync(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            if (instances == null || instances.Count() == 0)
            {
                return 0;
            }
            var queryResult = SqlGenerator.GetBulkInsert(instances);
            var count = await Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction);
            return count;
        }

        public int AddRange(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            return BulkInsert(instances, transaction);
        }
    }
}
