using System.Collections.Generic;
using tsdn.Common;
using tsdn.Dependency;
using tsdn.Entity;

namespace tsdn.Service.Contract
{
    /// <summary>
    /// 特勤任务接口
    /// </summary>
    public interface ISpecialServiceService : ITransientDependency
    {
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        ApiResult<List<SpecialSchemeViewModel>> PostQuery(QueryCondition condition);
        /// <summary>
        /// 单个信息查询
        /// </summary>
        /// <returns></returns>
        ApiResult<SpecialSchemeViewModel> QueryById(string Id);
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ApiResult<bool> Delete(string Id, UserInfo userInfo);
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="SpecialScheme"></param>
        /// <returns></returns>
        ApiResult<bool> Save(SpecialSchemeViewModel SpecialScheme, UserInfo userInfo);
    }
}
