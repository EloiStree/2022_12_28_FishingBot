using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct ZhuLi_ToParse_WindowKeyPress : IZhuLiCommand
{
    public string m_keyToPress;
}
[System.Serializable]
public struct ZhuLi_ToParse_WindowKeyRelease : IZhuLiCommand
{
    public string m_keyToRelease;
}
[System.Serializable]
public struct ZhuLi_ToParse_WindowKeyStroke : IZhuLiCommand
{
    public string m_keyToStroke;
    public float m_timeInSecondBetweenStroke;
}

[System.Serializable]
public struct ZhuLi_WindowAppKeyPression : IZhuLiCommand
{
    public ZhuLiEnum.KeyboardWindowMAppKey m_keyboardKey;
    public ZhuLiEnum.User32PressType m_keyPressionType;
}
[System.Serializable]
public struct ZhuLi_WindowAppKeyStroke : IZhuLiCommand
{
    public ZhuLiEnum.KeyboardWindowMAppKey m_keyboardKey;
    public ZhuLiEnum.User32PressType m_keyToStroke;
    public float m_timeInSecondBetweenStroke;
}
[System.Serializable]

public struct ZhuLi_WindowKeyStablePression : IZhuLiCommand
{
    public ZhuLiEnum.KeyboardStableKey m_keyboardKey;
    public ZhuLiEnum.User32PressType m_keyPressionType;
}
[System.Serializable]
public struct ZhuLi_WindowKeyStableStroke : IZhuLiCommand
{
    public ZhuLiEnum.KeyboardStableKey m_keyboardKey;
    public ZhuLiEnum.User32PressType m_keyToStroke;
    public float m_timeInSecondBetweenStroke;
}

[System.Serializable]
public struct ZhuLi_FrameStableKeyPression : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processesToApplyTo;
    public ZhuLiEnum.KeyboardStableKey m_keyboardKey;
    public ZhuLiEnum.User32PressType m_keyPressionType;
}
/// <summary> USE POST OR SEND MESSAGE ?

[System.Serializable]
public struct ZhuLi_FrameStableKeyStroke : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processesToApplyTo;
    public ZhuLiEnum.KeyboardStableKey m_keyboardKey;
    public ZhuLiEnum.User32PressType m_keyToStroke;
    public float m_timeInSecondBetweenStroke;
}
[System.Serializable]

public struct ZhuLi_CurrentFrameStablePression : IZhuLiCommand
{
    public ZhuLiEnum.KeyboardStableKey m_keyboardKey;
    public ZhuLiEnum.User32PressType m_keyPressionType;
}
[System.Serializable]
public struct ZhuLi_CurrentFrameStableKeyStroke : IZhuLiCommand
{
    public ZhuLiEnum.KeyboardStableKey m_keyboardKey;
    public ZhuLiEnum.User32PressType m_keyToStroke;
    public float m_timeInSecondBetweenStroke;
}





