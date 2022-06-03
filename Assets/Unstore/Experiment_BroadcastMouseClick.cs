using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class Experiment_BroadcastMouseClick : MonoBehaviour
{
    public KeyCode m_keyCodeLoop = KeyCode.Home;
    public KeyCode m_keyCodeOnce = KeyCode.End;
    public int m_sent;
    public string m_wowProcessName = "Wow";
    public List<WindowIntPtrUtility.ProcessInformation> m_wowProcess;
    public float m_timeBetweenLoop=1f;
    public void Start()
    {
        RefreshWowProcess();
        ExperimentCoroutine();
    }

    [ContextMenu("Refresh")]
    private void RefreshWowProcess()
    {
        WindowIntPtrUtility.FrechWindowWithExactProcessName(in m_wowProcessName, out List<WindowIntPtrUtility.ProcessInformation> p, out m_wowProcess);
    }

    public int x=10;
    public int y=10;

    public void Update()
    {
        if (Input.GetKeyDown(m_keyCodeLoop))
        {
            m_loopActive = !m_loopActive;
        }
        if (Input.GetKeyDown(m_keyCodeOnce))
        {
            //float x = UnityEngine.Random.value;
            //float y = UnityEngine.Random.value;
            BroadcastMessageMouseLeftDown(x, y);
            BroadcastMessageMouseLeftUp(x, y);
            //   BroadcastMessageMouseLeft(0.5f, 0.5f);
        }
    }

    private void ExperimentCoroutine()
    {
        StartCoroutine(Start_ExperimentCoroutine());

    }
    public bool m_loopActive;
    public float m_jumpChance = 0.05f;
    public IEnumerator Start_ExperimentCoroutine()
    {
        while (true)
        {
            if (m_loopActive)
            {

                //float x = UnityEngine.Random.value;
                //float y = UnityEngine.Random.value;
                BroadcastMessageMouseLeftDown(x,y);
                yield return new WaitForSeconds(0.1f);
                BroadcastMessageMouseLeftUp(x, y);
                yield return new WaitForSeconds(0.1f);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForEndOfFrame();
        }
    }


    public void BroadcastMessageMouseLeft(float l2rPct, float b2tPct)
    {
        BroadcastMessageMouseLeftDown(l2rPct, b2tPct);
        BroadcastMessageMouseLeftUp(l2rPct, b2tPct);
    }

    public void BroadcastMessageMouseLeftUp(float l2rPct, float b2tPct)
    {
        for (int i = 0; i < m_wowProcess.Count; i++)

            SendKeyMessageToWindows.SendMouseLeftUp(m_wowProcess[i].GetAsParent(), l2rPct, b2tPct);
    }

    public void BroadcastMessageMouseLeftDown(float l2rPct, float b2tPct)
    {
        for (int i = 0; i < m_wowProcess.Count; i++)

            SendKeyMessageToWindows.SendMouseLeftDown(m_wowProcess[i].GetAsParent(), l2rPct, b2tPct);
    }
    public void BroadcastMessageMouseLeftUp(int x, int y)
    {
        for (int i = 0; i < m_wowProcess.Count; i++)

            SendKeyMessageToWindows.SendMouseLeftUp(m_wowProcess[i].GetAsParent(), x, y);
    }

    public void BroadcastMessageMouseLeftDown(int x, int y)
    {
        for (int i = 0; i < m_wowProcess.Count; i++)

            SendKeyMessageToWindows.SendMouseLeftDown(m_wowProcess[i].GetAsParent(), x, y);
    }

}
