using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sleepy_ZhuLi : MonoBehaviour
{
    public int [] m_processesId;
    public float m_timeBetforeStart=5;
    public float m_secondsBetweenPress=1;
    public float m_secondsBetweenKeys=2;
    public int m_jumpCount;

    public int m_jumpIndex;

    public ZhuLiEnum.KeyboardStableKey[] list;
    IEnumerator Start()
    {

        while (true) {
            yield return new WaitForEndOfFrame();
            //Eloi.E_EnumUtility.GetAllEnumOf(out ZhuLiEnum.KeyboardStableKey[] list);

            ZhuLiStruct.User32.ProcessesArrayOfId targets = new ZhuLiStruct.User32.ProcessesArrayOfId()
            {
                m_processesId = m_processesId
            };
            foreach (var item in list)
            {
                Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = item, m_keyPressionType = ZhuLiEnum.User32PressType.Press, m_processesToApplyTo = targets });
                yield return new WaitForSeconds(m_secondsBetweenPress);
                Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = item, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });
                yield return new WaitForSeconds(m_secondsBetweenKeys);
              
            }
            Eloi.E_UnityRandomUtility.GetRandomOf(out int rp, m_processesId);
            m_jumpCount++;
            if (m_jumpCount%20 == 0)
            {
                m_jumpIndex++;
       
               targets = new ZhuLiStruct.User32.ProcessesArrayOfId()
                {
                    m_processesId = new int[] { m_processesId[m_jumpIndex % m_processesId.Length] }
                };
                Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = ZhuLiEnum.KeyboardStableKey.Space, m_keyPressionType = ZhuLiEnum.User32PressType.Press, m_processesToApplyTo = targets });
                yield return new WaitForSeconds(m_secondsBetweenPress);
                Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = ZhuLiEnum.KeyboardStableKey.Space, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });

            }

        }
        //yield return new WaitForSeconds(m_timeBetforeStart);

        //Eloi.E_EnumUtility.GetAllEnumOf(out ZhuLiEnum.KeyboardStableKey[] list);
      
        //ZhuLiStruct.User32.ProcessesArrayOfId targets = new ZhuLiStruct.User32.ProcessesArrayOfId() { 
        //    m_processesId = m_processesId
        //};
        //foreach (var item in list)
        //{
        //    Execute(new ZhuLi_FrameStableKeyPression() {m_keyboardKey = item, m_keyPressionType= ZhuLiEnum.User32PressType.Press,m_processesToApplyTo= targets });
        //    yield return new WaitForSeconds(m_secondsBetweenPress);
        //    Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = item, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });
        //    yield return new WaitForSeconds(m_secondsBetweenKeys);
        //}
    }
    IZhuLiCommand[] cmds = new IZhuLiCommand[] {
     new  ZhuLi_WindowMouseKeyPression          ()
    , new ZhuLi_WindowMouseStroke              ()
    , new ZhuLi_CurrentFrameMouseKeyPression   ()
    , new ZhuLi_CurrentFrameMouseStroke        ()
    , new ZhuLi_FrameMouseKeyPression          ()
    , new ZhuLi_FrameMouseStroke               ()
    , new ZhuLi_WindowMouseMoveMainWindowAxes  ()
    , new ZhuLi_WindowMouseMoveMainWindowAxis  ()
    , new ZhuLi_CurrentFrameMouseMoveAxes      ()
    , new ZhuLi_CurrentFrameMouseMoveAxis      ()
    , new ZhuLi_FrameMouseMoveAxes        ()
    , new ZhuLi_FrameMouseMoveAxis             ()
    , new ZhuLi_WindowMouseScoll               ()
    , new ZhuLi_ToParse_WindowKeyPress         ()
    , new ZhuLi_ToParse_WindowKeyRelease       ()
    , new ZhuLi_ToParse_WindowKeyStroke        ()
    , new ZhuLi_WindowAppKeyPression           ()
    , new ZhuLi_WindowAppKeyStroke             ()
    , new ZhuLi_WindowKeyStablePression        ()
    , new ZhuLi_WindowKeyStableStroke          ()
    , new ZhuLi_FrameStableKeyPression         ()
    , new ZhuLi_FrameStableKeyStroke           ()
    , new ZhuLi_CurrentFrameStablePression  ()
    , new ZhuLi_CurrentFrameStableKeyStroke    ()
    };

    public void ToTest() {
      

    }




 public void Execute ( ZhuLi_WindowMouseKeyPression             request){}
 public void Execute ( ZhuLi_WindowMouseStroke                  request){}
 public void Execute ( ZhuLi_CurrentFrameMouseKeyPression       request){}
 public void Execute ( ZhuLi_CurrentFrameMouseStroke            request){}
 public void Execute ( ZhuLi_FrameMouseKeyPression              request){}
 public void Execute ( ZhuLi_FrameMouseStroke                   request){}
 public void Execute ( ZhuLi_WindowMouseMoveMainWindowAxes      request){}
 public void Execute ( ZhuLi_WindowMouseMoveMainWindowAxis      request){}
 public void Execute ( ZhuLi_CurrentFrameMouseMoveAxes          request){}
 public void Execute ( ZhuLi_CurrentFrameMouseMoveAxis          request){}
 public void Execute ( ZhuLi_FrameMouseMoveAxes            request){}
 public void Execute ( ZhuLi_FrameMouseMoveAxis                 request){}
 public void Execute ( ZhuLi_WindowMouseScoll                   request){}
 public void Execute ( ZhuLi_ToParse_WindowKeyPress             request){}
 public void Execute ( ZhuLi_ToParse_WindowKeyRelease           request){}
 public void Execute ( ZhuLi_ToParse_WindowKeyStroke            request){}
 public void Execute ( ZhuLi_WindowAppKeyPression               request){}
 public void Execute ( ZhuLi_WindowAppKeyStroke                 request){}
 public void Execute ( ZhuLi_WindowKeyStablePression            request){}
 public void Execute ( ZhuLi_WindowKeyStableStroke              request){}
 public void Execute ( ZhuLi_FrameStableKeyPression             request){

        if (request.m_keyPressionType == ZhuLiEnum.User32PressType.Press)
        {

            foreach (var item in request.m_processesToApplyTo.m_processesId)
            {
                SendKeyMessageToWindows.SendKeyDown(User32ZhuliConverter.ConvertPostKey(request.m_keyboardKey)
                    , IntPtrProcessId.Int(item), true);
               // Debug.Log("Received:" + request.m_keyboardKey+"  -  "+ User32ZhuliConverter.ConvertPostKey(request.m_keyboardKey));
            }
        }
        else
        {
            foreach (var item in request.m_processesToApplyTo.m_processesId)
            {
                SendKeyMessageToWindows.SendKeyUp(User32ZhuliConverter.ConvertPostKey(request.m_keyboardKey)
                    , IntPtrProcessId.Int(item), true);
            }
        }

    }

   
    public void Execute ( ZhuLi_FrameStableKeyStroke               request){}
 public void Execute ( ZhuLi_CurrentFrameStablePression      request){}
    public void Execute(ZhuLi_CurrentFrameStableKeyStroke request) { }

}





public class User32ZhuliConverter {

    public static void ConvertPostKey(ZhuLiEnum.KeyboardStableKey givenKey, out User32PostMessageKeyEnum keyFound) {
        keyFound = ConvertPostKey(givenKey);
    }



    public static User32PostMessageKeyEnum ConvertPostKey(ZhuLiEnum.KeyboardStableKey m_keyboardKey)
    {
        switch (m_keyboardKey)
        {
            case ZhuLiEnum.KeyboardStableKey.A: return User32PostMessageKeyEnum.VK_A;
            case ZhuLiEnum.KeyboardStableKey.B: return User32PostMessageKeyEnum.VK_B;
            case ZhuLiEnum.KeyboardStableKey.C: return User32PostMessageKeyEnum.VK_C;
            case ZhuLiEnum.KeyboardStableKey.D: return User32PostMessageKeyEnum.VK_D;
            case ZhuLiEnum.KeyboardStableKey.E: return User32PostMessageKeyEnum.VK_E;
            case ZhuLiEnum.KeyboardStableKey.F: return User32PostMessageKeyEnum.VK_F;
            case ZhuLiEnum.KeyboardStableKey.G: return User32PostMessageKeyEnum.VK_G;
            case ZhuLiEnum.KeyboardStableKey.H: return User32PostMessageKeyEnum.VK_H;
            case ZhuLiEnum.KeyboardStableKey.I: return User32PostMessageKeyEnum.VK_I;
            case ZhuLiEnum.KeyboardStableKey.J: return User32PostMessageKeyEnum.VK_J;
            case ZhuLiEnum.KeyboardStableKey.K: return User32PostMessageKeyEnum.VK_K;
            case ZhuLiEnum.KeyboardStableKey.L: return User32PostMessageKeyEnum.VK_L;
            case ZhuLiEnum.KeyboardStableKey.M: return User32PostMessageKeyEnum.VK_M;
            case ZhuLiEnum.KeyboardStableKey.N: return User32PostMessageKeyEnum.VK_N;
            case ZhuLiEnum.KeyboardStableKey.O: return User32PostMessageKeyEnum.VK_O;
            case ZhuLiEnum.KeyboardStableKey.P: return User32PostMessageKeyEnum.VK_P;
            case ZhuLiEnum.KeyboardStableKey.Q: return User32PostMessageKeyEnum.VK_Q;
            case ZhuLiEnum.KeyboardStableKey.R: return User32PostMessageKeyEnum.VK_R;
            case ZhuLiEnum.KeyboardStableKey.S: return User32PostMessageKeyEnum.VK_S;
            case ZhuLiEnum.KeyboardStableKey.T: return User32PostMessageKeyEnum.VK_T;
            case ZhuLiEnum.KeyboardStableKey.U: return User32PostMessageKeyEnum.VK_U;
            case ZhuLiEnum.KeyboardStableKey.V: return User32PostMessageKeyEnum.VK_V;
            case ZhuLiEnum.KeyboardStableKey.W: return User32PostMessageKeyEnum.VK_W;
            case ZhuLiEnum.KeyboardStableKey.X: return User32PostMessageKeyEnum.VK_X;
            case ZhuLiEnum.KeyboardStableKey.Y: return User32PostMessageKeyEnum.VK_Y;
            case ZhuLiEnum.KeyboardStableKey.Z: return User32PostMessageKeyEnum.VK_Z;
            case ZhuLiEnum.KeyboardStableKey._0: return User32PostMessageKeyEnum.VK_0;
            case ZhuLiEnum.KeyboardStableKey._1: return User32PostMessageKeyEnum.VK_1;
            case ZhuLiEnum.KeyboardStableKey._2: return User32PostMessageKeyEnum.VK_2;
            case ZhuLiEnum.KeyboardStableKey._3: return User32PostMessageKeyEnum.VK_3;
            case ZhuLiEnum.KeyboardStableKey._4: return User32PostMessageKeyEnum.VK_4;
            case ZhuLiEnum.KeyboardStableKey._5: return User32PostMessageKeyEnum.VK_5;
            case ZhuLiEnum.KeyboardStableKey._6: return User32PostMessageKeyEnum.VK_6;
            case ZhuLiEnum.KeyboardStableKey._7: return User32PostMessageKeyEnum.VK_7;
            case ZhuLiEnum.KeyboardStableKey._8: return User32PostMessageKeyEnum.VK_8;
            case ZhuLiEnum.KeyboardStableKey._9: return User32PostMessageKeyEnum.VK_9;
            case ZhuLiEnum.KeyboardStableKey.NP0: return User32PostMessageKeyEnum.VK_NUMPAD0;
            case ZhuLiEnum.KeyboardStableKey.NP1: return User32PostMessageKeyEnum.VK_NUMPAD1;
            case ZhuLiEnum.KeyboardStableKey.NP2: return User32PostMessageKeyEnum.VK_NUMPAD2;
            case ZhuLiEnum.KeyboardStableKey.NP3: return User32PostMessageKeyEnum.VK_NUMPAD3;
            case ZhuLiEnum.KeyboardStableKey.NP4: return User32PostMessageKeyEnum.VK_NUMPAD4;
            case ZhuLiEnum.KeyboardStableKey.NP5: return User32PostMessageKeyEnum.VK_NUMPAD5;
            case ZhuLiEnum.KeyboardStableKey.NP6: return User32PostMessageKeyEnum.VK_NUMPAD6;
            case ZhuLiEnum.KeyboardStableKey.NP7: return User32PostMessageKeyEnum.VK_NUMPAD7;
            case ZhuLiEnum.KeyboardStableKey.NP8: return User32PostMessageKeyEnum.VK_NUMPAD8;
            case ZhuLiEnum.KeyboardStableKey.NP9: return User32PostMessageKeyEnum.VK_NUMPAD9;
            case ZhuLiEnum.KeyboardStableKey.F1: return User32PostMessageKeyEnum.VK_F1;
            case ZhuLiEnum.KeyboardStableKey.F2: return User32PostMessageKeyEnum.VK_F2;
            case ZhuLiEnum.KeyboardStableKey.F3: return User32PostMessageKeyEnum.VK_F3;
            case ZhuLiEnum.KeyboardStableKey.F4: return User32PostMessageKeyEnum.VK_F4;
            case ZhuLiEnum.KeyboardStableKey.F5: return User32PostMessageKeyEnum.VK_F5;
            case ZhuLiEnum.KeyboardStableKey.F6: return User32PostMessageKeyEnum.VK_F6;
            case ZhuLiEnum.KeyboardStableKey.F7: return User32PostMessageKeyEnum.VK_F7;
            case ZhuLiEnum.KeyboardStableKey.F8: return User32PostMessageKeyEnum.VK_F8;
            case ZhuLiEnum.KeyboardStableKey.F9: return User32PostMessageKeyEnum.VK_F9;
            case ZhuLiEnum.KeyboardStableKey.F10: return User32PostMessageKeyEnum.VK_F10;
            case ZhuLiEnum.KeyboardStableKey.F11: return User32PostMessageKeyEnum.VK_F11;
            case ZhuLiEnum.KeyboardStableKey.F12: return User32PostMessageKeyEnum.VK_F12;
            case ZhuLiEnum.KeyboardStableKey.F13: return User32PostMessageKeyEnum.VK_F13;
            case ZhuLiEnum.KeyboardStableKey.F14: return User32PostMessageKeyEnum.VK_F14;
            case ZhuLiEnum.KeyboardStableKey.F15: return User32PostMessageKeyEnum.VK_F15;
            case ZhuLiEnum.KeyboardStableKey.F16: return User32PostMessageKeyEnum.VK_F16;
            case ZhuLiEnum.KeyboardStableKey.F17: return User32PostMessageKeyEnum.VK_F17;
            case ZhuLiEnum.KeyboardStableKey.F18: return User32PostMessageKeyEnum.VK_F18;
            case ZhuLiEnum.KeyboardStableKey.F19: return User32PostMessageKeyEnum.VK_F19;
            case ZhuLiEnum.KeyboardStableKey.F20: return User32PostMessageKeyEnum.VK_F20;
            case ZhuLiEnum.KeyboardStableKey.F21: return User32PostMessageKeyEnum.VK_F21;
            case ZhuLiEnum.KeyboardStableKey.F22: return User32PostMessageKeyEnum.VK_F22;
            case ZhuLiEnum.KeyboardStableKey.F23: return User32PostMessageKeyEnum.VK_F23;

            case ZhuLiEnum.KeyboardStableKey.F24: return User32PostMessageKeyEnum.VK_F24;
            case ZhuLiEnum.KeyboardStableKey.NPAdd: return User32PostMessageKeyEnum.VK_ADD;
            case ZhuLiEnum.KeyboardStableKey.NPSub: return User32PostMessageKeyEnum.VK_SUBTRACT;
            case ZhuLiEnum.KeyboardStableKey.NPMul: return User32PostMessageKeyEnum.VK_MULTIPLY;
            case ZhuLiEnum.KeyboardStableKey.NPDiv: return User32PostMessageKeyEnum.VK_DIVIDE;
            case ZhuLiEnum.KeyboardStableKey.NPDot: return User32PostMessageKeyEnum.VK_DECIMAL;
            case ZhuLiEnum.KeyboardStableKey.NPEnter: return User32PostMessageKeyEnum.VK_RETURN;
            case ZhuLiEnum.KeyboardStableKey.ArrowLeft: return User32PostMessageKeyEnum.VK_LEFT;
            case ZhuLiEnum.KeyboardStableKey.ArrowRight: return User32PostMessageKeyEnum.VK_RIGHT;
            case ZhuLiEnum.KeyboardStableKey.ArrowDown: return User32PostMessageKeyEnum.VK_DOWN;
            case ZhuLiEnum.KeyboardStableKey.ArrowUp: return User32PostMessageKeyEnum.VK_UP;
            case ZhuLiEnum.KeyboardStableKey.Tab: return User32PostMessageKeyEnum.VK_TAB;
            case ZhuLiEnum.KeyboardStableKey.Enter: return User32PostMessageKeyEnum.VK_RETURN;
            case ZhuLiEnum.KeyboardStableKey.Backspace: return User32PostMessageKeyEnum.VK_BACK;
            case ZhuLiEnum.KeyboardStableKey.ShiftLeft: return User32PostMessageKeyEnum.VK_LSHIFT;
            case ZhuLiEnum.KeyboardStableKey.ShiftRight: return User32PostMessageKeyEnum.VK_RSHIFT;
            case ZhuLiEnum.KeyboardStableKey.CtrlLeft: return User32PostMessageKeyEnum.VK_LCONTROL;
            case ZhuLiEnum.KeyboardStableKey.CtrlRight: return User32PostMessageKeyEnum.VK_RCONTROL;
            case ZhuLiEnum.KeyboardStableKey.Altleft: return User32PostMessageKeyEnum.VK_LMENU;
            case ZhuLiEnum.KeyboardStableKey.AltRight: return User32PostMessageKeyEnum.VK_RMENU;
            case ZhuLiEnum.KeyboardStableKey.ShiftLock: return User32PostMessageKeyEnum.VK_CAPITAL;
            case ZhuLiEnum.KeyboardStableKey.NumLock: return User32PostMessageKeyEnum.VK_NUMLOCK;
            case ZhuLiEnum.KeyboardStableKey.PlatformButtonLeft: return User32PostMessageKeyEnum.VK_LWIN;
            case ZhuLiEnum.KeyboardStableKey.PlatformButtonRight: return User32PostMessageKeyEnum.VK_RWIN;
            case ZhuLiEnum.KeyboardStableKey.RightMenu: return User32PostMessageKeyEnum.VK_APPS;
            case ZhuLiEnum.KeyboardStableKey.PrintScreen: return User32PostMessageKeyEnum.VK_SNAPSHOT;
            case ZhuLiEnum.KeyboardStableKey.Insert: return User32PostMessageKeyEnum.VK_INSERT;
            case ZhuLiEnum.KeyboardStableKey.Delete: return User32PostMessageKeyEnum.VK_DELETE;
            case ZhuLiEnum.KeyboardStableKey.Escape: return User32PostMessageKeyEnum.VK_ESCAPE;
            case ZhuLiEnum.KeyboardStableKey.PageDown: return User32PostMessageKeyEnum.VK_NEXT;
            case ZhuLiEnum.KeyboardStableKey.PageUp: return User32PostMessageKeyEnum.VK_PRIOR;
            case ZhuLiEnum.KeyboardStableKey.End: return User32PostMessageKeyEnum.VK_END;
            case ZhuLiEnum.KeyboardStableKey.Home: return User32PostMessageKeyEnum.VK_HOME;
            case ZhuLiEnum.KeyboardStableKey.Space: return User32PostMessageKeyEnum.VK_SPACE;
            case ZhuLiEnum.KeyboardStableKey.Pause: return User32PostMessageKeyEnum.VK_PAUSE;
            case ZhuLiEnum.KeyboardStableKey.VolumeDown: return User32PostMessageKeyEnum.VK_VOLUME_DOWN;
            case ZhuLiEnum.KeyboardStableKey.VolumeUp: return User32PostMessageKeyEnum.VK_VOLUME_UP;
            case ZhuLiEnum.KeyboardStableKey.VolumeMute: return User32PostMessageKeyEnum.VK_VOLUME_MUTE;
            case ZhuLiEnum.KeyboardStableKey.MediaStop: return User32PostMessageKeyEnum.VK_MEDIA_STOP;
            case ZhuLiEnum.KeyboardStableKey.MediaToggle: return User32PostMessageKeyEnum.VK_MEDIA_PLAY_PAUSE;
            case ZhuLiEnum.KeyboardStableKey.MediaPrevious: return User32PostMessageKeyEnum.VK_MEDIA_PREV_TRACK;
            case ZhuLiEnum.KeyboardStableKey.MediaNext: return User32PostMessageKeyEnum.VK_MEDIA_NEXT_TRACK;
            case ZhuLiEnum.KeyboardStableKey.Sleep: return User32PostMessageKeyEnum.VK_SLEEP;
            case ZhuLiEnum.KeyboardStableKey.Print: return User32PostMessageKeyEnum.VK_PRINT;
            default: return User32PostMessageKeyEnum.VK_SCROLL;

        }
    }


}
























