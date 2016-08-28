using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2AdInfo
{
    class Win32
    {
        public class User32Wrappers
        {

            public enum GWL : int
            {
                ExStyle = -20
            }

            public enum WS_EX : int
            {
                Transparent = 0x20,
                Layered = 0x80000
            }

            public enum LWA : int
            {
                ColorKey = 0x1,
                Alpha = 0x2
            }

            [DllImport("user32", EntryPoint = "GetWindowLong")]
            public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

            [DllImport("user32", EntryPoint = "SetWindowLong")]
            public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, WS_EX dsNewLong);

            [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
            public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);
        }
    }
}
