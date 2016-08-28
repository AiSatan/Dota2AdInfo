using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dota2AdInfo
{
    internal static class Win32Delegates
    {
        internal enum GWL : int
        {
            ExStyle = -20
        }

        internal enum WS_EX : int
        {
            Transparent = 0x20,
            Layered = 0x80000
        }

        internal enum LWA : int
        {
            ColorKey = 0x1,
            Alpha = 0x2
        }

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dsNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        internal static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        internal struct MousePoint
        {
            public uint X;
            public uint Y;

            public MousePoint(uint x, uint y)
            {
                X = x;
                Y = y;
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(out MousePoint lpMousePoint);

    }
}
