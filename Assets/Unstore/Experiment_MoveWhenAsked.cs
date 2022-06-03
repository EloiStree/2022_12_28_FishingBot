using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_MoveWhenAsked : MonoBehaviour
{
    public KeyboardKeyListener m_keyboardListener;
    public WindowSimWriter m_keyboardWriter;

    public KeyboardTouch m_isActiveKey = KeyboardTouch.WinUS_OEM_7_Quote;
    public KeyboardTouch m_forward = KeyboardTouch.Z;
    public KeyboardTouch m_left = KeyboardTouch.Q;
    public KeyboardTouch m_right = KeyboardTouch.D;
    public KeyboardTouch m_backward = KeyboardTouch.S;
    public bool m_isActive;

    private void Awake()
    {

        m_keyboardListener.m_onKeyDown.AddListener(ListenToKeyDown);
        m_keyboardListener.m_onKeyUp.AddListener(ListenToKeyUp);
    }


    public bool m_wasActive;
    public void Update()
    {
        if (m_wasActive != m_isActive) { }
        {
            m_wasActive = m_isActive;
            if (m_isActive)
                m_keyboardWriter.RealPressDown(KeyboardTouch.Z);
            else
                m_keyboardWriter.RealPressUp(KeyboardTouch.Z);

        }
        
    }


    private void ListenToKeyDown(KeyboardTouch arg0)
    {
        if (m_isActiveKey == arg0)
            m_isActive = !m_isActive;
    }
    private void ListenToKeyUp(KeyboardTouch arg0)
    {

    }
}
