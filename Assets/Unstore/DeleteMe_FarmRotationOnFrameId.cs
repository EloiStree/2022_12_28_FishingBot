using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;

public class DeleteMe_FarmRotationOnFrameId : MonoBehaviour
{
    public int m_processId;
    public float m_secondToWait = 0.3f;
    public float m_pressTime = 0.1f;
    public int m_index;
    public User32PostMessageKeyEnum[] m_keyToPress;
    public IntPtrWrapGet m_targetProcess;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            m_targetProcess = IntPtrProcessId.Int(m_processId);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(m_secondToWait);
            User32KeyStrokeManager.SendKeyPostMessage(m_targetProcess, m_keyToPress[m_index], User32PressionType.Press);
            yield return new WaitForSecondsRealtime(m_pressTime); 
            User32KeyStrokeManager.SendKeyPostMessage(m_targetProcess, m_keyToPress[m_index], User32PressionType.Release);
            m_index++;
            if (m_index >= m_keyToPress.Length) { 
                m_index = 0;
                User32KeyStrokeManager.SendKeyPostMessage(m_targetProcess, User32PostMessageKeyEnum.VK_TAB, User32PressionType.Press);
                yield return new WaitForSecondsRealtime(m_pressTime);
                User32KeyStrokeManager.SendKeyPostMessage(m_targetProcess, User32PostMessageKeyEnum.VK_TAB, User32PressionType.Release);

            }
        }
    }

}
