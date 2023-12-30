using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eloi;
using UnityEngine.Events;
using System;

public class ProcessesFocusHistoryInScene : Eloi.GenericSingletonOfMono<ProcessesFocusHistoryMono> { }
public class ProcessesFocusHistoryMono : MonoBehaviour
{
    public ProcessesAccessMono m_processesAccess;
    public int m_parentId, m_displayChildrenId;
    public int m_currentId = -1;
    public int m_previousId = -1;
    public WindowIntPtrUtility.ProcessInformation m_current;
    public WindowIntPtrUtility.ProcessInformation m_previous;
    public ProcessHistory m_processHistory = new ProcessHistory();
    public Eloi.PrimitiveUnityEvent_Int m_onFrameSwitchNew;
    public Eloi.PrimitiveUnityEvent_Int m_onFrameSwitchPrevious;

    public bool m_useUpdateRefresh=true;
    public void GetCurrentProcessId(out int processId) { processId = m_currentId; }
    public void GetCurrentProcessInfo(out WindowIntPtrUtility.ProcessInformation processFocus) 
    { 
        processFocus = m_current;
    }



    [System.Serializable]
    public class ProcessHistory : GenericClampHistory<int> { }
    public void Awake()
    {
        m_processesAccess = ProcessesAccessInScene.Instance;
    }
    public void Update()
    {
        if(m_useUpdateRefresh)
            RefreshInfo();
    }
    [ContextMenu("Refresh Info")]
    public void RefreshInfo()
    {
        bool found;
        WindowIntPtrUtility.GetCurrentProcessId(out IntPtrWrapGet pt);
        m_processesAccess.GetParentOf(pt, out found, out IntPtrWrapGet c);
        m_parentId = c.GetAsInt();

        m_processesAccess.GetChildrenThatDisplay(pt, out  found, out IntPtrWrapGet p);
        if (found && p != null)
            m_displayChildrenId = p.GetAsInt();
        else m_displayChildrenId = 0;

        pt.GetAsInt(out int processId);
        if (processId > 0) { 
            if (processId != m_currentId )
            {
                m_previousId = m_currentId;
                m_currentId = processId;
                m_previous = m_current;
                m_processHistory.PushIn(processId);
                m_processesAccess.GetProcessInfoFor(pt, out  found, out WindowIntPtrUtility.ProcessInformation v);
                m_previous.m_processTitle = WindowIntPtrUtility.GetWindowText(m_previous.m_processId);
                if (found)
                {
                    m_current = v;
                    v.m_processTitle = WindowIntPtrUtility.GetWindowText(pt);
                }
                else
                {
                    m_current = new WindowIntPtrUtility.ProcessInformation();
                }
                m_onFrameSwitchNew.Invoke(m_currentId);
                m_onFrameSwitchPrevious.Invoke(m_previousId);
            }
        }
    }

    internal void AddNewProcessDetected(UnityAction<int> listener)
    {
        m_onFrameSwitchNew.AddListener(listener);
            
    }
}
