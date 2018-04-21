/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common;
using tsdn.Common.MVC;
using tsdn.Entity;
using tsdn.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace tsdn.Api
{
    /// <summary>
    /// 特勤接口
    /// </summary>
    public class SpecialServiceController : ApiController
    {
        /// <summary>
        /// 特勤服务
        /// </summary>
        private readonly ISpecialServiceService SpecialService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_SpecialService"></param>
        public SpecialServiceController(ISpecialServiceService _SpecialService)
        {
            SpecialService = _SpecialService;
        }

        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        public ApiResult<List<SpecialSchemeViewModel>> Get([FromBody]QueryCondition condition)
        {
            return SpecialService.PostQuery(condition);
        }
        /// <summary>
        /// 列表查询接口
        /// </summary>
        /// <param name="SSID">特勤方案ID</param>
        /// <returns>岗位信息</returns>  
        [HttpGet]
        public ApiResult<SpecialSchemeViewModel> Get(string SSID)
        {
            return SpecialService.QueryById(SSID);
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(string PostId)
        {
            return SpecialService.Delete(PostId, CurrentUserInfo);
        }
        /// <summary>
        /// 保存接口
        /// </summary>
        /// <param name="SpecialSchemeViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ApiResult<bool> Put([FromBody]SpecialSchemeViewModel SpecialSchemeViewModel)
        {
            return SpecialService.Save(SpecialSchemeViewModel, CurrentUserInfo);
        }
    }
}
