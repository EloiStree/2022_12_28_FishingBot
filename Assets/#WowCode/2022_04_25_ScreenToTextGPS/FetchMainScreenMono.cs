using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using static VdmDesktopManager;

public class FetchMainScreenMono : MonoBehaviour
{
    public RawImage m_debugImageRaw;
    public AspectRatioFitter m_ratioFitter;


    public int m_displayCount;
    public Texture2D m_screenTexture;
    public Eloi.ClassicUnityEvent_Texture2D m_createdTextureFromRenderer;

    [Tooltip("Monitor Color Space")]
    public bool LinearColorSpace = false;


    IEnumerator Start()
    {

        m_displayCount = DesktopCapturePlugin_GetNDesks();
        DesktopCapturePlugin_Initialize();
        int width = DesktopCapturePlugin_GetWidth(0);
        int height = DesktopCapturePlugin_GetHeight(0);
        //var tex = new Texture2D(width, height, TextureFormat.BGRA32, false, LinearColorSpace);
        var tex = new Texture2D(width, height, TextureFormat.BGRA32, false, LinearColorSpace);
        //m_screenTexture[i] = new Texture2D(2, 2);
        m_screenTexture = tex;
        if (m_debugImageRaw)
            m_debugImageRaw.texture = tex;
        if (m_ratioFitter)
            m_ratioFitter.aspectRatio = width / (float)height;
        DesktopCapturePlugin_SetTexturePtr(0, m_screenTexture.GetNativeTexturePtr());
        yield return new WaitForSeconds(1);
        StartCoroutine(OnRender());
        m_createdTextureFromRenderer.Invoke(m_screenTexture);
    }

    IEnumerator OnRender()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            GL.IssuePluginEvent(DesktopCapturePlugin_GetRenderEventFunc(), 0);
        }
    }



    //public void HackStart()
    //{
    //    HackStop();

    //    string exePath = "Assets\\VR Desktop Mirror\\Hack\\VrDesktopMirrorWorkaround.exe";
    //    if (System.IO.File.Exists(exePath))
    //    {
    //        m_process = new System.Diagnostics.Process();
    //        m_process.StartInfo.FileName = exePath;
    //        m_process.StartInfo.CreateNoWindow = true;
    //        m_process.StartInfo.UseShellExecute = true;
    //        m_process.StartInfo.Arguments = GetActiveWindow().ToString();
    //        m_process.Start();
    //    }
    //    else
    //    {
    //        Debug.Log("VR Desktop Mirror Hack exe not found: " + exePath);
    //    }
    //}

    //public void HackStop()
    //{
    //    if (m_process != null)
    //    {
    //        if (m_process.HasExited == false)
    //        {
    //            m_process.Kill();
    //        }
    //    }
    //    m_process = null;
    //}


    [DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();
    [DllImport("DesktopCapture")]
    private static extern void DesktopCapturePlugin_Initialize();
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetNDesks();
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetWidth(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetHeight(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetNeedReInit();
    [DllImport("DesktopCapture")]
    private static extern bool DesktopCapturePlugin_IsPointerVisible(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetPointerX(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetPointerY(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_SetTexturePtr(int iDesk, IntPtr ptr);
    [DllImport("DesktopCapture")]
    private static extern IntPtr DesktopCapturePlugin_GetRenderEventFunc();


}
