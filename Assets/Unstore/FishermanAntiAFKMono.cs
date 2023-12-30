using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanAntiAFKMono : MonoBehaviour
{
    public IndexToProcessIdCollectionMono m_fishermenProcessId;
    public FishermenTimeHolderMono m_fishermenTimeHolder;
    public float m_maxTimeSilence=60;
    public Eloi.PrimitiveUnityEvent_Int m_onRecastLaneOfFishermanIndex;
    public List<int> m_afkIndex = new List<int>();
    void Update()
    {
        m_afkIndex.Clear();
        for (int i = 0; i < m_fishermenTimeHolder.GetFishermanCount(); i++)
        {
            if (m_fishermenProcessId.HasProcess(i)) { 
                m_fishermenTimeHolder.GetAtIndex(i, out FishermanEventTimes fisherman);
                if ((DateTime.Now - fisherman.m_lastTimeFishDetected.GetAsDate()).TotalSeconds > m_maxTimeSilence) {
                    m_onRecastLaneOfFishermanIndex.Invoke(i);
                    m_afkIndex.Add(i);
                }
            }
        }
    }
}
