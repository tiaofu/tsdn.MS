using System.Collections.Generic;
using tsdn.Common;
using tsdn.PIS.Entities;
using tsdn.Dependency;

namespace tsdn.Service.Contract
{
    /// <summary>
    /// 警力备案服务
    /// </summary>
    public interface IPoliceRecordService : ITransientDependency
    {
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        ApiResult<List<PoliceRecord>> PostQuery(QueryCondition condition);
        /// <summary>
        /// 单个备案信息查询
        /// </summary>
        /// <returns></returns>
        ApiResult<PoliceRecord> QueryById(string PoliceRecordId);
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="PoliceRecordId"></param>
        /// <returns></returns>
        ApiResult<bool> Delete(string PoliceRecordId, UserInfo userInfo);
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="PoliceRecord"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        ApiResult<bool> Save(PoliceRecord PoliceRecord, UserInfo userInfo);
        /// <summary>
        /// 查询最新的备案信息
        /// </summary>
        /// <returns></returns>
        ApiResult<List<PostRecordViewModel>> QueryLastTimeRecord();
    }
}
