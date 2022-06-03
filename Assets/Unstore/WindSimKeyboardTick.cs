using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindSimKeyboardTick : MonoBehaviour
{
    public KeyboardKeyListener m_keyListener;

    public KeyboardTouch m_keyboard;
    public UnityEvent m_down;
    public UnityEvent m_up;
    public bool m_useUpdate;
    public UnityEvent m_update;
    public DefaultBooleanChangeListener m_isTouch = new DefaultBooleanChangeListener() ;
    void Update()
    {
        
        m_isTouch.SetBoolean(m_keyListener.IsTouchActive(m_keyboard), out bool hasChanged);

        if (hasChanged) {
            if (m_isTouch.GetBoolean())
                m_down.Invoke();
            else
                m_up.Invoke();
        }
          
        if (m_useUpdate)
            if (m_isTouch.GetBoolean())
                m_update.Invoke();
    }

}
