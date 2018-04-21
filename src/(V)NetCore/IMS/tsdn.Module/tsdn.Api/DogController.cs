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
using tsdn.Common.Utility;
using tsdn.Entity;
using tsdn.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace tsdn.Api
{

    /// <summary>
    /// 测试接口 Dog服务
    /// </summary>
    [SwaggerHidden]
    [AllowAnonymous]
    public class DogController : ApiController
    {
        private readonly ILogger<DogController> logger;

        private readonly IEnumerable<IDogService> dogServiceList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="_dogServiceList"></param>
        public DogController(ILogger<DogController> _logger, IEnumerable<IDogService> _dogServiceList)
        {
            logger = _logger;
            dogServiceList = _dogServiceList;
        }

        /// <summary>
        /// 获取所有的狗品种
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> Get()
        {
            logger.LogInformation("DogController Getting item {ID}", "123");
            List<string> list = new List<string>();
            Random dom = new Random();
            foreach (var item in dogServiceList)
            {
                list.Add(item.Eat(Math.Round(dom.NextDouble() * 2, 1)));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取指定Id的路面设备数据
        /// </summary>
        /// <param name="id">路面设备</param>
        /// <returns>设备数据</returns>
        [HttpGet("{id}")]
        public ApiResult<FrontendDevice> Get(string id)
        {
            ApiResult<FrontendDevice> result = new ApiResult<FrontendDevice>();
            using (CommonDbContext db = new CommonDbContext())
            {
                result.Result = db.Set<FrontendDevice>().FindById(id);
                //测试SqlQuery方法
                var list = db.SqlQuery("SELECT * FROM PASSPORT_APPLICATIONFORM WHERE APPLICATIONID =:Id", new { Id = "0EXFQJESIFD491UF530X" }).Read<ApplicationForm>();
                result.Message = JsonConvert.SerializeObject(list);
                //测试非查询SQL执行
                int rowCount = db.ExecuteSqlCommand(@"UPDATE PASSPORT_APPLICATIONFORM " +
                    "                   SET MODIFIEDTIME=:Time WHERE APPLICATIONID =:Id", new { Time = DateTime.Now, Id = "0EXFQJESIFD491UF530X" });
                result.TotalCount = rowCount;
            }
            return result;
        }
    }
}
