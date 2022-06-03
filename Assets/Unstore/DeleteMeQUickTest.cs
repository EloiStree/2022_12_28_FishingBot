using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeleteMeQUickTest : MonoBehaviour
{
    public List<WindowIntPtrUtility.ProcessInformation> m_allProcess = new List<WindowIntPtrUtility.ProcessInformation>();
    public List<WindowIntPtrUtility.ProcessInformation> m_processFound = new List<WindowIntPtrUtility.ProcessInformation>();

    public int m_idTarget;
    public List<int> m_ids;
    public WindowIntPtrUtility.ProcessInformation m_test;



    public string m_whatToLookForProcessName = "Wow";

    void Start()
    {
        RefreshListOfProcess();

    }

    [ContextMenu("Refresh")]
    private void RefreshListOfProcess()
    {
        WindowIntPtrUtility.FetchAllProcesses(out m_allProcess);
        WindowIntPtrUtility.FrechWindowWithExactProcessNameFrom(in m_whatToLookForProcessName, in m_allProcess, out m_processFound);

        m_test = new WindowIntPtrUtility.ProcessInformation();
        m_ids = WindowIntPtrUtility.GetProcessIdChildrenWindows(IntPtrTemp.Int(m_idTarget))
        .Select(k=>k.GetAsInt()).ToList();
   
        for (int i = 0; i < m_allProcess.Count; i++)
        {
            if (m_allProcess[i].m_processId == m_idTarget) {
                m_test = m_allProcess[i];
                return;
            }
        }
        //for (int i = 0; i < m_ids.Count; i++)
        //{
        //    WindowIntPtrUtility.GetWindowThreadProcessId(
        // m_ids[i],out WindowIntPtrUtility.ProcessInformation p);
        //}
      


    }

    public int m_xToTest=10;
    public int m_yToTest=10;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Test");

            for (int i = 0; i < m_processFound.Count; i++)
            {
               // SendKeyToWindow.RequestPastAction(m_processFound[i].m_processId);
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            m_xToTest = (int)Input.mousePosition.x;
            m_yToTest = (int)Input.mousePosition.y;
            for (int i = 0; i < m_processFound.Count; i++)
            {
                //SendKeyToWindow.SendMouseLeftDown(m_processFound[i].m_processId, m_xToTest, m_yToTest);
                //SendKeyToWindow.SendMouseLeftUp(m_processFound[i].m_processId, m_xToTest, m_yToTest);
                SendKeyMessageToWindows.SendMouseRightDown(IntPtrTemp.Int( m_processFound[i].m_processId), m_xToTest, m_yToTest);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            m_xToTest = (int)Input.mousePosition.x;
            m_yToTest = (int)Input.mousePosition.y;
            for (int i = 0; i < m_processFound.Count; i++)
            {
                //SendKeyToWindow.SendMouseLeftDown(m_processFound[i].m_processId, m_xToTest, m_yToTest);
                //SendKeyToWindow.SendMouseLeftUp(m_processFound[i].m_processId, m_xToTest, m_yToTest);
                SendKeyMessageToWindows.SendMouseRightUp(IntPtrTemp.Int(m_processFound[i].m_processId), m_xToTest, m_yToTest);
            }

        }

    }
}
