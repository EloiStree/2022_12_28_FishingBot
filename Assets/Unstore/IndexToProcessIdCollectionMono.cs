using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexToProcessIdCollectionMono : MonoBehaviour
{
    public IndexToProcessIdCollection m_indexToProcessId =new IndexToProcessIdCollection();

    public void SetProcessIdOf(in int index, in int processId)
    {

        m_indexToProcessId.SetProcessIdOf(in index, in processId);
    }

    public void GetProcessIdOf(in int index, out int processId)
    {
        m_indexToProcessId.GetProcessIdOf(in index, out  processId);
    }

    public int GetCount()
    {
       return  m_indexToProcessId.GetCount();
    }

    public bool HasProcess(int index)
    {
        return index < GetCount() && m_indexToProcessId.GetProcessIdOf(index) > 0;
    }
}

[System.Serializable]
public class IndexToProcessIdCollection
{
   public List<int> m_processIds = new List<int>();

    public void SetProcessIdOf(in int index, in int processId) {

        CheckIfNeedOfMoreFishermen(index);
        m_processIds[index] = processId;
    }

    public void GetProcessIdOf(in int index, out int processId)
    {
        CheckIfNeedOfMoreFishermen(index);
        processId = m_processIds[index];
    }
    public int GetProcessIdOf(in int index)
    {
        CheckIfNeedOfMoreFishermen(index);
        return  m_processIds[index];
    }



    private void CheckIfNeedOfMoreFishermen(int index)
    {
        while (m_processIds.Count <= index)
            m_processIds.Add(0);
    }

    public int GetCount()
    {
        return m_processIds.Count;
    }
}
