using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct ZhuLi_SetClipboardWithText : IZhuLiCommand
{
    public string m_textToSendToClipboard;
}
[System.Serializable]

public struct ZhuLi_ClipboardTextAsEmpty : IZhuLiCommand
{
    public string m_textToSendToClipboard;
}


[System.Serializable]
public struct ZhuLi_WindowCopyPast : IZhuLiCommand
{
    public enum CopyPastType { Copy, Cut, Past }
    public CopyPastType m_copyPastType;
}


[System.Serializable]
public struct ZhuLi_FrameCopyPast : IZhuLiCommand
{
    public ZhuLiStruct.User32.ProcessesArrayOfId m_processesId;
    public enum CopyPastType { Copy, Cut, Past }
    public CopyPastType m_copyPastType;
}


[System.Serializable]
public struct ZhuLi_SetClipboardImageWithTexture2D : IZhuLiCommand
{
    public Texture2D m_textToSendToClipboard;
}

