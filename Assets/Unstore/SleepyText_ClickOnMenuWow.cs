using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SleepyText_ClickOnMenuWow : MonoBehaviour
{
    public FetchWindowOfProcessBasedOnNameMono m_accessLoaded;
    public DeductedInfoOfWindowSizeWithSource m_deductedInfoTarget;
    public int m_processId;

    public float m_waitFocusToApply = 0.01f;
    public float m_waitBetweenKeyPress = 0.01f;

    public bool m_usePost=true;

    public TextAsset m_textAsset;
    public List<PointAsPercentLR2DT> m_points = new List<PointAsPercentLR2DT>();

    public char m_lineSpliter = '♦';


    public string m_ingamemenu_callMenu = "ingamemenu_callMenu";
    public string m_menu_System     = "menu_System";
    public string m_menu_Interface  = "menu_Interface ";
    public string m_menu_KeyBinding = "menu_KeyBinding";
    public string m_menu_Macro      = "menu_Macro";
    public string m_menu_LogoutId   = "menu_LogoutId";
    public string m_menu_ExitGame   = "menu_ExitGame";
    private void Awake()
    {
        m_accessLoaded.Fetch(in m_processId, out bool found, out m_deductedInfoTarget);
        
    }


    [ContextMenu("Load from text")]
    public void Load() {
        m_points.Clear();
        string t = m_textAsset.text;
        string[] lines = t.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(m_lineSpliter);
            if (parts.Length > 2 && !string.IsNullOrEmpty(parts[0])) {
                try
                {
                    PointAsPercentLR2DT pt = new PointAsPercentLR2DT();
                    if (parts.Length > 0)
                        pt.m_id = parts[0];
                    if (parts.Length > 1)
                        pt.m_left2RightPercent = float.Parse(parts[1]);
                    if (parts.Length > 2)
                        pt.m_down2TopPercent = float.Parse(parts[2]);
                    if (parts.Length > 3)
                        pt.m_description = parts[3];
                    m_points.Add(pt);
                }
                catch (Exception) { }
            }

        }
    }
    


    [System.Serializable]
    public class PointAsPercentLR2DT {
        public string m_id;
        public string m_description;
        [Range(0,1)]
        public float m_left2RightPercent;
        [Range(0, 1)]
        public float m_down2TopPercent;
    }


    [ContextMenu("Launch Test")]
    public void QuickSleepyTest()
    {
        StartCoroutine(ClickTest(m_processId ));
    }
    [ContextMenu("Set Cursor")]
    public void SetUserDefault()
    {
        StartCoroutine(Coroutine_SetUserDefault());
    }
    [ContextMenu("Invoke copy past")]
    public void CopyPastText()
    {
        StartCoroutine(PastText("Hello now: "+DateTime.Now.ToString()));
    }

    private IEnumerator Coroutine_SetUserDefault()
    {
        yield return OpenMenu();
    }

    public IEnumerator OpenMenu() {

        FocusProcessId(m_processId);
        yield return new WaitForSeconds(m_waitFocusToApply);
        yield return PressKeyWithDelay(m_processId, User32PostMessageKeyEnum.VK_ESCAPE);

    }

    public void FocusProcessId(int processId)
    => WindowIntPtrUtility.SetForegroundWindow(processId);
    public void PressEscape(int processId)
        => SendKeyMessageToWindows.SendKeyClick(User32PostMessageKeyEnum.VK_ESCAPE,IntPtrTemp.Int(  processId), m_usePost);

    public void PressKeyDown(int processId, User32PostMessageKeyEnum key)
        => SendKeyMessageToWindows.SendKeyDown(key, IntPtrTemp.Int(processId), m_usePost);
    public void PressKeyUp(int processId, User32PostMessageKeyEnum key)
        => SendKeyMessageToWindows.SendKeyUp(key, IntPtrTemp.Int(processId), m_usePost);


    public void PressKey(int processId, User32PostMessageKeyEnum key)
    {
        PressKeyDown(processId, key);
        PressKeyUp(processId, key);
    }
    public IEnumerator PressKeyWithDelay(int processId, User32PostMessageKeyEnum key)
    {
        PressKeyDown(processId, key);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        PressKeyUp(processId, key);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
    }


    public IEnumerator ClickTest(int processId)
    {
        yield return Test("playerimage");
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test("minimap");
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test(m_ingamemenu_callMenu);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test(m_menu_System);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test(m_menu_Interface);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test(m_menu_KeyBinding);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test(m_menu_Macro);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test(m_menu_LogoutId);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test(m_menu_ExitGame);


        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return Test(m_ingamemenu_callMenu);



    }

    public IEnumerator Test(string id) {
        yield return ClickAt(m_processId, id);
        yield return new WaitForSeconds(1f);
        PressEscape(m_processId);
    }


    private IEnumerator ClickAt(int processId, string whereToPressid)
    {
        yield return ClickAt(processId,(PointAsPercentLR2DT) GetFrom(whereToPressid));
    }

    private PointAsPercentLR2DT GetFrom(string whereToPressid)
    {
        for (int i = 0; i < m_points.Count; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(
                 in m_points[i].m_id,
                 in whereToPressid, true, true))
                return m_points[i];
        }
        return null;
    }

    private IEnumerator ClickAt(int processId, PointAsPercentLR2DT whereToPress)
    {
        if (whereToPress != null)
        {
            m_deductedInfoTarget.m_frameSize.GetLeftToRightToRelative(whereToPress.m_left2RightPercent, out int x);
            m_deductedInfoTarget.m_frameSize.GetBottomToTopToRelative(whereToPress.m_down2TopPercent, out int y);

            Debug.Log("Test: " + x + " " + y);
            yield return PostMouseUtility.SendMouseLeftDown(processId, x, y, true, true);
            yield return new WaitForSeconds(0.01f);
            yield return PostMouseUtility.SendMouseLeftDown(processId, x, y, true, true);
            yield return new WaitForSeconds(0.01f);
            yield return PostMouseUtility.SendMouseLeftUp(processId, x, y, true, true);
            yield return new WaitForSeconds(0.01f);
            yield return PostMouseUtility.SendMouseLeftUp(processId, x, y, true, true);
            //WindowIntPtrUtility.MouseClickLeft();
        }
        else {

            Debug.Log("Hummm");
        }

    }

    public IEnumerator PastText(string text)
    {

        GUIUtility.systemCopyBuffer = text;
        yield return new WaitForSeconds(4);
        //Test text
        PressKey(m_processId, User32PostMessageKeyEnum.VK_RETURN);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        PressKeyDown(m_processId, User32PostMessageKeyEnum.VK_LCONTROL);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);

        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        PressKeyDown(m_processId, User32PostMessageKeyEnum.VK_V);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        PressKeyUp(m_processId, User32PostMessageKeyEnum.VK_V); 
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        // Hello you


        //SendKeyToWindow.RequestPastAction(m_processId);
        //yield return new WaitForSeconds(0.5f);
        //yield return new WaitForSeconds(m_waitBetweenKeyPress);

        PressKeyUp(m_processId, User32PostMessageKeyEnum.VK_LCONTROL);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        PressKey(m_processId, User32PostMessageKeyEnum.VK_BACK);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        PressKey(m_processId, User32PostMessageKeyEnum.VK_BACK);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        yield return new WaitForSeconds(m_waitBetweenKeyPress);
        PressKey(m_processId, User32PostMessageKeyEnum.VK_RETURN);
    }


    //Delete ma after;
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern string SendMessage(int hWnd, int msg, string wParam, IntPtr lParam);
}
