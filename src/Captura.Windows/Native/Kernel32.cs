using System;
using System.Runtime.InteropServices;

namespace Captura.Native
{
    public class Kernel32
    {
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr Dest, IntPtr Src, int Count);
    }
}
