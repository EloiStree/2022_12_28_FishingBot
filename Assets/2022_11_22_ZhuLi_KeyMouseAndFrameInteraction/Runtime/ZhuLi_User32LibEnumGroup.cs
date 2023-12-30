using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static partial class ZhuLiEnum
{
    public enum User32MoveType { SetAtPosition, MoveFromPosition }
    public enum User32ValueType { Pixel, Percent }
    public enum User32AxesDirection2D { L2R_D2T, R2L_D2T, L2R_T2D, R2L_T2D, }
    public enum User32AxisDirection2D { L2R, R2L, D2T,T2D }


    public enum User32PressType { Press, Release}
    public enum User32MouseButton { Left,Middle,Right}

    /// <summary>
    /// PostMessage = UDP kind of, SendMssage TCP kind of message.
    /// </summary>
    public enum FakeInputType { Broadcast, WaitCallBack}
    public enum KeyboardStableKey {
            A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y, Z
            , _0, _1, _2, _3, _4, _5, _6, _7, _8, _9
            , NP0, NP1, NP2, NP3, NP4, NP5, NP6, NP7, NP8, NP9
            , F1,  F2,  F3,  F4,  F5,  F6,  F7,  F8,  F9
            , F10, F11, F12, F13, F14, F15, F16, F17, F18, F19
            , F20, F21, F22, F23, F24
            , NPAdd, NPSub, NPMul, NPDiv, NPDot, NPEnter 
            , ArrowLeft, ArrowRight, ArrowDown, ArrowUp
            , Tab, Enter, Backspace
            , ShiftLeft, ShiftRight
            , CtrlLeft, CtrlRight
            , Altleft, AltRight 
            , ShiftLock, NumLock
            , PlatformButtonLeft   , PlatformButtonRight
            , RightMenu
            , PrintScreen, Insert, Delete, Escape
            , PageDown, PageUp, End, Home, Space,
                Pause,
        VolumeDown, VolumeUp, VolumeMute, MediaStop, MediaToggle, MediaPrevious, MediaNext
            ,  Print, Sleep,
        OEM_1, OEM_2, OEM_3, OEM_4, OEM_5, OEM_6, OEM_7, OEM_8,
        OEM_102, OEM_PLUS, OEM_COMMA, OEM_MINUS, OEM_PERIOD, VK_OEM_CLEAR

    }


    public enum KeyboardSpecial { 
        OEM_1, OEM_2,OEM_3,OEM_4,OEM_5,OEM_6,OEM_7, OEM_8,
        OEM_102, OEM_PLUS, OEM_COMMA,OEM_MINUS,OEM_PERIOD, VK_OEM_CLEAR


    }

//https://gist.github.com/Citillara/fed3bf32f21afaa0839af1390cfb7ffc
public enum KeyboardWindowMAppKey {Back,Forward,Refresh,Stop,Search,Favourites,WebHome,Mutevolume,Mail
            ,Media,MyComputer,Calculator,Mutemicrophone,LowerVolume,RaiseVolume,Help,Find,New,Open
            ,Close,Save,Print,Undo,Redo,Copy,Cut,Paste,Reply,ForwardMail,Send,SpellingChecker
            ,ToggleDictation,ToggleMicrophone,Corrections
}
}