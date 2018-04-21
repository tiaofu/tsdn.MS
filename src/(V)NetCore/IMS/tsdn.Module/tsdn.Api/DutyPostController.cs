/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tsdn.Common;
using tsdn.PIS.Entities;
using tsdn.Common.MVC;

namespace tsdn.Api
{
    /// <summary>
    /// 勤务岗位Api
    /// </summary>

    public class DutyPostController : ApiController
    {
        /// <summary>
        /// 岗位服务
        /// </summary>
        private readonly IPostService DutyPostService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_DutyPostService"></param>
        public DutyPostController(IPostService _DutyPostService)
        {
            DutyPostService = _DutyPostService;
        }

        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        public ApiResult<List<DutyPostViewModel>> Get([FromBody]QueryCondition condition)
        {
            return DutyPostService.PostQuery(condition);
        }
        /// <summary>
        /// 单个岗位信息查询
        /// </summary>
        /// <param name="PostId">岗位ID</param>
        /// <returns>岗位信息</returns>  
        [HttpGet]
        public ApiResult<DutyPost> Get(string PostId)
        {
            return DutyPostService.QueryById(PostId);
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(string PostId)
        {
            return DutyPostService.Delete(PostId, CurrentUserInfo);
        }
        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="DutyPost"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult<bool> Put([FromBody]DutyPost DutyPost)
        {
            return DutyPostService.Save(DutyPost, CurrentUserInfo);
        }
    }
}
