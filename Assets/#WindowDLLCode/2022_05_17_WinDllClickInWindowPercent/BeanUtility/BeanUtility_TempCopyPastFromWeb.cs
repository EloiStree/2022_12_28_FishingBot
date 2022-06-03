using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BeanUtility_TempCopyPastFromWeb 
{
    [DllImportAttribute("user32.dll", EntryPoint = "GetForegroundWindow")]
    public static extern IntPtr GetForegroundWindow();

    //https://zetcode.com/csharp/process/
    //Processs.Kill();
}
