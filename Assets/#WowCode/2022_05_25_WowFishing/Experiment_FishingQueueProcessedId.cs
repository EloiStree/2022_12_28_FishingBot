using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_FishingQueueProcessedId : MonoBehaviour
{
    public List<MaxTimeToProcess> m_processIdQueue = new List<MaxTimeToProcess>();
    public ThreadQueueDateTimeCallMono m_threadQueue;
    public Test_FishingPostMessageClickMono m_fishPostMessage;
    public FishermenTimeHolderMono m_fishermenTimeHolder;

    public float m_maxTimeToProcess = 5f;
    [System.Serializable]
    public class MaxTimeToProcess {
        public DateTime m_enteredAt;
        public int m_processId;
        public MaxTimeToProcess(int processId) {
            m_enteredAt = DateTime.Now;
            m_processId = processId;
        }
        public bool HasStillTime(DateTime now, in float maxTimeToCache)
        {
            double timePast = (now- m_enteredAt).TotalSeconds;
            return (timePast < maxTimeToCache);

        }
        public bool HasStillTimeNow( in float maxTimeToCache)
        {
            return HasStillTime(DateTime.Now, in maxTimeToCache);
        }

        public IntPtrWrapGet GetAsParent()
        {
            return IntPtrTemp.Int(m_processId);
        }
    }
    public void AddProcessToDoActionOn(int processId) {
        m_processIdQueue.Add(new MaxTimeToProcess(processId));
    }

    private void Update()
    {
        if (m_threadQueue == null)
            m_threadQueue = ThreadQueueDateTimeCall.Instance;
        if (m_threadQueue == null)
            return;

        if ( m_processIdQueue.Count > 0) {
            IntPtrWrapGet  id = m_processIdQueue[0].GetAsParent();
            if (!m_processIdQueue[0].HasStillTimeNow(in m_maxTimeToProcess))
            {
                m_fishPostMessage.LaunchTheLineWithProcess(id);
            }else
            {
                m_fishPostMessage.RecovertTheLineWithProcessId(id);
            }
            m_processIdQueue.RemoveAt(0);
            //Eloi.E_DebugLog.B();
        }
    }

}
