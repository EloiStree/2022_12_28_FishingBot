using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fishing_ClockActionMono : MonoBehaviour
{

    public UnityEvent m_atStart;
    public UnityEvent m_every1Minute;
    public UnityEvent m_every15Minutes;
    public UnityEvent m_every30Minutes;
    public UnityEvent m_every60Minutes;
    public int m_tickCound=1;
    public Coroutine m_previousCorutine;
    
    public void ResetClock()
    {
        if (m_previousCorutine != null) StopCoroutine(m_previousCorutine);
        m_previousCorutine = StartCoroutine(ClockCoroutine());
    }
    public IEnumerator ClockCoroutine()
    {
        m_atStart.Invoke();
        while (true)
        {

            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(60);
            AddTick();
        }
    }

    private void AddTick()
    {
        m_every1Minute.Invoke();
        if (m_tickCound % 15 == 0)
            m_every15Minutes.Invoke();
        if (m_tickCound % 30 == 0)
            m_every15Minutes.Invoke();
        if (m_tickCound % 60 == 0)
            m_every15Minutes.Invoke();
        m_tickCound++;
    }

}
