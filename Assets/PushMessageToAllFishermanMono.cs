using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PushMessageToAllFishermanMono : MonoBehaviour
{
    public IndexToProcessIdCollectionMono m_fishermanProcess;
    public User32PostMessageKeyEnum m_wowCommandKey = User32PostMessageKeyEnum.VK_OEM_1;
    public int timeBetween = 0;
    public int pressEnterMS = 200;
    public int pressCtrlVMS = 300;
    public int pressBetweenMS = 10;
    public int pressBackMS = 400;
    public int validateMS = 1000;
    public string commandToPushDebug = "guild";

    [ContextMenu("Push Debug")]
    public void PushDebugCmd()
    {
        PushCommand(commandToPushDebug);
    }

    [ContextMenu("Push Macro Menu")]
    public void PushMacroMenu()
    {
        PushCommand("macro");
    }
    [ContextMenu("Push Roar")]
    public void PushRoar()
    {
        PushCommand("roar");
    }
    [ContextMenu("Push Watch")]
    public void PushWatch()
    {
        PushCommand("stopwatch");
    }

    public void PushCommand(string cmd) {
        m_fishermanProcess.GetIntPtrValide(out IntPtrWrapGet[] ptr);
        User32BoardcastUtilityToThread.CopyPastChatText(ptr, cmd, 
                    pressEnterMS, pressCtrlVMS, pressBetweenMS, pressBackMS, validateMS , m_wowCommandKey, timeBetween
            );
    }

    public void PushJump(User32PostMessageKeyEnum keyToPush)
    {
        foreach (var item in m_fishermanProcess.m_indexToProcessId.m_processIds)
        {
            if (item > 0)
            {
                IntPtrWrapGet intPt = IntPtrProcessId.Int(item);
                SendKeyMessageToWindows.SendKeyDown(keyToPush, intPt, true);
                SendKeyMessageToWindows.SendKeyUp(keyToPush, intPt, true);
            }
        }
    }
}
