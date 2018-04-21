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
    /// 考核备案API
    /// </summary>

    public class DutyRecordController : ApiController
    {
        /// <summary>
        /// 考核记录服务
        /// </summary>
        private readonly IDutyRecordService DutyRecordService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_DutyRecordService"></param>
        public DutyRecordController(IDutyRecordService _DutyRecordService)
        {
            DutyRecordService = _DutyRecordService;
        }

        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        public ApiResult<List<DutyRecord>> Get([FromBody]QueryCondition condition)
        {
            return DutyRecordService.PostQuery(condition);
        }
        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <param name="Id">岗位ID</param>
        /// <returns>岗位信息</returns>  
        [HttpGet]
        public ApiResult<DutyRecord> Get(string Id)
        {
            return DutyRecordService.QueryById(Id);
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(string Id)
        {
            return DutyRecordService.Delete(Id, CurrentUserInfo);
        }
        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="DutyRecord"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult<bool> Put([FromBody]DutyRecord DutyRecord)
        {
            return DutyRecordService.Save(DutyRecord, CurrentUserInfo);
        }
    }
}
