using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryToAutoCompleteProcessWowProcess : MonoBehaviour
{
    public InputField [] m_processInputField;

   

    [ContextMenu("Fetch process list")]
    public void RefreshListOfProcesses()
    {
        for (int i = 0; i < m_processInputField.Length; i++)
        {

            ProcessesAccessInScene.Instance.FetchProcessInfoBasedOnIndex("Wow", i, out bool found, out TargetParentProcessIntPtr pointer);
            if (found)
            {

                WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(IntPtrProcessId.Int(pointer.m_proccessId), out bool childFound, out IntPtrWrapGet t);
                if (childFound)
                {
                    m_processInputField[i].text = "" + t.GetAsInt();
                }else
                m_processInputField[i].text = "";

            }
            else m_processInputField[i].text = "";
        }

    }

}
