using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermenTimeHolderMono : MonoBehaviour
{

    public List<FishermanEventTimes> m_fishermen = new List<FishermanEventTimes>();
    public void NotifyAsFishDetectedFromNow( int index)
    {
        CheckIfNeedOfMoreFishermen(index);
        m_fishermen[index].NotifyAsFishDetectedFromNow();
    }

    public void NotifyAsFishLaneSentFromNow(int index, float timeToBeOutdated)
    {
        CheckIfNeedOfMoreFishermen(index);
        m_fishermen[index].NotifyAsFishLaneSentFromNow(timeToBeOutdated);
    }
    public bool IsFishingLaneOutdatedFromNow(int index)
    {
        CheckIfNeedOfMoreFishermen(index);
        return m_fishermen[index].IsFishingLaneOutdatedFromNow();
    }
    private void CheckIfNeedOfMoreFishermen(int index)
    {
        while (m_fishermen.Count <= index)
            m_fishermen.Add(new FishermanEventTimes());
    }

    public void GetAtIndex(int index, out FishermanEventTimes fisherman)
    {
        CheckIfNeedOfMoreFishermen(index);
        fisherman =  m_fishermen[index];
    }

    public int GetFishermanCount()
    {
        return m_fishermen.Count;
    }
    public void Reset()
    {
        m_fishermen.Clear();
        for (int i = 0; i < 8; i++)
        {

            m_fishermen.Add(new FishermanEventTimes());
        }
        foreach (var item in m_fishermen)
        {
            item.ResetInfoToConstructor();

        }
    }
}

[System.Serializable]

public class FishermanEventTimes {

    public Eloi.SerializableDateTime m_lastTimeFishDetected;
    public Eloi.SerializableDateTime m_lastTimeFishLaneSend;
    public Eloi.SerializableDateTime m_outdatedFishLaneTimeEstimation;

    public FishermanEventTimes()
    {
        ResetInfoToConstructor();
    }

    public void ResetInfoToConstructor()
    {
        m_lastTimeFishDetected.SetWithDate(DateTime.Now.AddSeconds(-3600));
        m_lastTimeFishLaneSend.SetWithDate(DateTime.Now.AddSeconds(-3600));
        m_outdatedFishLaneTimeEstimation.SetWithDate(DateTime.Now.AddSeconds(-3600));
    }

    public bool IsFishingLaneShouldBeActive(in DateTime now) {

        return now > m_lastTimeFishLaneSend.GetAsDate() && now < m_outdatedFishLaneTimeEstimation.GetAsDate();
    }

    public void NotifyAsFishDetectedFromNow()
    {
        NotifyAsFishDetected(DateTime.Now);
    }
    public void NotifyAsFishLaneSentFromNow(float timeToBeOutdated)
    {
        NotifyAsFishLaneSent(DateTime.Now, timeToBeOutdated);
    }
    public bool IsFishingLaneOutdatedFromNow()
    {
        return IsFishingLaneOutdated(DateTime.Now);
    }
    public void NotifyAsFishDetected(DateTime time)
    {
        m_lastTimeFishDetected.SetWithDate(time);
    }
    public void NotifyAsFishLaneSent(DateTime time, float timetoBeOutdate)
    {
        m_lastTimeFishLaneSend.SetWithDate(time);
        m_outdatedFishLaneTimeEstimation.SetWithDate(time.AddSeconds(timetoBeOutdate));
    }
 
    public bool IsFishingLaneOutdated(in DateTime now) {

        return now > m_outdatedFishLaneTimeEstimation.GetAsDate() ;
    }
}
