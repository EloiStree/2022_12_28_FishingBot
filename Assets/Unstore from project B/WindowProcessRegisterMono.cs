using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface WindowProcessRegister
{
    public void SaveFocusWindowAsAlias(in string windowAlias);
    public void SaveWindowAsAlias(in string windowAlias, in IntPtrWrapGet pointer);
    public void GetWindowPointer(in string windowAlias,out bool found, out IntPtrWrapGet pointer);

}




public class WindowProcessRegisterMono : MonoBehaviour, WindowProcessRegister
{
    public Dictionary<string, AliasToProcessId> m_aliasToId = new Dictionary<string, AliasToProcessId>();


    public class AliasToProcessId {
        public string m_alias;
        public int m_pointerIntId;
        public IntPtrWrapGet m_pointer;

        public AliasToProcessId(string alias)
        {
            m_alias = alias;
            SetPointer(null);
        }

        public AliasToProcessId(string alias, IntPtrWrapGet pointer) : this(alias)
        {
            SetPointer(pointer);
        }

        public void SetPointer(IntPtrWrapGet pointer) {
            if (pointer == null) {
                m_pointer = null; 
                m_pointerIntId = -1;
            }
            m_pointerIntId = pointer.GetAsInt();
            m_pointer = pointer;
        }
    }


    public void SaveFocusWindowAsAlias(in string windowAlias)
    {
        WindowIntPtrUtility.GetCurrentProcessId(out IntPtrWrapGet pt);
        SaveWindowAsAlias(in windowAlias, in pt);
    }


    public void SaveWindowAsAlias(in string windowAlias, in IntPtrWrapGet pointer)
    {
        if (m_aliasToId.ContainsKey(windowAlias))
        {
            m_aliasToId[windowAlias].SetPointer( pointer ) ;
        }
        else m_aliasToId.Add(windowAlias, new AliasToProcessId(windowAlias, pointer));
    }

    public void GetWindowPointer(in string windowAlias,out bool found, out IntPtrWrapGet pointer)
    {
        found = m_aliasToId.ContainsKey(windowAlias);
        if (found)
        {
            pointer = m_aliasToId[windowAlias].m_pointer;
        }
        else pointer = null;
    }
}



public abstract class SubProcessAccesFromMono
{
    public ProcessesAccessMono processesAcess;
    public List<WindowIntPtrUtility.ProcessInformation> m_observedProcess = new List<WindowIntPtrUtility.ProcessInformation>();


    public void RefreshObservedProcess() {
        processesAcess = ProcessesAccessInScene.Instance;
    }

    public abstract void ScanForValideProcessToAccess(out List<WindowIntPtrUtility.ProcessInformation> processObserved);
}


public class ScanAllWindowProcessScannerMono : SubProcessAccesFromMono
{
    public override void ScanForValideProcessToAccess(out List<WindowIntPtrUtility.ProcessInformation> processObserved)
    {
        throw new System.NotImplementedException();
    }
}