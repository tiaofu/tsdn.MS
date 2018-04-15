/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong.
 * Address: wuhan
 * Create: 2018/4/14 11:11:05
 * Modify: 2018/4/14 11:11:05
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 
 *    所有接口的依赖接口，每次创建新实例
 *    用于Autofac自动注册时，查找所有依赖该接口的实现
 *    实现自动注册功能
 *********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tsdn.Core
{
    public interface ITransientDependency
    {
    }
}
