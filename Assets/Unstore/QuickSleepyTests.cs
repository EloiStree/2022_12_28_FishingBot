using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class QuickSleepyTests : MonoBehaviour
{


    public List<WindowIntPtrUtility.ProcessInformation> m_allProcess = new List<WindowIntPtrUtility.ProcessInformation>();
    public List<WindowIntPtrUtility.ProcessInformation> m_processFound = new List<WindowIntPtrUtility.ProcessInformation>();

    public string m_whatToLookForProcessName = "Wow";

    void Start()
    {
        RefreshListOfProcess();
        InvokeRepeating("RefreshListOfProcess", 0, 0.5f);

    }

    [Range(0f,1f)]
    public float m_pourcentWidth;

    [Range(0f, 1f)]
    public float m_pourcentHeight;

    public int x;
    public int y;
    public int xt;
    public int yt;
    public int width;
    public int height;
    public int widthInner;
    public int heightInner;


    public int m_borderTopWindow = 10;
    public int m_borderLeftWindow = 7;
    public int m_borderRightWindow = 7;
    public int m_borderDownWindow = 0;



    public WindowIntPtrUtility.Point m_point;
    public float m_xPercent;
    public float m_yPercent;

    public float m_xPercentSave;
    public float m_yPercentSave;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RefreshListOfProcess();
            MoveCursorToTest();
        }
        WindowIntPtrUtility.GetCursorPos(ref m_point);
        GetPercentOf((int)m_point.x, (int)m_point.y, out m_xPercent, out m_yPercent);


        if (Input.GetMouseButtonDown(1))
        {
            m_xPercentSave = m_xPercent;
            m_yPercentSave = m_yPercent;
        }


        if (Input.GetMouseButtonDown(2))
            SwitchAutoLoot();
    }

   

    private void GetPercentOf(int x, int y, out float xPercent, out float yPercent)
    {
        x -= this.x;
        y -= this.yt ;
        y -= m_borderTopWindow;
        xPercent = (x / (float) widthInner);
        yPercent = (y / (float) heightInner);
    }

    public List<WindowIntPtrUtility.IntPtrToRawRect> list = new List<WindowIntPtrUtility.IntPtrToRawRect>();
    [ContextMenu("Refresh")]
    private void RefreshListOfProcess()
    {
        WindowIntPtrUtility.FetchAllProcesses(out m_allProcess);
        WindowIntPtrUtility.FrechWindowWithExactProcessNameFrom(in m_whatToLookForProcessName, in m_allProcess, out m_processFound);

        list.Clear();
        for (int i = 0; i < m_processFound.Count; i++)
        {

            IntPtrWrapGet[] prs = WindowIntPtrUtility.GetProcessIdChildrenWindows(IntPtrTemp.Int( m_processFound[i].m_processId));

            if (prs.Length <= 0) return;
            foreach (IntPtrWrapGet p in prs)
            {
                WindowIntPtrUtility.RectPadValue r = new WindowIntPtrUtility.RectPadValue();
                WindowIntPtrUtility.GetWindowRect(p, ref r);
                if(r.m_borderLeft != 0 || r.m_borderRight != 0 
                    || r.m_borderTop != 0 || r.m_borderBottom != 0)
                list.Add(new WindowIntPtrUtility.IntPtrToRawRect(p, r));
            }

         
        }


        if (list.Count > 0)
        {
            WindowIntPtrUtility.IntPtrToRawRect w = list[0];
            x = w.m_rectInt.m_borderLeft;
            y = w.m_rectInt.m_borderBottom;
            xt = w.m_rectInt.m_borderRight;
            yt = w.m_rectInt.m_borderTop;
            width = Math.Abs(w.m_rectInt.m_borderLeft - w.m_rectInt.m_borderRight);
            height = Math.Abs(w.m_rectInt.m_borderBottom - w.m_rectInt.m_borderTop);
            widthInner = width - (m_borderLeftWindow + m_borderRightWindow);
            heightInner = height - (m_borderTopWindow + m_borderDownWindow);
           
            //AdjustWindowRectEx(ref m_rectttt, dwStyle, bMenu, dwExStyle);
        }
    }


    [ContextMenu("MoveCursor")]
    public void MoveCursorToTest() {


        if (list.Count > 0)
        {
            WindowIntPtrUtility. IntPtrToRawRect w = list[0];
            x = w.m_rectInt.m_borderLeft;
            y = w.m_rectInt.m_borderBottom;
            xt = w.m_rectInt.m_borderRight;
            yt = w.m_rectInt.m_borderTop;
            width = Math.Abs(x -xt);
            height = Math.Abs(y-yt);
            widthInner = width - (m_borderLeftWindow + m_borderRightWindow);
            heightInner = height - (m_borderTopWindow + m_borderDownWindow);
            SetCursorPosPercent(m_xPercent,m_yPercent);
            WindowIntPtrUtility.SetForegroundWindow((IntPtr)w.m_intPtr);
        }
        
    }


    public void SwitchAutoLoot() {
        StartCoroutine(Coroutine_SwitchAutoLoot());
    }
    public IEnumerator Coroutine_SwitchAutoLoot() {
        yield return new WaitForSeconds(5);
        SetCursorPosPercent(0.9921, .9762901);
        yield return new WaitForSeconds(5);
        SetCursorPosPercent(0.503120, .45467);
        yield return new WaitForSeconds(5);
        SetCursorPosPercent(0.3845,0.376569);
    }

    private void SetCursorPosPercent(double xPercent, double yPercent)
    {
        WindowIntPtrUtility.SetCursorPos(
                   (int)(
                   x + m_borderLeftWindow +
                   (width - (m_borderLeftWindow + m_borderRightWindow)) * xPercent),
                   (int)(
                   y - m_borderDownWindow - (height - (m_borderTopWindow + m_borderDownWindow)) * (1f - yPercent)));

    }

    public uint dwStyle;
    public bool bMenu;
    public uint dwExStyle;


   
}
