using System.Collections.Generic;
using tsdn.Common;
using tsdn.PIS.Entities;
using tsdn.Dependency;

namespace tsdn.Service.Contract
{
    /// <summary>
    /// 考核记录服务接口
    /// </summary>
    public interface IDutyRecordService : ITransientDependency
    {
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        ApiResult<List<DutyRecord>> PostQuery(QueryCondition condition);
        /// <summary>
        /// 单个备案信息查询
        /// </summary>
        /// <returns></returns>
        ApiResult<DutyRecord> QueryById(string Id);
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ApiResult<bool> Delete(string Id, UserInfo userInfo);
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="DutyRecord"></param>
        /// <returns></returns>
        ApiResult<bool> Save(DutyRecord DutyRecord, UserInfo userInfo);
    }
}
