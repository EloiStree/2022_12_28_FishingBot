using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Win32;

public class WindowListMono : MonoBehaviour
{
    public WindowInformation m_wowFirst;
    public WindowInformation[] m_allWow;
    public List<WindowInformation> m_windowListExtended;


    [ContextMenu("Refresh")]
    public void Refresh(){
        m_windowListExtended = WindowList.GetAllWindowsExtendedInfo();
        m_wowFirst = m_windowListExtended.Find(
                           w => w.Caption.StartsWith("World of warcraft")
                       );


    }
}
