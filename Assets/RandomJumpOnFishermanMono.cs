using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomJumpOnFishermanMono : MonoBehaviour
{
    public IndexToProcessIdCollectionMono m_fishermanProcess;


    public float m_cycleTimeInSecondes = 30;
    public float m_randomnessInSeconds = 10;
    public float m_jumpCountDown;
    public int m_index;
    public int m_processSelected;

  

    void Update()
    {
        m_jumpCountDown -= Time.deltaTime;
        if (m_jumpCountDown <= 0) {
            m_jumpCountDown = m_cycleTimeInSecondes + Random.Range(-m_jumpCountDown,m_jumpCountDown);
            PushJumpRandom();
        }
    }

    [ContextMenu("Push Random Jump")]
    public void PushJumpRandom()
    {
        int count = m_fishermanProcess.m_indexToProcessId.m_processIds.Count;
        if (count == 0) return;
        int processId = m_fishermanProcess.m_indexToProcessId.m_processIds[m_index % count];
        m_processSelected = processId;
        PushJump(processId);
        m_index++;
    }
    [ContextMenu("Push All Jump")]
    public void PushAllRandom()
    {
        foreach (int index in m_fishermanProcess.m_indexToProcessId.m_processIds)
        {
            PushJump(index);
        }
    }

    public void PushJump(int processId) {

        IntPtrWrapGet intPt= IntPtrProcessId.Int(processId);
        SendKeyMessageToWindows.SendKeyDown(User32PostMessageKeyEnum.VK_SPACE, intPt, true);
        SendKeyMessageToWindows.SendKeyUp(User32PostMessageKeyEnum.VK_SPACE, intPt, true);
    }
}
