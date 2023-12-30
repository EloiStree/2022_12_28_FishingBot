using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ZhuLi_SetFocusOnFrame : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessUniqueId m_processToApplyTo;
}

[System.Serializable]
public struct ZhuLi_SetFrameDisplayState : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processToApplyTo;
    public enum DisplayStateType { TotallyHidden, HiddenInBar, Normal, Maximized }
    public DisplayStateType m_displayStateType;
}




[System.Serializable]
public struct ZhuLi_SetFramePositionOnScreen : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processToApplyTo;
    public string m_monitorAliasName;
    public ZhuLiStruct.User32.AxesCoordinateInfo m_botLeftCorner;
    public ZhuLiStruct.User32.AxesCoordinateInfo m_topRightCorner;
}

[System.Serializable]
public struct ZhuLi_SetFramePositionFromWindowNativeCursorPosition : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processToApplyTo;
    public string m_monitorAliasName;
    public float m_x;
    public float m_y;
}


public class AbstractTarget {
    public class Bean_AllProcessesWithExactName
    {
        public string m_processName;
    }
    public class Bean_OneProcessFromIndexOfExactNameSearch
    {
        public string m_processName;
        public int m_indexInListCallback;
    }
    public class Bean_AllProcessesUnderScreenCoordinate
    {
        public string m_monitorAliasName;
        public ZhuLiStruct.User32.AxesCoordinateInfo m_cusorPosition;
    }

}
