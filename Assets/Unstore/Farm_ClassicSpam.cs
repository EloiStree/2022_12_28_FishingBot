using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm_ClassicSpam : MonoBehaviour
{
    public IndexToProcessIdCollectionMono m_processes;
    public User32KeyStrokeManagerMono m_sender;
    public float m_timeBetweenStep=0.3f;
    public float m_pressTime = 0.1f;
    public int m_index;
    public User32PostMessageKeyEnum[] m_keyToPress;
    public IntPtrWrapGet m_targetProcess;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
          
            yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(m_timeBetweenStep);

            for (int i = 0; i < m_processes.GetCount(); i++)
            {
                m_processes.GetProcessIdOf(i, out int processId);
                if (processId == 0) continue;
                m_targetProcess = IntPtrProcessId.Int(processId);

                User32KeyStrokeManager.SendKeyPostMessage(m_targetProcess, m_keyToPress[m_index], User32PressionType.Press);



            }
            yield return new WaitForSecondsRealtime(m_pressTime);
            for (int i = 0; i < m_processes.GetCount(); i++)
            {
                m_processes.GetProcessIdOf(i, out int processId);
                if (processId == 0) continue;
                m_targetProcess = IntPtrProcessId.Int(processId);


                User32KeyStrokeManager.SendKeyPostMessage(m_targetProcess, m_keyToPress[m_index], User32PressionType.Release);


            }



            m_index++;


            if (m_index >= m_keyToPress.Length)
            {
                m_index = 0;

            }
        }
    }

}
