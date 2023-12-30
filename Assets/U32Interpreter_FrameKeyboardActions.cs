
using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U32Interpreter_FrameKeyboardActions : AbstractGenericIntepreterMono<IZhuLiCommand>
{
    public override bool CanInterpreterUnderstand(in IZhuLiCommand value)
    {

        return 
            value is ZhuLi_FrameStableKeyPression ||
            value is ZhuLi_FrameStableKeyStroke ||
            value is ZhuLi_CurrentFrameStablePression ||
            value is ZhuLi_CurrentFrameStableKeyStroke ||
            value is ZhuLi_FrameCopyPast ||
            value is ZhuLi_FrameMouseKeyPression;

    }

    public override void TryTranslate(out bool succedToTranslate, in IZhuLiCommand value)
    {

        if (value is ZhuLi_FrameMouseKeyPression)
        {
            ZhuLi_FrameMouseKeyPression request = (ZhuLi_FrameMouseKeyPression)value;
            foreach (var item in request.m_processesToApplyTo.m_processesId)
            {
                IntPtrWrapGet p = IntPtrProcessId.Int(item);
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
                {
                    User32KeyStrokeManager.SendKeyPostMessage(p,GetKeyFromMouseType(request.m_mouseAction.m_mouseButtonType), Parse(request.m_mouseAction.m_pressionType));
                });
            }
            succedToTranslate = true;
        }
        else if(value is ZhuLi_CurrentFrameStablePression)
        {
            ZhuLi_CurrentFrameStablePression request = (ZhuLi_CurrentFrameStablePression)value;

            WindowIntPtrUtility.GetCurrentProcessId(out IntPtrWrapGet process);
            WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(process, out bool found, out IntPtrWrapGet currentUI);
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32KeyStrokeManager.SendKeyPostMessage(currentUI, GetKeyboardKeyFrom(request.m_keyboardKey), Parse(request.m_keyPressionType));
            });
            succedToTranslate = true;
        }
        else if (value is ZhuLi_CurrentFrameStableKeyStroke)
        {
            ZhuLi_CurrentFrameStableKeyStroke request = (ZhuLi_CurrentFrameStableKeyStroke)value;
            WindowIntPtrUtility.GetCurrentProcessId(out IntPtrWrapGet process);
            WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(process, out bool found, out IntPtrWrapGet currentUI);
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32KeyStrokeManager.SendKeyPostMessage(currentUI,GetKeyboardKeyFrom(request.m_keyboardKey), User32PressionType.Press);
            });
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs((int)(request.m_timeInSecondBetweenStroke / 1000f), () => {
                User32KeyStrokeManager.SendKeyPostMessage(currentUI,GetKeyboardKeyFrom(request.m_keyboardKey), User32PressionType.Release);
            }); ;
            succedToTranslate = true;

        }

        else if (value is ZhuLi_FrameStableKeyPression)
        {
            ZhuLi_FrameStableKeyPression request = (ZhuLi_FrameStableKeyPression)value;
            foreach (var item in request.m_processesToApplyTo.m_processesId)
            {
                IntPtrWrapGet p = IntPtrProcessId.Int(item);
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
                {
                    User32KeyStrokeManager.SendKeyPostMessage(p, GetKeyboardKeyFrom(request.m_keyboardKey), Parse(request.m_keyPressionType));
                });
            }
            succedToTranslate = true;
        }
        else if (value is ZhuLi_FrameStableKeyStroke)
        {
            ZhuLi_FrameStableKeyStroke request = (ZhuLi_FrameStableKeyStroke)value;
            foreach (var item in request.m_processesToApplyTo.m_processesId)
                {
                    IntPtrWrapGet p = IntPtrProcessId.Int(item);
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
                    {
                        User32KeyStrokeManager.SendKeyPostMessage(p,GetKeyboardKeyFrom(request.m_keyboardKey), User32PressionType.Press);
                    });
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs((int)(request.m_timeInSecondBetweenStroke / 1000f), () =>
                    {
                        User32KeyStrokeManager.SendKeyPostMessage(p, GetKeyboardKeyFrom(request.m_keyboardKey), User32PressionType.Release);
                    }); ;
            }
            succedToTranslate = true;
        }
        else if (value is ZhuLi_FrameCopyPast)
        {
            ZhuLi_FrameCopyPast request = (ZhuLi_FrameCopyPast)value;
            if (request.m_copyPastType == ZhuLi_FrameCopyPast.CopyPastType.Past)
            {
                foreach (var item in request.m_processesId.m_processesId)
                {
                    IntPtrWrapGet p = IntPtrProcessId.Int(item);
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                        User32KeyStrokeManager.SendKeyPostMessage(p,User32PostMessageKeyEnum.VK_LCONTROL, User32PressionType.Press);
                    });
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                        User32KeyStrokeManager.SendKeyPostMessage(p, GetClipboardKeyFor(request.m_copyPastType), User32PressionType.Press);
                    });
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                        User32KeyStrokeManager.SendKeyPostMessage(p, GetClipboardKeyFor(request.m_copyPastType), User32PressionType.Release);
                    }); ;
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                        User32KeyStrokeManager.SendKeyPostMessage(p, User32PostMessageKeyEnum.VK_LCONTROL, User32PressionType.Release);
                    }); ;
                }
               

            }

            succedToTranslate = true;
        }
        
       

        succedToTranslate = false;
    }

    private User32PostMessageKeyEnum GetKeyFromMouseType(ZhuLiEnum.User32MouseButton m_mouseButtonType)
    {
        switch (m_mouseButtonType)
        {
            case ZhuLiEnum.User32MouseButton.Left:return User32PostMessageKeyEnum.VK_LBUTTON;
            case ZhuLiEnum.User32MouseButton.Middle:return User32PostMessageKeyEnum.VK_MBUTTON;
            case ZhuLiEnum.User32MouseButton.Right:return User32PostMessageKeyEnum.VK_RBUTTON;
            default:
                break;
        }
        throw new Exception("Should not be reach");
    }

    private User32PostMessageKeyEnum GetClipboardKeyFor(ZhuLi_FrameCopyPast.CopyPastType copyType)
    {
        switch (copyType)
        {
            case ZhuLi_FrameCopyPast.CopyPastType.Copy: return User32PostMessageKeyEnum.VK_C;
            case ZhuLi_FrameCopyPast.CopyPastType.Cut: return User32PostMessageKeyEnum.VK_X;
            case ZhuLi_FrameCopyPast.CopyPastType.Past: return User32PostMessageKeyEnum.VK_V;
        }
        throw new Exception("Shoul not be reach");
    }

    private User32PostMessageKeyEnum GetKeyboardKeyFrom(ZhuLiEnum.KeyboardWindowMAppKey key)
    {
        switch (key)
        {

            //case ZhuLiEnum.KeyboardWindowMAppKey.MyComputer: return User32KeyboardStrokeCodeEnum.;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Send: return User32KeyboardStrokeCodeEnum.PAUSE;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Redo: return User32KeyboardStrokeCodeEnum.redo;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Calculator: return User32KeyboardStrokeCodeEnum.;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Mutemicrophone: return User32KeyboardStrokeCodeEnum.;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Help: return User32KeyboardStrokeCodeEnum.help;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Find: return User32KeyboardStrokeCodeEnum.find;
            //case ZhuLiEnum.KeyboardWindowMAppKey.New: return User32KeyboardStrokeCodeEnum.;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Open: return User32KeyboardStrokeCodeEnum.open;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Close: return User32KeyboardStrokeCodeEnum.c;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Save: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Print: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Undo: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Copy: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Cut: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Paste: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Reply: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.SpellingChecker: return User32KeyboardStrokeCodeEnum.spe;
            //case ZhuLiEnum.KeyboardWindowMAppKey.ToggleDictation: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.ToggleMicrophone: return User32KeyboardStrokeCodeEnum.KEY_A;
            //case ZhuLiEnum.KeyboardWindowMAppKey.Corrections: return User32KeyboardStrokeCodeEnum.KEY_A;


            case ZhuLiEnum.KeyboardWindowMAppKey.Back: return        User32PostMessageKeyEnum.VK_BROWSER_BACK;
            case ZhuLiEnum.KeyboardWindowMAppKey.Forward: return     User32PostMessageKeyEnum.VK_BROWSER_FORWARD;
            case ZhuLiEnum.KeyboardWindowMAppKey.Refresh: return     User32PostMessageKeyEnum.VK_BROWSER_REFRESH;
            case ZhuLiEnum.KeyboardWindowMAppKey.Stop: return        User32PostMessageKeyEnum.VK_BROWSER_STOP;
            case ZhuLiEnum.KeyboardWindowMAppKey.Search: return      User32PostMessageKeyEnum.VK_BROWSER_SEARCH;
            case ZhuLiEnum.KeyboardWindowMAppKey.Favourites: return  User32PostMessageKeyEnum.VK_BROWSER_FAVORITES;
            case ZhuLiEnum.KeyboardWindowMAppKey.WebHome: return     User32PostMessageKeyEnum.VK_BROWSER_HOME;
            case ZhuLiEnum.KeyboardWindowMAppKey.Mutevolume: return  User32PostMessageKeyEnum.VK_VOLUME_MUTE;
            case ZhuLiEnum.KeyboardWindowMAppKey.Mail: return        User32PostMessageKeyEnum.VK_LAUNCH_MAIL;
            case ZhuLiEnum.KeyboardWindowMAppKey.Media: return       User32PostMessageKeyEnum.VK_LAUNCH_MEDIA_SELECT;
            case ZhuLiEnum.KeyboardWindowMAppKey.LowerVolume: return User32PostMessageKeyEnum.VK_VOLUME_DOWN;
            case ZhuLiEnum.KeyboardWindowMAppKey.RaiseVolume: return User32PostMessageKeyEnum.VK_VOLUME_UP;
            case ZhuLiEnum.KeyboardWindowMAppKey.ForwardMail: return User32PostMessageKeyEnum.VK_LAUNCH_MAIL;

            default: throw new NotImplementedException("A key is not implemented: " + key);
        }
    }


    private User32PostMessageKeyEnum GetKeyboardKeyFrom(ZhuLiEnum.KeyboardStableKey m_keyboardKey)
    {
        switch (m_keyboardKey)
        {

            case ZhuLiEnum.KeyboardStableKey.A:     return User32PostMessageKeyEnum.VK_A;
            case ZhuLiEnum.KeyboardStableKey.B:     return User32PostMessageKeyEnum.VK_B;
            case ZhuLiEnum.KeyboardStableKey.C:     return User32PostMessageKeyEnum.VK_C;
            case ZhuLiEnum.KeyboardStableKey.D:     return User32PostMessageKeyEnum.VK_D;
            case ZhuLiEnum.KeyboardStableKey.E:     return User32PostMessageKeyEnum.VK_E;
            case ZhuLiEnum.KeyboardStableKey.F:     return User32PostMessageKeyEnum.VK_F;
            case ZhuLiEnum.KeyboardStableKey.G:     return User32PostMessageKeyEnum.VK_G;
            case ZhuLiEnum.KeyboardStableKey.H:     return User32PostMessageKeyEnum.VK_H;
            case ZhuLiEnum.KeyboardStableKey.I:     return User32PostMessageKeyEnum.VK_I;
            case ZhuLiEnum.KeyboardStableKey.J:     return User32PostMessageKeyEnum.VK_J;
            case ZhuLiEnum.KeyboardStableKey.K:     return User32PostMessageKeyEnum.VK_K;
            case ZhuLiEnum.KeyboardStableKey.L:     return User32PostMessageKeyEnum.VK_L;
            case ZhuLiEnum.KeyboardStableKey.M:     return User32PostMessageKeyEnum.VK_M;
            case ZhuLiEnum.KeyboardStableKey.N:     return User32PostMessageKeyEnum.VK_N;
            case ZhuLiEnum.KeyboardStableKey.O:     return User32PostMessageKeyEnum.VK_O;
            case ZhuLiEnum.KeyboardStableKey.P:     return User32PostMessageKeyEnum.VK_P;
            case ZhuLiEnum.KeyboardStableKey.Q:     return User32PostMessageKeyEnum.VK_Q;
            case ZhuLiEnum.KeyboardStableKey.R:     return User32PostMessageKeyEnum.VK_R;
            case ZhuLiEnum.KeyboardStableKey.S:     return User32PostMessageKeyEnum.VK_S;
            case ZhuLiEnum.KeyboardStableKey.T:     return User32PostMessageKeyEnum.VK_T;
            case ZhuLiEnum.KeyboardStableKey.U:     return User32PostMessageKeyEnum.VK_U;
            case ZhuLiEnum.KeyboardStableKey.V:     return User32PostMessageKeyEnum.VK_V;
            case ZhuLiEnum.KeyboardStableKey.W:     return User32PostMessageKeyEnum.VK_W;
            case ZhuLiEnum.KeyboardStableKey.X:     return User32PostMessageKeyEnum.VK_X;
            case ZhuLiEnum.KeyboardStableKey.Y:     return User32PostMessageKeyEnum.VK_Y;
            case ZhuLiEnum.KeyboardStableKey.Z:     return User32PostMessageKeyEnum.VK_Z;
            case ZhuLiEnum.KeyboardStableKey._0:    return User32PostMessageKeyEnum.VK_0;
            case ZhuLiEnum.KeyboardStableKey._1:    return User32PostMessageKeyEnum.VK_1;
            case ZhuLiEnum.KeyboardStableKey._2:    return User32PostMessageKeyEnum.VK_2;
            case ZhuLiEnum.KeyboardStableKey._3:    return User32PostMessageKeyEnum.VK_3;
            case ZhuLiEnum.KeyboardStableKey._4:    return User32PostMessageKeyEnum.VK_4;
            case ZhuLiEnum.KeyboardStableKey._5:    return User32PostMessageKeyEnum.VK_5;
            case ZhuLiEnum.KeyboardStableKey._6:    return User32PostMessageKeyEnum.VK_6;
            case ZhuLiEnum.KeyboardStableKey._7:    return User32PostMessageKeyEnum.VK_7;
            case ZhuLiEnum.KeyboardStableKey._8:    return User32PostMessageKeyEnum.VK_8;
            case ZhuLiEnum.KeyboardStableKey._9:    return User32PostMessageKeyEnum.VK_9;
            case ZhuLiEnum.KeyboardStableKey.NP0:   return User32PostMessageKeyEnum.VK_NUMPAD0;
            case ZhuLiEnum.KeyboardStableKey.NP1:   return User32PostMessageKeyEnum.VK_NUMPAD1;
            case ZhuLiEnum.KeyboardStableKey.NP2:   return User32PostMessageKeyEnum.VK_NUMPAD2;
            case ZhuLiEnum.KeyboardStableKey.NP3:   return User32PostMessageKeyEnum.VK_NUMPAD3;
            case ZhuLiEnum.KeyboardStableKey.NP4:   return User32PostMessageKeyEnum.VK_NUMPAD4;
            case ZhuLiEnum.KeyboardStableKey.NP5:   return User32PostMessageKeyEnum.VK_NUMPAD5;
            case ZhuLiEnum.KeyboardStableKey.NP6:   return User32PostMessageKeyEnum.VK_NUMPAD6;
            case ZhuLiEnum.KeyboardStableKey.NP7:   return User32PostMessageKeyEnum.VK_NUMPAD7;
            case ZhuLiEnum.KeyboardStableKey.NP8:   return User32PostMessageKeyEnum.VK_NUMPAD8;
            case ZhuLiEnum.KeyboardStableKey.NP9:   return User32PostMessageKeyEnum.VK_NUMPAD9;
            case ZhuLiEnum.KeyboardStableKey.F1:    return User32PostMessageKeyEnum.VK_F1;
            case ZhuLiEnum.KeyboardStableKey.F2:    return User32PostMessageKeyEnum.VK_F2;
            case ZhuLiEnum.KeyboardStableKey.F3:    return User32PostMessageKeyEnum.VK_F3;
            case ZhuLiEnum.KeyboardStableKey.F4:    return User32PostMessageKeyEnum.VK_F4;
            case ZhuLiEnum.KeyboardStableKey.F5:    return User32PostMessageKeyEnum.VK_F5;
            case ZhuLiEnum.KeyboardStableKey.F6:    return User32PostMessageKeyEnum.VK_F6;
            case ZhuLiEnum.KeyboardStableKey.F7:    return User32PostMessageKeyEnum.VK_F7;
            case ZhuLiEnum.KeyboardStableKey.F8:    return User32PostMessageKeyEnum.VK_F8;
            case ZhuLiEnum.KeyboardStableKey.F9:    return User32PostMessageKeyEnum.VK_F9;
            case ZhuLiEnum.KeyboardStableKey.F10:   return User32PostMessageKeyEnum.VK_F10;
            case ZhuLiEnum.KeyboardStableKey.F11:   return User32PostMessageKeyEnum.VK_F11;
            case ZhuLiEnum.KeyboardStableKey.F12:   return User32PostMessageKeyEnum.VK_F12;
            case ZhuLiEnum.KeyboardStableKey.F13:   return User32PostMessageKeyEnum.VK_F13;
            case ZhuLiEnum.KeyboardStableKey.F14:   return User32PostMessageKeyEnum.VK_F14;
            case ZhuLiEnum.KeyboardStableKey.F15:   return User32PostMessageKeyEnum.VK_F15;
            case ZhuLiEnum.KeyboardStableKey.F16:   return User32PostMessageKeyEnum.VK_F16;
            case ZhuLiEnum.KeyboardStableKey.F17:   return User32PostMessageKeyEnum.VK_F17;
            case ZhuLiEnum.KeyboardStableKey.F18:   return User32PostMessageKeyEnum.VK_F18;
            case ZhuLiEnum.KeyboardStableKey.F19:   return User32PostMessageKeyEnum.VK_F19;
            case ZhuLiEnum.KeyboardStableKey.F20:   return User32PostMessageKeyEnum.VK_F20;
            case ZhuLiEnum.KeyboardStableKey.F21:   return User32PostMessageKeyEnum.VK_F21;
            case ZhuLiEnum.KeyboardStableKey.F22:   return User32PostMessageKeyEnum.VK_F22;
            case ZhuLiEnum.KeyboardStableKey.F23:   return User32PostMessageKeyEnum.VK_F23;
            case ZhuLiEnum.KeyboardStableKey.F24:   return User32PostMessageKeyEnum.VK_F24;
            case ZhuLiEnum.KeyboardStableKey.NPAdd: return User32PostMessageKeyEnum.VK_ADD;
            case ZhuLiEnum.KeyboardStableKey.NPSub: return User32PostMessageKeyEnum.VK_SUBTRACT;
            case ZhuLiEnum.KeyboardStableKey.NPMul: return User32PostMessageKeyEnum.VK_MULTIPLY;
            case ZhuLiEnum.KeyboardStableKey.NPDiv: return User32PostMessageKeyEnum.VK_DIVIDE;
            case ZhuLiEnum.KeyboardStableKey.NPDot: return User32PostMessageKeyEnum.VK_DECIMAL;
            case ZhuLiEnum.KeyboardStableKey.NPEnter: return User32PostMessageKeyEnum.VK_RETURN;
            case ZhuLiEnum.KeyboardStableKey.ArrowLeft: return  User32PostMessageKeyEnum.VK_LEFT;
            case ZhuLiEnum.KeyboardStableKey.ArrowRight: return User32PostMessageKeyEnum.VK_RIGHT;
            case ZhuLiEnum.KeyboardStableKey.ArrowDown: return  User32PostMessageKeyEnum.VK_DOWN;
            case ZhuLiEnum.KeyboardStableKey.ArrowUp: return    User32PostMessageKeyEnum.VK_UP;
            case ZhuLiEnum.KeyboardStableKey.Tab: return        User32PostMessageKeyEnum.VK_TAB;
            case ZhuLiEnum.KeyboardStableKey.Enter: return      User32PostMessageKeyEnum.VK_RETURN;
            case ZhuLiEnum.KeyboardStableKey.Backspace: return  User32PostMessageKeyEnum.VK_BACK;
            case ZhuLiEnum.KeyboardStableKey.ShiftLeft: return  User32PostMessageKeyEnum.VK_LSHIFT;
            case ZhuLiEnum.KeyboardStableKey.ShiftRight: return User32PostMessageKeyEnum.VK_RSHIFT;
            case ZhuLiEnum.KeyboardStableKey.CtrlLeft: return   User32PostMessageKeyEnum.VK_LCONTROL;
            case ZhuLiEnum.KeyboardStableKey.CtrlRight: return  User32PostMessageKeyEnum.VK_RCONTROL;
            case ZhuLiEnum.KeyboardStableKey.Altleft: return    User32PostMessageKeyEnum.VK_LMENU;
            case ZhuLiEnum.KeyboardStableKey.AltRight: return   User32PostMessageKeyEnum.VK_RMENU;
            case ZhuLiEnum.KeyboardStableKey.ShiftLock: return  User32PostMessageKeyEnum.VK_CAPITAL;
            case ZhuLiEnum.KeyboardStableKey.NumLock: return    User32PostMessageKeyEnum.VK_NUMLOCK;
            case ZhuLiEnum.KeyboardStableKey.PlatformButtonLeft: return User32PostMessageKeyEnum.VK_LWIN;
            case ZhuLiEnum.KeyboardStableKey.PlatformButtonRight: return User32PostMessageKeyEnum.VK_RWIN;
            case ZhuLiEnum.KeyboardStableKey.RightMenu: return User32PostMessageKeyEnum.VK_APPS;
            case ZhuLiEnum.KeyboardStableKey.PrintScreen: return User32PostMessageKeyEnum.VK_SNAPSHOT;
            case ZhuLiEnum.KeyboardStableKey.Insert: return     User32PostMessageKeyEnum.VK_INSERT;
            case ZhuLiEnum.KeyboardStableKey.Delete: return     User32PostMessageKeyEnum.VK_DELETE;
            case ZhuLiEnum.KeyboardStableKey.Escape: return     User32PostMessageKeyEnum.VK_ESCAPE;
            case ZhuLiEnum.KeyboardStableKey.PageDown: return   User32PostMessageKeyEnum.VK_NEXT;
            case ZhuLiEnum.KeyboardStableKey.PageUp: return     User32PostMessageKeyEnum.VK_PRIOR;
            case ZhuLiEnum.KeyboardStableKey.End: return        User32PostMessageKeyEnum.VK_END;
            case ZhuLiEnum.KeyboardStableKey.Home: return       User32PostMessageKeyEnum.VK_HOME;
            case ZhuLiEnum.KeyboardStableKey.Space: return      User32PostMessageKeyEnum.VK_SPACE;
            case ZhuLiEnum.KeyboardStableKey.Pause: return      User32PostMessageKeyEnum.VK_PAUSE;
            case ZhuLiEnum.KeyboardStableKey.VolumeDown: return User32PostMessageKeyEnum.VK_VOLUME_DOWN;
            case ZhuLiEnum.KeyboardStableKey.VolumeUp: return   User32PostMessageKeyEnum.VK_VOLUME_UP;
            case ZhuLiEnum.KeyboardStableKey.VolumeMute: return User32PostMessageKeyEnum.VK_VOLUME_MUTE;
            case ZhuLiEnum.KeyboardStableKey.MediaStop: return User32PostMessageKeyEnum.VK_MEDIA_STOP;
            case ZhuLiEnum.KeyboardStableKey.MediaToggle: return User32PostMessageKeyEnum.VK_MEDIA_PLAY_PAUSE;
            case ZhuLiEnum.KeyboardStableKey.MediaPrevious: return User32PostMessageKeyEnum.VK_MEDIA_PREV_TRACK;
            case ZhuLiEnum.KeyboardStableKey.MediaNext: return User32PostMessageKeyEnum.VK_MEDIA_NEXT_TRACK;
            case ZhuLiEnum.KeyboardStableKey.Print: return      User32PostMessageKeyEnum.VK_PRINT;
            case ZhuLiEnum.KeyboardStableKey.Sleep: return      User32PostMessageKeyEnum.VK_SLEEP;
            case ZhuLiEnum.KeyboardStableKey.OEM_1: return      User32PostMessageKeyEnum.VK_OEM_1;
            case ZhuLiEnum.KeyboardStableKey.OEM_2: return      User32PostMessageKeyEnum.VK_OEM_2;
            case ZhuLiEnum.KeyboardStableKey.OEM_3: return      User32PostMessageKeyEnum.VK_OEM_3;
            case ZhuLiEnum.KeyboardStableKey.OEM_4: return      User32PostMessageKeyEnum.VK_OEM_4;
            case ZhuLiEnum.KeyboardStableKey.OEM_5: return      User32PostMessageKeyEnum.VK_OEM_5;
            case ZhuLiEnum.KeyboardStableKey.OEM_6: return      User32PostMessageKeyEnum.VK_OEM_6;
            case ZhuLiEnum.KeyboardStableKey.OEM_7: return      User32PostMessageKeyEnum.VK_OEM_7;
            case ZhuLiEnum.KeyboardStableKey.OEM_8: return      User32PostMessageKeyEnum.VK_OEM_8;
            case ZhuLiEnum.KeyboardStableKey.OEM_102: return    User32PostMessageKeyEnum.VK_OEM_102;
            case ZhuLiEnum.KeyboardStableKey.OEM_PLUS: return   User32PostMessageKeyEnum.VK_OEM_PLUS;
            case ZhuLiEnum.KeyboardStableKey.OEM_COMMA: return  User32PostMessageKeyEnum.VK_OEM_COMMA;
            case ZhuLiEnum.KeyboardStableKey.OEM_MINUS: return  User32PostMessageKeyEnum.VK_OEM_MINUS;
            case ZhuLiEnum.KeyboardStableKey.OEM_PERIOD: return User32PostMessageKeyEnum.VK_OEM_PERIOD;
            case ZhuLiEnum.KeyboardStableKey.VK_OEM_CLEAR: return User32PostMessageKeyEnum.VK_OEM_CLEAR;
            default: throw new NotImplementedException("A key had been add and is not implemented");
        }
    }

    private User32PressionType Parse(ZhuLiEnum.User32PressType m_keyPressionType)
    {
        if (m_keyPressionType == ZhuLiEnum.User32PressType.Press) return User32PressionType.Press;
        else return User32PressionType.Release;
    }

}