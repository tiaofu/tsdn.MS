/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Core;
using System;

namespace tsdn.Common.Module.Common
{
    public class ConsoleConfigure : IAfterRunConfigure
    {
        public int Order
        {
            get
            {
                return 100;
            }
        }

        public void Configure()
        {
            Console.Title = SysConfig.MicroServiceOption.Application.Title + " " + SysConfig.MicroServiceOption.Application.Version;
        }
    }
}
