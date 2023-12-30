using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FishermenActionToThreadMono : MonoBehaviour
{
    public IndexToProcessIdCollectionMono m_indexToProcess;
    public Eloi.PrimitiveUnityEvent_Int m_castFishLane;
    public Eloi.PrimitiveUnityEvent_Int m_castRecallAndFishLane;
    public int m_indexReceived;
    public int m_processId;
    public void CastFishingOfFishermanIndex(int index)
    {
        m_indexToProcess.GetProcessIdOf(index, out int processId);
        m_indexReceived = index;
        m_processId = processId;
        m_castFishLane.Invoke((processId));
    }
    public void CastRecallOfFishermanIndex(int index)
    {
        m_indexToProcess.GetProcessIdOf(index, out int processId);
        m_indexReceived = index;
        m_processId = processId;
        m_castRecallAndFishLane.Invoke(processId);
    }
   
}
