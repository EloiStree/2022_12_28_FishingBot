using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Experiment_UdpMixerToFishingActions : MonoBehaviour
{
    public FisherMan [] m_fishermen;
    public Eloi.PrimitiveUnityEvent_Int m_processToDoActionOn;

    public List<string> m_messageReceivedQueue;

    public void Awake()
    {
        for (int i = 0; i < m_fishermen.Length; i++)
        {
            int index = i;
            m_fishermen[i].m_udpToAction.m_changeHappend.m_onEvent.AddListener(
              m_fishermen[i].m_muteWhenRecalling.TriggerActionIfNotMutedAndMute);

            m_fishermen[i].m_muteWhenRecalling.m_doTheThing.AddListener(
               m_fishermen[i].m_resetIfNothing.ResetTimeToZero);

            m_fishermen[i].m_resetIfNothing.m_doTheThingIfMaxTimeReach.AddListener(
                () =>
                {
                    if (m_fishermen[index].m_active)
                        PushRecallInstruction(m_fishermen[index].m_processId);
                }
                );

            m_fishermen[i].m_muteWhenRecalling.m_doTheThing.AddListener(
                () =>
                {
                    if (m_fishermen[index].m_active)
                        PushRecallInstruction(m_fishermen[index].m_processId);
                }
                );

            PushRecallInstruction(m_fishermen[i].m_processId);
        }
    }
    private void PushRecallInstruction(int processId)
    {

        NotifyAsUsed(processId);
        m_processToDoActionOn.Invoke(processId);
    }

    private void NotifyAsUsed(int processId)
    {
        for (int i = 0; i < m_fishermen.Length; i++)
        {
            if (m_fishermen[i].m_processId == processId)
            {
                m_fishermen[i].m_muteWhenRecalling.Mute();
                m_fishermen[i].m_resetIfNothing.ResetTimeToZero();
            }
        }
    }

    public void Update()
    {
        if (m_messageReceivedQueue.Count > 0)
        {
            foreach (var item in m_messageReceivedQueue)
            {
                for (int i = 0; i < m_fishermen.Length; i++)
                {
                    m_fishermen[i].m_udpToAction.TryToPush(in item);
                }
            }
            m_messageReceivedQueue.Clear();
        }

        float delta = Time.deltaTime;
        for (int i = 0; i < m_fishermen.Length; i++)
        {
            m_fishermen[i].m_muteWhenRecalling.DecreaseMuteTimeOf(delta);
            m_fishermen[i].m_resetIfNothing.AddSomeTime(delta);

            m_fishermen[i].m_resetIfNothing.CheckIfToMuchTimePastAndReset(out bool reseted);
        }
        
    }
    public void ReceivedMessage(string text)
    {
        if(this.gameObject.activeInHierarchy)
        m_messageReceivedQueue.Add(text);

    }

    private void OnEnable()
    {
        m_messageReceivedQueue.Clear();
        
    }

    [System.Serializable]
    public class FisherMan
    {
        public bool m_active=true;
        public int m_processId;
        public StringToBoolEvent m_udpToAction;
        public DoSomethingIfNothingHappened m_resetIfNothing;
        public AudioDelayMuteTrigger m_muteWhenRecalling;

       
    }


    [System.Serializable]
    public class StringToBoolEvent {

        public string m_setTrueText;
        public string m_setFalseText;
        public Eloi.PrimitiveUnityEventExtra_Bool m_changeHappend;

        public void TryToPush(in string text)
        {
            if (Eloi.E_StringUtility.AreEquals(m_setTrueText, in text, true, true))
                m_changeHappend.Invoke(true);
            if (Eloi.E_StringUtility.AreEquals(m_setFalseText, in text, true, true))
                m_changeHappend.Invoke(false);
        }
    }

    [System.Serializable]
    public class DoSomethingIfNothingHappened {

        public float m_maxTimeBeforeDoingSomething=20;
        public float m_timeCount;

        public UnityEvent  m_doTheThingIfMaxTimeReach;

        public void ResetTimeToZero()
        {
            m_timeCount = 0;
        }
        public void AddSomeTime(float timeToAddInSeconds)
        {
            m_timeCount += timeToAddInSeconds;
        }
        public void CheckIfToMuchTimePastAndReset(out bool resetCall) {
            if (m_timeCount >= m_maxTimeBeforeDoingSomething) {
                ResetTimeToZero();
                m_doTheThingIfMaxTimeReach.Invoke();
                resetCall = true;
            }
            else 
            resetCall = false;
        }

    }
    [System.Serializable]
    public class AudioDelayMuteTrigger
    {
        public float m_muteTime=5;
        public float m_mutedTimeLeft;
        public void TriggerActionIfNotMutedAndMute() {
            TriggerActionIfNotMutedAndMute(out bool t);
        }
        public void TriggerActionIfNotMutedAndMute( out bool wasSent)
        {
            if (m_mutedTimeLeft <= 0)
            {
                m_doTheThing.Invoke();
                SetMuteForSeconds(m_muteTime);
                wasSent = true;
            }
            else wasSent = false;
        }
        public void SetMuteForSeconds(float secondToMute) {
            m_mutedTimeLeft = secondToMute;
        }
        public void DecreaseMuteTimeOf(float timeInSeconds) {
            if (m_mutedTimeLeft > 0)
                m_mutedTimeLeft -= timeInSeconds;
        }

        public void Mute()
        {
            m_mutedTimeLeft = m_muteTime;
        }

        public UnityEvent m_doTheThing;
    }


    

}
