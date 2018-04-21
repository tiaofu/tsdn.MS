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
    /// 排班信息api
    /// </summary>

    public class PostPoliceOnDutyController : ApiController
    {
        /// <summary>
        /// 排班服务
        /// </summary>
        private readonly IPostPoliceOnDutyService PostPoliceOnDutyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_PostPoliceOnDutyService"></param>
        public PostPoliceOnDutyController(IPostPoliceOnDutyService _PostPoliceOnDutyService)
        {
            PostPoliceOnDutyService = _PostPoliceOnDutyService;
        }

        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        public ApiResult<List<PostPoliceOnduty>> Get([FromBody]QueryCondition condition)
        {
            return PostPoliceOnDutyService.QueryByPost(condition);
        }
        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <param name="PPNId">排班Id</param>
        /// <returns>岗位信息</returns>  
        [HttpGet]
        public ApiResult<PostPoliceOnduty> Get(string PPNId)
        {
            return PostPoliceOnDutyService.QueryById(PPNId);
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="PPNId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(string PPNId)
        {
            return PostPoliceOnDutyService.Delete(PPNId, CurrentUserInfo);
        }
        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="PostPoliceOnduty"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult<bool> Put([FromBody]PostPoliceOnduty PostPoliceOnduty)
        {
            return PostPoliceOnDutyService.Save(PostPoliceOnduty, CurrentUserInfo);
        }
        /// <summary>
        /// 查询警员排班信息
        /// </summary>
        /// <param name="PostPoliceOnduty"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public ApiResult<List<PoliceMan>> QueryPoliceOnDuty([FromBody]QueryCondition condition)
        {
            return PostPoliceOnDutyService.QueryPoliceMan(condition);
        }
    }
}
