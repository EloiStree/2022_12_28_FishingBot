using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing_AttemptCount : MonoBehaviour
{
    public int m_attempt;
    public Eloi.PrimitiveUnityEvent_Int m_onAttemptChanged;

    public bool m_keepBetweenSession=true;
    public void Increment(int value = 1) { 
        m_attempt++;
        m_onAttemptChanged.Invoke(m_attempt);
    }
    public void ResetCounter(int value = 0) { 
        m_attempt = value; 
        m_onAttemptChanged.Invoke(m_attempt);
    }
    public string m_playerPrefsId = "FishingCount";
    public void OnApplicationQuit()
    {
        if(m_keepBetweenSession)
        PlayerPrefs.SetInt(m_playerPrefsId, m_attempt);

    }
    public void OnDisable()
    {
        if (m_keepBetweenSession)
            PlayerPrefs.SetInt(m_playerPrefsId, m_attempt);

    }
    private void OnEnable()
    {
        if (m_keepBetweenSession)
            ResetCounter(PlayerPrefs.GetInt(m_playerPrefsId));
    }
}
