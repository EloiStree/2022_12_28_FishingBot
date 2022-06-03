using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class BeanUtility_AccessWindowPositionFromProcessId 
{

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr FindWindow(string strClassName, string strWindowName);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

    [System.Serializable]
    public struct Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }

    public static void GetScreenPositionFromWindow(IntPtr mainWindowHandlePtr, out Rect windowPosition) {
        windowPosition = new Rect();
        GetWindowRect(mainWindowHandlePtr, ref windowPosition);
    }
}
