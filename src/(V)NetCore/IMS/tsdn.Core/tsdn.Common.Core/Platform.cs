/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.Runtime.InteropServices;

namespace tsdn.Common
{
    public static class Platform
    {
        public const string NET_FRAMEWORK = ".NET Framework";
        public const string NET_CORE = ".NET Core";

        public static bool IsFullFramework => RuntimeInformation.FrameworkDescription.StartsWith(NET_FRAMEWORK);

        public static bool IsNetCore => RuntimeInformation.FrameworkDescription.StartsWith(NET_CORE);
    }
}
