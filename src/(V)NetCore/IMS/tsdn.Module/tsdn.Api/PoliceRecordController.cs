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
    /// 警力报备API
    /// </summary>

    public class PoliceRecordController : ApiController
    {
        /// <summary>
        /// 警力备案服务
        /// </summary>
        private readonly IPoliceRecordService PoliceRecordService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_PoliceRecordService"></param>
        public PoliceRecordController(IPoliceRecordService _PoliceRecordService)
        {
            PoliceRecordService = _PoliceRecordService;
        }

        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        public ApiResult<List<PoliceRecord>> Get([FromBody]QueryCondition condition)
        {
            return PoliceRecordService.PostQuery(condition);
        }
        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <param name="Id">岗位ID</param>
        /// <returns>岗位信息</returns>  
        [HttpGet]
        public ApiResult<PoliceRecord> Get(string Id)
        {
            return PoliceRecordService.QueryById(Id);
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(string Id)
        {
            return PoliceRecordService.Delete(Id, CurrentUserInfo);
        }
        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="PoliceRecord"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult<bool> Put([FromBody]PoliceRecord PoliceRecord)
        {
            return PoliceRecordService.Save(PoliceRecord, CurrentUserInfo);
        }
        /// <summary>
        /// 获取最新报备信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public ApiResult<List<PostRecordViewModel>> QueryPostPolice()
        {
            return PoliceRecordService.QueryLastTimeRecord();
        }
    }
}
