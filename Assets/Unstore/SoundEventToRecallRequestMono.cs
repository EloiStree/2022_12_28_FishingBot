using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEventToRecallRequestMono : MonoBehaviour
{
    public FishermenTimeHolderMono m_fishermenTimeHolder;
    public List<RecallRequestWithArrivedTime> m_fishermanRecallIndexQueue;
    public ThreadQueueDateTimeCallMono m_threadQueueForRecallOnly;
    public float m_maxTimeToRecover = 5;
    public float m_maxTimeToFish = 20;
    public Eloi.PrimitiveUnityEvent_Int m_requestRecall;
    public Eloi.PrimitiveUnityEvent_Int m_requestToRecastFishWithoutRecall;
    public void ReceivedIndexOfFishermanSound(int index) {

        m_fishermanRecallIndexQueue.Add(new RecallRequestWithArrivedTime(index));

    }

    public bool m_needToWaitPreviousToFinish;
    public int m_inQueueCount;
    private void Update()
    {
        if (m_needToWaitPreviousToFinish)
        {
            m_inQueueCount = m_threadQueueForRecallOnly.GetInQueueCount();
            if (m_inQueueCount <= 0)
            {
                SendNextRequest();
            }
        }
        else { 
            SendNextRequest();
        }
    }

    private void SendNextRequest()
    {
        if (m_fishermanRecallIndexQueue.Count > 0) {
            RecallRequestWithArrivedTime next = m_fishermanRecallIndexQueue[0];
            m_fishermanRecallIndexQueue.RemoveAt(0);
            if ((DateTime.Now - next.m_whenReceived).TotalSeconds < m_maxTimeToRecover)
            {
                m_requestRecall.Invoke(next.m_fishermanIndex);
                m_fishermenTimeHolder.NotifyAsFishLaneSentFromNow(next.m_fishermanIndex, m_maxTimeToFish);
            }
            else
            {
                m_requestToRecastFishWithoutRecall.Invoke(next.m_fishermanIndex);
            }
        }
       
    }
    [System.Serializable]
    public class RecallRequestWithArrivedTime {
        public int m_fishermanIndex;
        public DateTime m_whenReceived;

        public RecallRequestWithArrivedTime(int fishermanIndex)
        {
            m_fishermanIndex = fishermanIndex;
            m_whenReceived = DateTime.Now;
        }
    }


    //public void NotifyFishLaneSentFromFishermenIndex(int index)
    //{
    //    m_fishermenTimeHolder.GetAtIndex(index, out FishermanEventTimes fisherman);
    //    DateTime now = DateTime.Now;
    //    if (fisherman.m_lastTimeFishDetected.GetAsDate().AddSeconds(m_ignoreSoundTimeInSeconds) > now) {
    //        m_onFishLaneSent.Invoke(index);
    //        m_fishermenTimeHolder.NotifyAsFishLaneSentFromNow(index,20);
    //    }
    //}

    //public bool IsFishLaneOutdated(int index) {
    //    return m_fishermenTimeHolder.IsFishingLaneOutdatedFromNow(index);
    //}
    //DateTime m_previous = DateTime.Now;
    //DateTime m_now = DateTime.Now;
    //public void Update()
    //{
    //    if ((m_previous - DateTime.Now).Milliseconds > 10)
    //    {
    //        m_previous = m_now;
    //        m_now = DateTime.Now;

    //        for (int i = 0; i < m_fishermenTimeHolder.GetFishermanCount(); i++)
    //        {
    //            m_fishermenTimeHolder.GetAtIndex(i, out FishermanEventTimes fisherman);
    //            DateTime outdated = fisherman.m_outdatedFishLaneTimeEstimation.GetAsDate();
    //            if (outdated < m_previous && outdated <= m_now)
    //            {
    //                m_onFishLaneOutdatedDetected.Invoke(i);
    //            }
    //        }
    //    }


    //}
}
