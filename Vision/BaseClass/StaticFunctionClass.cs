using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vision.BaseClass;

public static class UIExtensions
{
    /// <summary>
    /// 线程安全的UI操作扩展方法
    /// </summary>
    public static void InvokeIfRequired(this Control control, Action action)
    {
        if (control == null) return;

        if (control.InvokeRequired)
        {
            control.Invoke(action);
        }
        else
        {
            action();
        }
    }
}