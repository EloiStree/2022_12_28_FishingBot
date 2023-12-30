using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ZhuLi_Native_WindowMouseKeyPress : IZhuLiCommand
{
    public User32MouseClassicButton m_buttonToPress;
}
public struct ZhuLi_Native_WindowMouseRelease : IZhuLiCommand
{
    public User32MouseClassicButton m_buttonToPress;
}
public struct ZhuLi_Native_WindowMouseStroke : IZhuLiCommand
{
    public User32MouseClassicButton m_buttonToPress;
    public float m_timeInSecondBetweenStroke;
}



public struct ZhuLi_Native_WindowKeyPress : IZhuLiCommand
{
    public User32KeyboardStrokeCodeEnum m_keyToPress;
}
public struct ZhuLi_Native_WindowKeyRelease : IZhuLiCommand
{
    public User32KeyboardStrokeCodeEnum m_keyToRelease;
}
public struct ZhuLi_Native_WindowKeyStroke : IZhuLiCommand
{
    public User32KeyboardStrokeCodeEnum m_keyToStroke;
    public float m_timeInSecondBetweenStroke;
}