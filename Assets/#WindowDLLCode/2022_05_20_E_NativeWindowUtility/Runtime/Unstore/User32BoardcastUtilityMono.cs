using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User32BoardcastUtilityMono : MonoBehaviour
{
}

public class User32BoardcastUtilityToThread {
    public static void SendKey(IntPtrWrapGet activeProcessId,
      User32PostMessageKeyEnum keyToSend)
    {
        Send(activeProcessId, keyToSend, User32PressionType.Press);
        Send(activeProcessId, keyToSend, User32PressionType.Release);
    }
    public static void SendKeyDown(IntPtrWrapGet activeProcessId,
         User32PostMessageKeyEnum keyToSend)
    {
        Send(activeProcessId, keyToSend, User32PressionType.Press);
    }
    public static void SendKeyUp(IntPtrWrapGet activeProcessId,
        User32PostMessageKeyEnum keyToSend)
    {
        Send(activeProcessId, keyToSend, User32PressionType.Release);
    }



    public static void Send(IntPtrWrapGet activeProcessId,
        User32PostMessageKeyEnum keyToSend,
        User32PressionType pressType)
    {
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
        {
            TargetChildrenProcessIntPtr p = new TargetChildrenProcessIntPtr();
            p.SetAsInt(activeProcessId.GetAsInt());
            User32KeyStrokeManager.SendKeyPostMessage(p,
               keyToSend, pressType);
        });
    }
    public static void Send(int activeProcessId,
       User32KeyboardStrokeCodeEnum keyToSend,
       User32PressionType pressType) => Send(activeProcessId, keyToSend, pressType, 1);
    public static void Send(int activeProcessId,
        User32KeyboardStrokeCodeEnum keyToSend,
        User32PressionType pressType,
        int timeToFocusInMs=150)
    {
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
        {
            WindowIntPtrUtility.SetForegroundWindow(activeProcessId);
        }); 
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(timeToFocusInMs, () =>
        {
            User32KeyStrokeManager.SendKeyStroke(keyToSend, pressType);
        });
    }

    public static void HeavyTryParseAndSendToProcesses( string processNameId, User32PostMessageKeyEnum whatToCast)
    {
       
            ProcessesAccessInScene.Instance.FetchListOfProcessesBasedOnName(processNameId,
                out GroupOfProcessesParentToChildrens info, false);

            for (int i = 0; i < info.m_processesAndChildrens.Count; i++)
            {
                WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(
                    info.m_processesAndChildrens[i].m_parent,
                    out bool foundchild, out IntPtrWrapGet target);
                TryParseAndSendToProcess(target, whatToCast);
            }
        
    }
    public static void HeavyTryParseAndSendToProcesses(string processNameId, string whatToCast)
    {
        StringToUser32PostMessageKeyEnum.Get(whatToCast, out bool found,
               out User32PostMessageKeyEnum tocast);
        if (found) {
            HeavyTryParseAndSendToProcesses(processNameId, tocast);
        }
    }


    public static void TryParseAndSendToProcess(IntPtrWrapGet activeProcessId, string whatToCast)
    {
        StringToUser32PostMessageKeyEnum.Get(whatToCast, out bool found,
               out User32PostMessageKeyEnum tocast);
        if (found)
        {
            TryParseAndSendToProcess(activeProcessId, tocast);
        }
    }
    public static void TryParseAndSendToProcess(IntPtrWrapGet activeProcessId, User32PostMessageKeyEnum whatToCast)
    {
        
            User32BoardcastUtilityToThread.Send(activeProcessId, whatToCast, User32PressionType.Press);
            User32BoardcastUtilityToThread.Send(activeProcessId, whatToCast, User32PressionType.Release);
        
    }

    public static IEnumerator Coroutine_CopyPastChatTextToProcesses(string processNameId, string chatText, float timeBetweenPush)
    {

        ProcessesAccessInScene.Instance.FetchListOfProcessesBasedOnName(processNameId,
            out GroupOfProcessesParentToChildrens info, false);

        for (int i = 0; i < info.m_processesAndChildrens.Count; i++)
        {
            WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(
                info.m_processesAndChildrens[i].m_parent,
                out bool foundchild, out IntPtrWrapGet target);
            CopyPastChatText(target, chatText);
            yield return new WaitForSeconds(timeBetweenPush);
        }

    }

    public static void CopyPastChatText(IntPtrWrapGet [] processesId, string text,
        int pressEnterMS, int pressCtrlVMS, int timeBetweenCopyKeyMS, int pressBacksMS, int validateMS,
        User32PostMessageKeyEnum returnKey = User32PostMessageKeyEnum.VK_RETURN, int startTimeInMS = 0 
         )
    {

        int time = startTimeInMS;
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + 0, () =>
        {
            User32ClipboardUtility.CopyTextToClipboard(text, false);
        });


        foreach (var t in processesId)
        {

            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + pressEnterMS, () =>
        {
            User32KeyStrokeManager.SendKeyPostMessage(t,
               returnKey,
               User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
                returnKey,
                User32PressionType.Release
                );

        });

            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + pressCtrlVMS, () =>
            {

                User32KeyStrokeManager.SendKeyPostMessage(t,
                      User32PostMessageKeyEnum.VK_LCONTROL,
                      User32PressionType.Press);

                User32KeyStrokeManager.SendKeyPostMessage(t,
                   User32PostMessageKeyEnum.VK_V,
                   User32PressionType.Press);
                User32KeyStrokeManager.SendKeyPostMessage(t,
                    User32PostMessageKeyEnum.VK_V,
                    User32PressionType.Release
                    );
                User32KeyStrokeManager.SendKeyPostMessage(t,
                    User32PostMessageKeyEnum.VK_LCONTROL,
                    User32PressionType.Release
                    );
            }); 
            //ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + pressCtrlVMS, () =>
            //{

            //});
            //ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + pressCtrlVMS + timeBetweenCopyKeyMS, () =>
            //{
                
            //});
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + pressBacksMS, () =>
            {

                User32KeyStrokeManager.SendKeyPostMessage(t,
                     User32PostMessageKeyEnum.VK_BACK,
                     User32PressionType.Press);
                User32KeyStrokeManager.SendKeyPostMessage(t,
                     User32PostMessageKeyEnum.VK_BACK,
                     User32PressionType.Release);
            }); 
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + pressBacksMS+100, () =>
            {

                User32KeyStrokeManager.SendKeyPostMessage(t,
                     User32PostMessageKeyEnum.VK_BACK,
                     User32PressionType.Press);
                User32KeyStrokeManager.SendKeyPostMessage(t,
                     User32PostMessageKeyEnum.VK_BACK,
                     User32PressionType.Release);
            });
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + validateMS, () =>
            {
                User32KeyStrokeManager.SendKeyPostMessage(t,
                    User32PostMessageKeyEnum.VK_RETURN,
                    User32PressionType.Press);
                User32KeyStrokeManager.SendKeyPostMessage(t,
                    User32PostMessageKeyEnum.VK_RETURN,
                    User32PressionType.Release
                    );
            });
            
            
        }


    }
    public static void CopyPastChatText(IntPtrWrapGet processId, string text, User32PostMessageKeyEnum returnKey =User32PostMessageKeyEnum.VK_RETURN, int startTimeInMS=0)
    {
        IntPtrTemp t ;
        try
        {
             t = new IntPtrTemp(processId);
        }
        catch (Exception e) { Debug.Log("Error:" + e.StackTrace); return; }

        int time = startTimeInMS;
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time+0, () =>
        {
            WindowIntPtrUtility.SetForegroundWindow(t);
        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + 80, () =>
        {
            User32KeyStrokeManager.SendKeyPostMessage(t,
               returnKey,
               User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
                returnKey,
                User32PressionType.Release
                );

        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + 150, () =>
        {
            User32ClipboardUtility.CopyTextToClipboard(text,false);
        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + 250, () =>
        {

            User32KeyStrokeManager.SendKeyPostMessage(t,
                  User32PostMessageKeyEnum.VK_LCONTROL,
                  User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
               User32PostMessageKeyEnum.VK_V,
               User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_V,
                User32PressionType.Release
                );
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_LCONTROL,
                User32PressionType.Release
                );
        }); 
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(time + 300, () =>
        {
            User32KeyStrokeManager.SendKeyPostMessage(t,
                   User32PostMessageKeyEnum.VK_BACK,
                   User32PressionType.Press); 
            User32KeyStrokeManager.SendKeyPostMessage(t,
                 User32PostMessageKeyEnum.VK_BACK,
                 User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_RETURN,
                User32PressionType.Press); 
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_RETURN,
                User32PressionType.Release
                );
        });

    }
}