using System.Collections.Generic;
using tsdn.Common;
using tsdn.PIS.Entities;
using tsdn.Dependency;

namespace tsdn.Service.Contract
{
    /// <summary>
    /// 排班信息接口
    /// </summary>
    public interface IPostPoliceOnDutyService : ITransientDependency
    {
        /// <summary>
        /// 根据岗位查询排班
        /// </summary>
        /// <returns></returns>
        ApiResult<List<PostPoliceOnduty>> QueryByPost(QueryCondition condition);
        /// <summary>
        /// 单个排班信息查询
        /// </summary>
        /// <returns></returns>
        ApiResult<PostPoliceOnduty> QueryById(string PPNId);
        /// <summary>
        /// 根据主键删除排班
        /// </summary>
        /// <param name="PPNId"></param>
        /// <returns></returns>
        ApiResult<bool> Delete(string PPNId, UserInfo userInfo);
        /// <summary>
        /// 保存排班信息
        /// </summary>
        /// <param name="DutyPost"></param>
        /// <returns></returns>
        ApiResult<bool> Save(PostPoliceOnduty DutyPost, UserInfo userInfo);
        /// <summary>
        /// 查询警员排班信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ApiResult<List<PoliceMan>> QueryPoliceMan(QueryCondition condition);
    }
}
