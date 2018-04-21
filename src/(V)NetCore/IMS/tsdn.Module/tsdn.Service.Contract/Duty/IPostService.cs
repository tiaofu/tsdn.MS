using System.Collections.Generic;
using tsdn.Common;
using tsdn.PIS.Entities;
using tsdn.Dependency;

namespace tsdn.Service.Contract
{
    /// <summary>
    /// 岗位信息
    /// </summary>
    public interface IPostService : ITransientDependency
    {
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        ApiResult<List<DutyPostViewModel>> PostQuery(QueryCondition condition);
        /// <summary>
        /// 单个岗位信息查询
        /// </summary>
        /// <returns></returns>
        ApiResult<DutyPost> QueryById(string PostId);
        /// <summary>
        /// 根据主键删除岗位
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        ApiResult<bool> Delete(string PostId, UserInfo userInfo);
        /// <summary>
        /// 保存岗位信息
        /// </summary>
        /// <param name="DutyPost"></param>
        /// <returns></returns>
        ApiResult<bool> Save(DutyPost DutyPost, UserInfo userInfo);
    }
}
