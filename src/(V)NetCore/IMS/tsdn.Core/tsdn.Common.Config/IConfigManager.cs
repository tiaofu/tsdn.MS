/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace tsdn.Common.Config
{
    /// <summary>
    /// 单例参数配置接口
    /// </summary>
    public interface IConfigManager : ISingletonDependency
    {
        /// <summary>
        /// 所有配置项
        /// </summary>
        IEnumerable<ConfigOption> AllConfig { get; }

        /// <summary>
        /// 参数配置服务
        /// </summary>
        IConfigService ConfigService { get; }

        /// <summary>
        /// 获取所有的参数配置项
        /// </summary>
        /// <param name="GroupType">分组信息</param>
        /// <returns></returns>
        List<OptionViewModel> GetAllOption(string GroupType = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupType"></param>
        /// <returns></returns>
        OptionViewModel GetOptionByGroup(string GroupType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Options GetOptionByGroupAndKey(string GroupType, string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="AfterSave"></param>
        /// <returns></returns>
        ApiResult<string> Save(OptionViewModel value, bool AfterSave = true);

        /// <summary>
        /// 初始化参数配置
        /// </summary>
        void Init();

        /// <summary>
        /// 保存时 对当前配置项进行赋值
        /// </summary>
        /// <param name="item">当前配置项</param>
        /// <param name="ListOptions">配置项值</param>
        /// <param name="AfterSave">是否调用保存后方法</param>
        void SetValue(ConfigOption item, List<Options> ListOptions, bool AfterSave = true);
    }
}
