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
using Autofac;
using tsdn.Reflection;
using tsdn.Common.Micro;
using tsdn.Common.Core;

namespace tsdn.Common.Config.Menu
{
    public class MenuConfig : IDependencyRegistrar
    {
        public int Order => 40;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<ImsMenuRegister>().Keyed<IMenuRegister>(RegisterType.IMS.ToString());
        }
    }
}
