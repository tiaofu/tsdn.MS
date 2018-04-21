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
using tsdn.RemoteEventBus;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace tsdn.ConfigHandler.Message
{
    /// <summary>
    /// 配置信息 MQ接收消息处理
    /// </summary>
    [RemoteEventHandler(ForType = "ConfigHandlerChanged", ForTopic = "Database.Changed.ConfigHandler", OnlyHandleThisTopic = true)]
    public class ConfigChangeHandler : IRemoteEventHandler
    {
        private readonly IConfigManager manger;

        public ConfigChangeHandler(IConfigManager _manager)
        {
            manger = _manager;
        }

        public void HandleEvent(RemoteEventArgs eventArgs)
        {
            var arr = eventArgs.EventData.Data as JArray;
            string GroupType = string.Empty;
            foreach (var item in arr)
            {
                if (item["Id"] != null && !string.IsNullOrEmpty(item["Id"].ToString()))
                {
                    GroupType = item["Id"].ToString();

                    var config = manger.AllConfig.FirstOrDefault(e => e.GroupType.Equals(GroupType, StringComparison.OrdinalIgnoreCase));
                    if (config != null)
                    {
                        manger.SetValue(config, manger.ConfigService.GetAllOptions(GroupType));
                    }
                }
            }
        }
    }
}
