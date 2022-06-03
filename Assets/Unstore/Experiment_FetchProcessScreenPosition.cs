using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static WindowIntPtrCoordiante;

public class Experiment_FetchProcessScreenPosition : MonoBehaviour
{

    public List<WindowIntPtrUtility.ProcessInformation> m_wowProcess;
    public float m_timeBetweenLoop = 1f;
    public string m_wowProcessName = "Wow";
    public List<WindowIntPtrCoordiante.RECT> coordinates;
    public List<RectWindowWrapper> m_coordinatesWithOrigine;
    public IEnumerator Start()
    {
        RefreshWowProcess();
        for (int i = 0; i < m_coordinatesWithOrigine.Count; i++)
        {
            //WindowIntPtrCoordiante.SetCursorPos(m_coordinatesWithOrigine[i].m_xLowerRightCorner,
            //    m_coordinatesWithOrigine[i].m_yLowerRightCorner);
            //yield return new WaitForSeconds(2); 
            WindowIntPtrCoordiante.SetCursorPos(
                (int)(
                ( m_coordinatesWithOrigine[i].m_xLowerRightCorner +
                m_coordinatesWithOrigine[i].m_xUpperLefCorner ) / 2f),
                (int)
                (( m_coordinatesWithOrigine[i].m_yLowerRightCorner +
                m_coordinatesWithOrigine[i].m_yUpperLefCorner ) / 2f));
            yield return new WaitForSeconds(2);
            //WindowIntPtrCoordiante.SetCursorPos(m_coordinatesWithOrigine[i].m_xUpperLefCorner,
            //    m_coordinatesWithOrigine[i].m_xUpperLefCorner);
            //yield return new WaitForSeconds(2);
        }
   
    }


    [ContextMenu("Refresh")]
    private void RefreshWowProcess()
    {
        WindowIntPtrUtility.FrechWindowWithExactProcessName(in m_wowProcessName, out List<WindowIntPtrUtility.ProcessInformation> p, out m_wowProcess);

        coordinates.Clear();
        m_coordinatesWithOrigine.Clear();
        for (int i = 0; i < m_wowProcess.Count; i++)
        {
           IntPtrWrapGet[] pointers= WindowIntPtrUtility.GetProcessIdChildrenWindows(IntPtrTemp.Int(m_wowProcess[i].m_processId));
            for (int j = 0; j < pointers.Length; j++)
            {
                WindowIntPtrCoordiante.RECT found;
                WindowIntPtrCoordiante.GetWindowRect(pointers[j], out found);
                if (found.Left != 0 || found.Right != 0 || found.Top != 0 || found.Bottom != 0) { 
                    coordinates.Add(found);
                    RectWindowWrapper r = new RectWindowWrapper(m_wowProcess[i].m_processId, pointers[j], found);
                    r.SetWith(found);
                    m_coordinatesWithOrigine.Add(r);
                }
            }
        }
    }

    [System.Serializable]
    public struct RectWindowWrapper
    {
        public RECT m_win32RawRect;
        public int m_processId;
        public IntPtrWrapGet m_givenHandler;
        public int m_xUpperLefCorner;
        public int m_yUpperLefCorner;
        public int m_xLowerRightCorner;
        public int m_yLowerRightCorner;

        public RectWindowWrapper(int processId, IntPtrWrapGet intPtr, RECT found) : this()
        {
            this.m_processId = processId;
            this.m_givenHandler = intPtr;
            this.m_win32RawRect = found;
        }

        public void SetWith(RECT rect) {
            m_xUpperLefCorner   = rect.Left;   
            m_yUpperLefCorner   = rect.Top;   
            m_xLowerRightCorner = rect.Right; 
            m_yLowerRightCorner = rect.Bottom;
        }
    }
}


public class WindowIntPtrCoordiante {

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
    public static  bool GetWindowRect(IntPtrWrapGet hwnd, out RECT lpRect) {
        return GetWindowRect(hwnd.GetAsIntPtr(), out lpRect);
    }
    
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    public static extern int SetCursorPos(int x, int y);
    [System.Serializable]
   [StructLayout(LayoutKind.Sequential)]
public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
   
}
