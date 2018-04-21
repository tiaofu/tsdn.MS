/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Config;
using tsdn.Common.MVC;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace tsdn.Common.Web.Controllers
{
    /// <summary>
    ///  系统参数配置
    /// </summary>
    [Route("api/Common/[controller]")]
    [SwaggerHidden]
    public class ConfigOptionsController : ApiController
    {
        /// <summary>
        /// 参数配置服务
        /// </summary>
        private readonly IConfigManager manager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_manager"></param>
        public ConfigOptionsController(IConfigManager _manager)
        {
            manager = _manager;
        }


        /// <summary>
        /// 获取所有配置信息
        /// </summary>
        /// <returns>所有配置信息</returns>
        [HttpGet]
        [Route("[action]")]
        public ApiResult<List<OptionViewModel>> GetAllOption()
        {
            ApiResult<List<OptionViewModel>> result = new ApiResult<List<OptionViewModel>>();
            List<OptionViewModel> list = manager.GetAllOption();
            result.Result = list;
            return result;
        }

        /// <summary>
        /// 获取指定项配置信息
        /// </summary>
        /// <param name="GroupType">分组项</param>
        /// <returns>所有配置信息</returns>
        [HttpGet]
        [Route("[action]")]
        public ApiResult<OptionViewModel> GetOptionByGroup(string GroupType)
        {
            ApiResult<OptionViewModel> result = new ApiResult<OptionViewModel>();
            var model = manager.GetOptionByGroup(GroupType);
            result.Result = model;
            return result;
        }

        /// <summary>
        /// 获取指定项配置信息
        /// </summary>
        /// <param name="GroupType">分组项</param>
        /// <param name="key">指定项</param>
        /// <returns>所有配置信息</returns>
        [HttpGet]
        [Route("[action]")]
        public ApiResult<Options> GetOptionByGroupAndKey(string GroupType, string key)
        {
            ApiResult<Options> result = new ApiResult<Options>();
            result.Result = manager.GetOptionByGroupAndKey(GroupType, key);
            return result;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="value">配置信息</param>
        [HttpPost]
        public ApiResult<string> Post([FromBody]OptionViewModel value)
        {
            return manager.Save(value);
        }
    }
}
