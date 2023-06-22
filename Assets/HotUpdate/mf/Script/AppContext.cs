using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
internal static class AppContext
{
    internal static ResourcesModule ResourcesModule => ResourcesModule.Instance;
    internal static UIModule UIModule => UIModule.Instance;
    internal static CameraModule CameraModule => CameraModule.Instance;
    internal static FSMModule FSMModule => FSMModule.Instance;
    internal static LogModule LogModule => LogModule.Instance;
    internal static MessageModule MessageModule => MessageModule.Instance;
    internal static TableModule TableModule => TableModule.Instance;
    internal static NetModule NetModule => NetModule.Instance;

    /// <summary>
    /// 初始化上下文
    /// </summary>
    internal static void InitAppContext()
    {

    }
}
