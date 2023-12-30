using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using static User32RelativePointsActionPusher;

public class TestIfItIsFishOrUserThatBut : MonoBehaviour
{
    public int m_processId;
    public IntPtrWrapGet m_processIdInteface;
    public DeductedInfoOfWindowSizeWithSource m_rectInfo;
    public float m_marginFromDown;
    public float m_marginFromRight;
    public int m_clickPositionXR;
    public int m_clickPositionYR;
    public int m_clickPositionXA;
    public int m_clickPositionYA;

    public float m_timeBetweenPing= 10f;
    private void Start()
    {
        InvokeRepeating("PushPingKeyAndMouse",m_timeBetweenPing, m_timeBetweenPing);
    }

    public void PushPingKeyAndMouse()
    {
        m_processIdInteface = IntPtrProcessId.Int(m_processId);
        FetchWindowInfoUtility.Get(m_processIdInteface, out m_rectInfo);
        m_rectInfo.m_frameSize.GetAbsoluteFromRelativePixelRight2Left((int)m_marginFromDown, out  m_clickPositionXA);
        m_rectInfo.m_frameSize.GetAbsoluteFromRelativePixelTop2Bot((int)m_marginFromRight, out m_clickPositionYA);

        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(100, () => {
            WindowIntPtrUtility.SetForegroundWindow(m_processId);
        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(110, () => {
            MouseOperations.SetCursorPosition(m_clickPositionXA, m_clickPositionYA);
        });

        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(140, () => {
            MouseOperations.MouseEventWithCurrentPosition(MouseOperations.MouseEventFlags.LeftDown);
        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(190, () => {
            MouseOperations.MouseEventWithCurrentPosition(MouseOperations.MouseEventFlags.LeftUp);
        });

        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(240, () =>
        {
            SendKeyMessageToWindows.SendKeyDown(User32PostMessageKeyEnum.VK_C, m_processIdInteface, true);
        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(280, () =>
        {
            SendKeyMessageToWindows.SendKeyUp(User32PostMessageKeyEnum.VK_C, m_processIdInteface, true);
        }); ThreadQueueDateTimeCall.Instance.AddFromNowInMs(440, () =>
        {
            SendKeyMessageToWindows.SendKeyDown(User32PostMessageKeyEnum.VK_C, m_processIdInteface, true);
        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(480, () =>
        {
            SendKeyMessageToWindows.SendKeyUp(User32PostMessageKeyEnum.VK_C, m_processIdInteface, true);
        });
    }

    private readonly static object threadLock = new object();
   
    
}

