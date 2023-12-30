using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DisplayCurrentPreviousProcessid : MonoBehaviour
{
    public ProcessesAccessMono m_processAccess;
    public ProcessesFocusHistoryMono m_processFocusInScene;

    public int m_currentGivenId;
    public int m_previousGivenId;

    public int m_currentParentId;
    public Eloi.PrimitiveUnityEvent_Int m_currentParent;
    public int m_currentDisplayChildId;
    public Eloi.PrimitiveUnityEvent_Int m_currentDisplayChild;

    public int m_previousParentId;
    public Eloi.PrimitiveUnityEvent_Int m_previousParent;
    public int m_previousDisplayChildId;
    public Eloi.PrimitiveUnityEvent_Int m_previousDisplayChild;

    private void Awake()
    {
        if (m_processAccess == null)
        {
            m_processAccess = ProcessesAccessInScene.Instance;
        }
        if (m_processFocusInScene == null)
        {
            m_processFocusInScene = ProcessesFocusHistoryInScene.Instance;
        }
        m_processFocusInScene.AddNewProcessDetected(SetNewFocusProcess);
    }
    public void SetNewFocusProcess(int processid)
    {


        if (processid != m_currentGivenId)
        {
            m_previousGivenId = m_currentGivenId;
            m_currentGivenId = processid;

            m_previousDisplayChildId = m_currentDisplayChildId;
            m_previousParentId = m_currentParentId;
            IntPtrWrapGet given = IntPtrProcessId.Int(m_currentGivenId);
            m_processAccess.GetParentOf(given, out bool foundParent, out IntPtrWrapGet parent);
            m_processAccess.GetChildrenThatDisplay(given, out bool foundChildren, out IntPtrWrapGet child);

            m_currentParentId = parent.GetAsInt();
            m_currentDisplayChildId = child.GetAsInt();

            m_currentParent.Invoke(m_currentParentId);
            m_currentDisplayChild.Invoke(m_currentDisplayChildId);
            m_previousParent.Invoke(m_previousParentId);
            m_previousDisplayChild.Invoke(m_previousDisplayChildId);

        }




    }
}
