using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFrameFacadeMono : MonoBehaviour
{
    public int m_processTarget;
    public bool m_usePost;
    public int m_horizontal;
    public int m_vertical;
    public FetchWindowOfProcessBasedOnNameMono m_accessLoaded;
    public DeductedInfoOfWindowSizeWithSource m_deductedInfoTarget;
    void Update()
    {
       
    }

    [ContextMenu("Test")]
    public void Test() {

        PostMouseUtility.SendMouseLeftDown(m_processTarget,
                   m_horizontal, m_vertical, m_usePost);

        //WindowIntPtrUtility.FrechWindowWithExactProcessName(in m_wowProcessName, out List<WindowIntPtrUtility.ProcessInformation> p, out m_wowProcess);

        //SendKeyMessageToWindows.SendMouseLeftUp(m_wowProcess[i].GetAsParent(), l2rPct, b2tPct);
        //SendKeyMessageToWindows.SendMouseLeftDown(m_wowProcess[i].GetAsParent(), l2rPct, b2tPct);
        //SendKeyMessageToWindows.SendMouseLeftUp(m_wowProcess[i].GetAsParent(), x, y);
        //SendKeyMessageToWindows.SendMouseLeftDown(m_wowProcess[i].GetAsParent(), x, y);
        //IUser32CoordinateContextConverter

        //    m_accessLoaded.Fetch(in m_processTarget, out bool found, out m_deductedInfoTarget);
        //m_deductedInfoTarget.m_frameSize.GetBottomToTopToRelative(m_yBotToTop, out int y);
        //m_deductedInfoTarget.m_frameSize.GetLeftToRightToRelative(m_xLeftToRight, out int x);
        //yield return PostMouseUtility.SendMouseLeftDown(m_processTarget,
        //        x, y);
        //yield return PostMouseUtility.SendMouseLeftUp(m_processTarget,
        //        x, y);

        //TDD_MoveMouseToSpecificProcess <<< Use

    }

}
