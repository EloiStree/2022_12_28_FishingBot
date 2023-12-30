using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class FilterSoundIrregularityWithMuteMono : MonoBehaviour
{

    public FishermenTimeHolderMono m_fishermenTimeHolder;
    public float m_ignoreSoundTimeInSeconds = 5;

    public void NotifySoundReceivedFromFishermenIndex(int index)
    {
        m_fishermenTimeHolder.GetAtIndex(index, out FishermanEventTimes fisherman);
        DateTime now = DateTime.Now;
        if (fisherman.m_lastTimeFishDetected.GetAsDate().AddSeconds(m_ignoreSoundTimeInSeconds) < now) {
            m_onFishSoundDetected.Invoke(index);
            m_fishermenTimeHolder.NotifyAsFishDetectedFromNow(index);
        }
    }
   

    public Eloi.PrimitiveUnityEvent_Int m_onFishSoundDetected;
}
