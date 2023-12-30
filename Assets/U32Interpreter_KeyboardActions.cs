using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U32Interpreter_KeyboardActions : AbstractGenericIntepreterMono<IZhuLiCommand>
{
    public override bool CanInterpreterUnderstand(in IZhuLiCommand value)
    {
      
            return value is ZhuLi_WindowAppKeyPression ||
                value is ZhuLi_WindowAppKeyStroke ||
                value is ZhuLi_WindowKeyStablePression ||
                value is ZhuLi_WindowKeyStableStroke ||
                value is ZhuLi_WindowMouseKeyPression ||
                value is ZhuLi_WindowCopyPast;

    }

    public override void TryTranslate(out bool succedToTranslate, in IZhuLiCommand value)
    {
        if (value is ZhuLi_WindowKeyStablePression) {
            ZhuLi_WindowKeyStablePression request=(ZhuLi_WindowKeyStablePression)value;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32KeyStrokeManager.SendKeyStroke(GetKeyboardKeyFrom(request.m_keyboardKey), Parse(request.m_keyPressionType));
            });
                }
        else if (value is ZhuLi_WindowKeyStableStroke)
        {
            ZhuLi_WindowKeyStableStroke request = (ZhuLi_WindowKeyStableStroke)value;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32KeyStrokeManager.SendKeyStroke(GetKeyboardKeyFrom(request.m_keyboardKey), User32PressionType.Press);
            });
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs((int)(request.m_timeInSecondBetweenStroke / 1000f), () => {
                User32KeyStrokeManager.SendKeyStroke(GetKeyboardKeyFrom(request.m_keyboardKey), User32PressionType.Release);
            }); ;

        }
        else if (value is ZhuLi_WindowCopyPast)
        {
            ZhuLi_WindowCopyPast request = (ZhuLi_WindowCopyPast)value;
            if (request.m_copyPastType == ZhuLi_WindowCopyPast.CopyPastType.Past) {

                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                    User32KeyStrokeManager.SendKeyStroke(User32KeyboardStrokeCodeEnum.LCONTROL, User32PressionType.Press);
                });
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32KeyStrokeManager.SendKeyStroke(GetClipboardKeyFor(request.m_copyPastType), User32PressionType.Press);
            });
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32KeyStrokeManager.SendKeyStroke(GetClipboardKeyFor(request.m_copyPastType), User32PressionType.Release);
            }); ;
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                    User32KeyStrokeManager.SendKeyStroke(User32KeyboardStrokeCodeEnum.LCONTROL, User32PressionType.Release);
                }); ;

            }

        }
        else if (value is ZhuLi_WindowAppKeyPression) {

            ZhuLi_WindowAppKeyPression request = (ZhuLi_WindowAppKeyPression)value;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32KeyStrokeManager.SendKeyStroke(GetKeyboardKeyFrom(request.m_keyboardKey), Parse(request.m_keyPressionType));
            });
        }
        else if (value is ZhuLi_WindowAppKeyStroke) {

            ZhuLi_WindowAppKeyStroke request = (ZhuLi_WindowAppKeyStroke)value;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32KeyStrokeManager.SendKeyStroke(GetKeyboardKeyFrom(request.m_keyboardKey), User32PressionType.Press);
            });
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs((int)(request.m_timeInSecondBetweenStroke / 1000f), () => {
                User32KeyStrokeManager.SendKeyStroke(GetKeyboardKeyFrom(request.m_keyboardKey), User32PressionType.Release);
            }); ;
        }
        else if (value is ZhuLi_WindowMouseKeyPression) { }

        succedToTranslate = false;
    }

    private User32KeyboardStrokeCodeEnum GetClipboardKeyFor(ZhuLi_WindowCopyPast.CopyPastType copyType)
    {
        switch (copyType)
        {
            case ZhuLi_WindowCopyPast.CopyPastType.Copy:return User32KeyboardStrokeCodeEnum.KEY_C;
            case ZhuLi_WindowCopyPast.CopyPastType.Cut: return User32KeyboardStrokeCodeEnum.KEY_X;
            case ZhuLi_WindowCopyPast.CopyPastType.Past:return User32KeyboardStrokeCodeEnum.KEY_V;
        }
        throw new Exception("Shoul not be reach");
    }

        private User32KeyboardStrokeCodeEnum GetKeyboardKeyFrom(ZhuLiEnum.KeyboardWindowMAppKey key)
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


            case ZhuLiEnum.KeyboardWindowMAppKey.Back: return User32KeyboardStrokeCodeEnum.BROWSER_BACK;
            case ZhuLiEnum.KeyboardWindowMAppKey.Forward: return User32KeyboardStrokeCodeEnum.BROWSER_FORWARD;
            case ZhuLiEnum.KeyboardWindowMAppKey.Refresh: return User32KeyboardStrokeCodeEnum.BROWSER_REFRESH;
            case ZhuLiEnum.KeyboardWindowMAppKey.Stop: return User32KeyboardStrokeCodeEnum.BROWSER_STOP;
            case ZhuLiEnum.KeyboardWindowMAppKey.Search: return User32KeyboardStrokeCodeEnum.BROWSER_SEARCH;
            case ZhuLiEnum.KeyboardWindowMAppKey.Favourites: return User32KeyboardStrokeCodeEnum.BROWSER_FAVORITES;
            case ZhuLiEnum.KeyboardWindowMAppKey.WebHome: return User32KeyboardStrokeCodeEnum.BROWSER_HOME;
            case ZhuLiEnum.KeyboardWindowMAppKey.Mutevolume: return User32KeyboardStrokeCodeEnum.VOLUME_MUTE;
            case ZhuLiEnum.KeyboardWindowMAppKey.Mail: return User32KeyboardStrokeCodeEnum.LAUNCH_MAIL;
            case ZhuLiEnum.KeyboardWindowMAppKey.Media: return User32KeyboardStrokeCodeEnum.LAUNCH_MEDIA_SELECT;
            case ZhuLiEnum.KeyboardWindowMAppKey.LowerVolume: return User32KeyboardStrokeCodeEnum.VOLUME_DOWN;
            case ZhuLiEnum.KeyboardWindowMAppKey.RaiseVolume: return User32KeyboardStrokeCodeEnum.VOLUME_UP;
            case ZhuLiEnum.KeyboardWindowMAppKey.ForwardMail: return User32KeyboardStrokeCodeEnum.BROWSER_FORWARD;
          
            default: throw new NotImplementedException("A key is not implemented: "+key);
        }
    }


    private User32KeyboardStrokeCodeEnum GetKeyboardKeyFrom(ZhuLiEnum.KeyboardStableKey m_keyboardKey)
    {
        switch (m_keyboardKey)
        {

            case ZhuLiEnum.KeyboardStableKey.A: return User32KeyboardStrokeCodeEnum.KEY_A;
            case ZhuLiEnum.KeyboardStableKey.B: return User32KeyboardStrokeCodeEnum.KEY_B;
            case ZhuLiEnum.KeyboardStableKey.C: return User32KeyboardStrokeCodeEnum.KEY_C;
            case ZhuLiEnum.KeyboardStableKey.D: return User32KeyboardStrokeCodeEnum.KEY_D;
            case ZhuLiEnum.KeyboardStableKey.E: return User32KeyboardStrokeCodeEnum.KEY_E;
            case ZhuLiEnum.KeyboardStableKey.F: return User32KeyboardStrokeCodeEnum.KEY_F;
            case ZhuLiEnum.KeyboardStableKey.G: return User32KeyboardStrokeCodeEnum.KEY_G;
            case ZhuLiEnum.KeyboardStableKey.H: return User32KeyboardStrokeCodeEnum.KEY_H;
            case ZhuLiEnum.KeyboardStableKey.I: return User32KeyboardStrokeCodeEnum.KEY_I;
            case ZhuLiEnum.KeyboardStableKey.J: return User32KeyboardStrokeCodeEnum.KEY_J;
            case ZhuLiEnum.KeyboardStableKey.K: return User32KeyboardStrokeCodeEnum.KEY_K;
            case ZhuLiEnum.KeyboardStableKey.L: return User32KeyboardStrokeCodeEnum.KEY_L;
            case ZhuLiEnum.KeyboardStableKey.M: return User32KeyboardStrokeCodeEnum.KEY_M;
            case ZhuLiEnum.KeyboardStableKey.N: return User32KeyboardStrokeCodeEnum.KEY_N;
            case ZhuLiEnum.KeyboardStableKey.O: return User32KeyboardStrokeCodeEnum.KEY_O;
            case ZhuLiEnum.KeyboardStableKey.P: return User32KeyboardStrokeCodeEnum.KEY_P;
            case ZhuLiEnum.KeyboardStableKey.Q: return User32KeyboardStrokeCodeEnum.KEY_Q;
            case ZhuLiEnum.KeyboardStableKey.R: return User32KeyboardStrokeCodeEnum.KEY_R;
            case ZhuLiEnum.KeyboardStableKey.S: return User32KeyboardStrokeCodeEnum.KEY_S;
            case ZhuLiEnum.KeyboardStableKey.T: return User32KeyboardStrokeCodeEnum.KEY_T;
            case ZhuLiEnum.KeyboardStableKey.U: return User32KeyboardStrokeCodeEnum.KEY_U;
            case ZhuLiEnum.KeyboardStableKey.V: return User32KeyboardStrokeCodeEnum.KEY_V;
            case ZhuLiEnum.KeyboardStableKey.W: return User32KeyboardStrokeCodeEnum.KEY_W;
            case ZhuLiEnum.KeyboardStableKey.X: return User32KeyboardStrokeCodeEnum.KEY_X;
            case ZhuLiEnum.KeyboardStableKey.Y: return User32KeyboardStrokeCodeEnum.KEY_Y;
            case ZhuLiEnum.KeyboardStableKey.Z: return User32KeyboardStrokeCodeEnum.KEY_Z;
            case ZhuLiEnum.KeyboardStableKey._0: return User32KeyboardStrokeCodeEnum.KEY_0;
            case ZhuLiEnum.KeyboardStableKey._1: return User32KeyboardStrokeCodeEnum.KEY_1;
            case ZhuLiEnum.KeyboardStableKey._2: return User32KeyboardStrokeCodeEnum.KEY_2;
            case ZhuLiEnum.KeyboardStableKey._3: return User32KeyboardStrokeCodeEnum.KEY_3;
            case ZhuLiEnum.KeyboardStableKey._4: return User32KeyboardStrokeCodeEnum.KEY_4;
            case ZhuLiEnum.KeyboardStableKey._5: return User32KeyboardStrokeCodeEnum.KEY_5;
            case ZhuLiEnum.KeyboardStableKey._6: return User32KeyboardStrokeCodeEnum.KEY_6;
            case ZhuLiEnum.KeyboardStableKey._7: return User32KeyboardStrokeCodeEnum.KEY_7;
            case ZhuLiEnum.KeyboardStableKey._8: return User32KeyboardStrokeCodeEnum.KEY_8;
            case ZhuLiEnum.KeyboardStableKey._9: return User32KeyboardStrokeCodeEnum.KEY_9;
            case ZhuLiEnum.KeyboardStableKey.NP0: return User32KeyboardStrokeCodeEnum.NUMPAD0;
            case ZhuLiEnum.KeyboardStableKey.NP1: return User32KeyboardStrokeCodeEnum.NUMPAD1;
            case ZhuLiEnum.KeyboardStableKey.NP2: return User32KeyboardStrokeCodeEnum.NUMPAD2;
            case ZhuLiEnum.KeyboardStableKey.NP3: return User32KeyboardStrokeCodeEnum.NUMPAD3;
            case ZhuLiEnum.KeyboardStableKey.NP4: return User32KeyboardStrokeCodeEnum.NUMPAD4;
            case ZhuLiEnum.KeyboardStableKey.NP5: return User32KeyboardStrokeCodeEnum.NUMPAD5;
            case ZhuLiEnum.KeyboardStableKey.NP6: return User32KeyboardStrokeCodeEnum.NUMPAD6;
            case ZhuLiEnum.KeyboardStableKey.NP7: return User32KeyboardStrokeCodeEnum.NUMPAD7;
            case ZhuLiEnum.KeyboardStableKey.NP8: return User32KeyboardStrokeCodeEnum.NUMPAD8;
            case ZhuLiEnum.KeyboardStableKey.NP9: return User32KeyboardStrokeCodeEnum.NUMPAD9;
            case ZhuLiEnum.KeyboardStableKey.F1: return User32KeyboardStrokeCodeEnum.F1;
            case ZhuLiEnum.KeyboardStableKey.F2: return User32KeyboardStrokeCodeEnum.F2;
            case ZhuLiEnum.KeyboardStableKey.F3: return User32KeyboardStrokeCodeEnum.F3;
            case ZhuLiEnum.KeyboardStableKey.F4: return User32KeyboardStrokeCodeEnum.F4;
            case ZhuLiEnum.KeyboardStableKey.F5: return User32KeyboardStrokeCodeEnum.F5;
            case ZhuLiEnum.KeyboardStableKey.F6: return User32KeyboardStrokeCodeEnum.F6;
            case ZhuLiEnum.KeyboardStableKey.F7: return User32KeyboardStrokeCodeEnum.F7;
            case ZhuLiEnum.KeyboardStableKey.F8: return User32KeyboardStrokeCodeEnum.F8;
            case ZhuLiEnum.KeyboardStableKey.F9: return User32KeyboardStrokeCodeEnum.F9;
            case ZhuLiEnum.KeyboardStableKey.F10: return User32KeyboardStrokeCodeEnum.F10;
            case ZhuLiEnum.KeyboardStableKey.F11: return User32KeyboardStrokeCodeEnum.F11;
            case ZhuLiEnum.KeyboardStableKey.F12: return User32KeyboardStrokeCodeEnum.F12;
            case ZhuLiEnum.KeyboardStableKey.F13: return User32KeyboardStrokeCodeEnum.F13;
            case ZhuLiEnum.KeyboardStableKey.F14: return User32KeyboardStrokeCodeEnum.F14;
            case ZhuLiEnum.KeyboardStableKey.F15: return User32KeyboardStrokeCodeEnum.F15;
            case ZhuLiEnum.KeyboardStableKey.F16: return User32KeyboardStrokeCodeEnum.F16;
            case ZhuLiEnum.KeyboardStableKey.F17: return User32KeyboardStrokeCodeEnum.F17;
            case ZhuLiEnum.KeyboardStableKey.F18: return User32KeyboardStrokeCodeEnum.F18;
            case ZhuLiEnum.KeyboardStableKey.F19: return User32KeyboardStrokeCodeEnum.F19;
            case ZhuLiEnum.KeyboardStableKey.F20: return User32KeyboardStrokeCodeEnum.F20;
            case ZhuLiEnum.KeyboardStableKey.F21: return User32KeyboardStrokeCodeEnum.F21;
            case ZhuLiEnum.KeyboardStableKey.F22: return User32KeyboardStrokeCodeEnum.F22;
            case ZhuLiEnum.KeyboardStableKey.F23: return User32KeyboardStrokeCodeEnum.F23;
            case ZhuLiEnum.KeyboardStableKey.F24: return User32KeyboardStrokeCodeEnum.F24;
            case ZhuLiEnum.KeyboardStableKey.NPAdd: return User32KeyboardStrokeCodeEnum.ADD;
            case ZhuLiEnum.KeyboardStableKey.NPSub: return User32KeyboardStrokeCodeEnum.SUBTRACT;
            case ZhuLiEnum.KeyboardStableKey.NPMul: return User32KeyboardStrokeCodeEnum.MULTIPLY;
            case ZhuLiEnum.KeyboardStableKey.NPDiv: return User32KeyboardStrokeCodeEnum.DIVIDE;
            case ZhuLiEnum.KeyboardStableKey.NPDot: return User32KeyboardStrokeCodeEnum.DECIMAL;
            case ZhuLiEnum.KeyboardStableKey.NPEnter: return User32KeyboardStrokeCodeEnum.ENTER;
            case ZhuLiEnum.KeyboardStableKey.ArrowLeft: return User32KeyboardStrokeCodeEnum.LEFT;
            case ZhuLiEnum.KeyboardStableKey.ArrowRight: return User32KeyboardStrokeCodeEnum.RIGHT;
            case ZhuLiEnum.KeyboardStableKey.ArrowDown: return User32KeyboardStrokeCodeEnum.DOWN;
            case ZhuLiEnum.KeyboardStableKey.ArrowUp: return User32KeyboardStrokeCodeEnum.UP;
            case ZhuLiEnum.KeyboardStableKey.Tab: return User32KeyboardStrokeCodeEnum.TAB;
            case ZhuLiEnum.KeyboardStableKey.Enter: return User32KeyboardStrokeCodeEnum.ENTER;
            case ZhuLiEnum.KeyboardStableKey.Backspace: return User32KeyboardStrokeCodeEnum.BACKSPACE;
            case ZhuLiEnum.KeyboardStableKey.ShiftLeft: return User32KeyboardStrokeCodeEnum.LSHIFT;
            case ZhuLiEnum.KeyboardStableKey.ShiftRight: return User32KeyboardStrokeCodeEnum.RSHIFT;
            case ZhuLiEnum.KeyboardStableKey.CtrlLeft: return User32KeyboardStrokeCodeEnum.LCONTROL;
            case ZhuLiEnum.KeyboardStableKey.CtrlRight: return User32KeyboardStrokeCodeEnum.RCONTROL;
            case ZhuLiEnum.KeyboardStableKey.Altleft: return User32KeyboardStrokeCodeEnum.LMENU;
            case ZhuLiEnum.KeyboardStableKey.AltRight: return User32KeyboardStrokeCodeEnum.RMENU;
            case ZhuLiEnum.KeyboardStableKey.ShiftLock: return User32KeyboardStrokeCodeEnum.CAPS_LOCK;
            case ZhuLiEnum.KeyboardStableKey.NumLock: return User32KeyboardStrokeCodeEnum.NUMLOCK;
            case ZhuLiEnum.KeyboardStableKey.PlatformButtonLeft: return User32KeyboardStrokeCodeEnum.LWIN;
            case ZhuLiEnum.KeyboardStableKey.PlatformButtonRight: return User32KeyboardStrokeCodeEnum.RWIN;
            case ZhuLiEnum.KeyboardStableKey.RightMenu: return User32KeyboardStrokeCodeEnum.APPSMENU;
            case ZhuLiEnum.KeyboardStableKey.PrintScreen: return User32KeyboardStrokeCodeEnum.SNAPSHOT;
            case ZhuLiEnum.KeyboardStableKey.Insert: return User32KeyboardStrokeCodeEnum.INSERT;
            case ZhuLiEnum.KeyboardStableKey.Delete: return User32KeyboardStrokeCodeEnum.DELETE;
            case ZhuLiEnum.KeyboardStableKey.Escape: return User32KeyboardStrokeCodeEnum.ESC;
            case ZhuLiEnum.KeyboardStableKey.PageDown: return User32KeyboardStrokeCodeEnum.PAGEDOWN;
            case ZhuLiEnum.KeyboardStableKey.PageUp: return User32KeyboardStrokeCodeEnum.PAGE_UP;
            case ZhuLiEnum.KeyboardStableKey.End: return User32KeyboardStrokeCodeEnum.END;
            case ZhuLiEnum.KeyboardStableKey.Home: return User32KeyboardStrokeCodeEnum.HOME;
            case ZhuLiEnum.KeyboardStableKey.Space: return User32KeyboardStrokeCodeEnum.SPACE_BAR;
            case ZhuLiEnum.KeyboardStableKey.Pause: return User32KeyboardStrokeCodeEnum.PAUSE;
            case ZhuLiEnum.KeyboardStableKey.VolumeDown: return User32KeyboardStrokeCodeEnum.VOLUME_DOWN;
            case ZhuLiEnum.KeyboardStableKey.VolumeUp: return User32KeyboardStrokeCodeEnum.VOLUME_UP;
            case ZhuLiEnum.KeyboardStableKey.VolumeMute: return User32KeyboardStrokeCodeEnum.VOLUME_MUTE;
            case ZhuLiEnum.KeyboardStableKey.MediaStop: return User32KeyboardStrokeCodeEnum.MEDIA_STOP;
            case ZhuLiEnum.KeyboardStableKey.MediaToggle: return User32KeyboardStrokeCodeEnum.MEDIA_PLAY_PAUSE;
            case ZhuLiEnum.KeyboardStableKey.MediaPrevious: return User32KeyboardStrokeCodeEnum.MEDIA_PREV_TRACK;
            case ZhuLiEnum.KeyboardStableKey.MediaNext: return User32KeyboardStrokeCodeEnum.MEDIA_NEXT_TRACK;
            case ZhuLiEnum.KeyboardStableKey.Print: return User32KeyboardStrokeCodeEnum.PRINT;
            case ZhuLiEnum.KeyboardStableKey.Sleep: return User32KeyboardStrokeCodeEnum.SLEEP;
            case ZhuLiEnum.KeyboardStableKey.OEM_1: return User32KeyboardStrokeCodeEnum.OEM_1;
            case ZhuLiEnum.KeyboardStableKey.OEM_2: return User32KeyboardStrokeCodeEnum.OEM_2;
            case ZhuLiEnum.KeyboardStableKey.OEM_3: return User32KeyboardStrokeCodeEnum.OEM_3;
            case ZhuLiEnum.KeyboardStableKey.OEM_4: return User32KeyboardStrokeCodeEnum.OEM_4;
            case ZhuLiEnum.KeyboardStableKey.OEM_5: return User32KeyboardStrokeCodeEnum.OEM_5;
            case ZhuLiEnum.KeyboardStableKey.OEM_6: return User32KeyboardStrokeCodeEnum.OEM_6;
            case ZhuLiEnum.KeyboardStableKey.OEM_7: return User32KeyboardStrokeCodeEnum.OEM_7;
            case ZhuLiEnum.KeyboardStableKey.OEM_8: return User32KeyboardStrokeCodeEnum.OEM_8;
            case ZhuLiEnum.KeyboardStableKey.OEM_102: return User32KeyboardStrokeCodeEnum.OEM_102;
            case ZhuLiEnum.KeyboardStableKey.OEM_PLUS: return User32KeyboardStrokeCodeEnum.OEM_PLUS;
            case ZhuLiEnum.KeyboardStableKey.OEM_COMMA: return User32KeyboardStrokeCodeEnum.OEM_COMMA;
            case ZhuLiEnum.KeyboardStableKey.OEM_MINUS: return User32KeyboardStrokeCodeEnum.OEM_MINUS;
            case ZhuLiEnum.KeyboardStableKey.OEM_PERIOD: return User32KeyboardStrokeCodeEnum.OEM_PERIOD;
            case ZhuLiEnum.KeyboardStableKey.VK_OEM_CLEAR: return User32KeyboardStrokeCodeEnum.OEM_CLEAR;
            default: throw new NotImplementedException("A key had been add and is not implemented");
        }
    }

    private User32PressionType Parse(ZhuLiEnum.User32PressType m_keyPressionType)
    {
        if (m_keyPressionType == ZhuLiEnum.User32PressType.Press) return User32PressionType.Press;
        else return User32PressionType.Release;
    }
    
}