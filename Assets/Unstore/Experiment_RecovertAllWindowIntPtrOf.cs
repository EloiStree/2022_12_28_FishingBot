using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;
using System;
using System.Runtime.InteropServices;

public class Experiment_RecovertAllWindowIntPtrOf : MonoBehaviour
{
    public List<WindowIntPtrUtility.ProcessInformation> m_allProcess= new List<WindowIntPtrUtility.ProcessInformation>();
    public List<WindowIntPtrUtility.ProcessInformation> m_processFound = new List<WindowIntPtrUtility.ProcessInformation>();
  
    public string m_whatToLookForProcessName = "Wow";
   
    void Start()
    {
        RefreshListOfProcess();

    }

    [ContextMenu("Refresh")]
    private void RefreshListOfProcess()
    {
        WindowIntPtrUtility.FetchAllProcesses(out  m_allProcess);
        WindowIntPtrUtility.FrechWindowWithExactProcessNameFrom(in m_whatToLookForProcessName, in m_allProcess,out m_processFound);
    }
}
