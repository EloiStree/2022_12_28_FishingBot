using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region DESKTOP



#region Mouse CLICK Actions

[System.Serializable]
public struct ZhuLi_WindowMouseKeyPression : IZhuLiCommand
{
    public ZhuLiStruct.User32.MouseAction m_mouseAction;
}

[System.Serializable]
public struct ZhuLi_WindowMouseStroke : IZhuLiCommand
{
    public ZhuLiEnum.User32MouseButton m_mouseButtonType;
    public float m_timeInSecondBetweenStroke;
}

[System.Serializable]

public struct ZhuLi_CurrentFrameMouseKeyPression : IZhuLiCommand
{
    public ZhuLiStruct.User32.MouseAction m_mouseAction;
}

[System.Serializable]
public struct ZhuLi_CurrentFrameMouseStroke : IZhuLiCommand
{
    public ZhuLiEnum.User32MouseButton m_mouseButtonType;
    public float m_timeInSecondBetweenStroke;
}

[System.Serializable]
public struct ZhuLi_FrameMouseKeyPression : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processesToApplyTo;
    public ZhuLiStruct.User32.MouseAction m_mouseAction;
}
[System.Serializable]

public struct ZhuLi_FrameMouseStroke : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processesToApplyTo;
    public ZhuLiEnum.User32MouseButton m_mouseButtonType;
    public float m_timeInSecondBetweenStroke;
}
#endregion





[System.Serializable]

public struct ZhuLi_WindowMouseMoveMainWindowAxes : IZhuLiCommand
{
    public ZhuLiStruct.User32.AxesCoordinateInfo m_moveValue;
}
[System.Serializable]
public struct ZhuLi_WindowMouseMoveMainWindowAxis : IZhuLiCommand
{
    public ZhuLiStruct.User32.AxisCoordinateInfo m_moveValue;
}
[System.Serializable]

public struct ZhuLi_WindowMouseMoveAtAliasMonitorAxes : IZhuLiCommand
{
    public string m_aliasNameOfMonitor;
    public ZhuLiStruct.User32.AxesCoordinateInfo m_moveValue;
}

[System.Serializable]
public struct ZhuLi_WindowMouseMoveAtAliasMonitorAxis : IZhuLiCommand
{
    public string m_aliasNameOfMonitor;
    public ZhuLiStruct.User32.AxisCoordinateInfo m_moveValue;
}

[System.Serializable]
public struct ZhuLi_SaveCurrentMonitorWithAliasName : IZhuLiCommand
{
    public string m_aliasNameOfMonitor;
}


#endregion
#region FRAME CMD

[System.Serializable]
public struct ZhuLi_CurrentFrameMouseMoveAxes : IZhuLiCommand
{
    public ZhuLiStruct.User32.AxesCoordinateInfo m_moveValue;
}
[System.Serializable]
public struct ZhuLi_CurrentFrameMouseMoveAxis : IZhuLiCommand
{
    public ZhuLiStruct.User32.AxisCoordinateInfo m_moveValue;
}

[System.Serializable]
public struct ZhuLi_FrameMouseMoveAxes : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processesToApplyTo;
    public ZhuLiStruct.User32.AxesCoordinateInfo  m_moveValue;
}
[System.Serializable]
public struct ZhuLi_FrameMouseMoveAxis : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processesToApplyTo;
    public ZhuLiStruct.User32.AxisCoordinateInfo m_moveValue;
}



#endregion


/// <summary>
/// Note: I am not sure we can simulate horizontal axis, at least I don't know how.
/// </summary>
[System.Serializable]
public struct ZhuLi_WindowMouseScoll : IZhuLiCommand
{
    public enum MouseScrollType { VerticalBack2Forward, HorizontalLeft2Right}
    public MouseScrollType m_scrollType;
    public int m_scrollValue;
}
