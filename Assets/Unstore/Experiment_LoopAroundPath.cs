using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Experiment_LoopAroundPath : MonoBehaviour
{

    public ListOfWowMapCoordinateLRTDOrigineScriptable m_source;
    public List<WowMapCoordinateLRTDOrigin> m_wowQueue= new List<WowMapCoordinateLRTDOrigin>();
    public Experiment_GoToTargetPoint m_target;
    public bool m_isActive;

    public int m_index;


    public void Awake()
    {
        if (m_source != null)
            m_wowQueue = m_source.m_data.m_coordinates;
        m_target.m_isArrivedListener.m_onChangedDelegate -= GoNext;
        m_target.m_isArrivedListener.m_onChangedDelegate += GoNext;
        SwitchToIndex(m_index);
    }
    public float m_pauseBeforeNext = 5f;
    public UnityEvent m_invokeGoNext;
    public UnityEvent m_readyToGoNext;
    private void GoNext(bool newValue)
    {
        if (newValue)
        {
            m_invokeGoNext.Invoke();
            Invoke("GoNext", m_pauseBeforeNext);
        }
    }
    private void GoNext()
    {
        
            m_index++;
            SwitchToIndex(m_index);
        m_readyToGoNext.Invoke();

    }

    private void SwitchToIndex(int index)
    {
        WowMapCoordinateLRTDOrigin originePoint = m_wowQueue[index % m_wowQueue.Count];
        WowMapUtility.Convert(originePoint, out WowMapCoordinateLRDTUnity unityPoint);
        m_target.SetDestination(unityPoint);
        m_target.SetAsActive(true);
    }
}
