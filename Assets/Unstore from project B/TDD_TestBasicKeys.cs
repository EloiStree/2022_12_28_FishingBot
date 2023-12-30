using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowNativeInput.Abstract;

public class TDD_TestBasicKeys : MonoBehaviour
{
    public TDD_InterfaceFacadeBuilder m_pushFacade;

    public int m_processTarget;
    public int m_processUsed;
    public EnumInputMessageType m_inputType;
    public IFrameReference m_reference;
    public bool m_testFrame = true;
    public bool m_testDesktop=true;

    public float m_timeBetween=1000;
    public string m_whatToPush = "0,1,2,3,4,5,6,7,8,9,np0,np1,np2,np3,np4,np5,np6,np7,np8,np9";

    [ContextMenu("SetWithNumber")]
    public void SetWithNumber() => m_whatToPush = m_numbers;
    public string m_numbers = "0,1,2,3,4,5,6,7,8,9,np0,np1,np2,np3,np4,np5,np6,np7,np8,np9";

    [ContextMenu("SetWithLetter")]
    public void SetWithLetter() => m_whatToPush = m_alphabet;
    public string m_alphabet = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";

    [ContextMenu("SetSpecial")]
    public void SetSpecial() => m_whatToPush = m_testSpecial;
    public string m_testSpecial = "*+/-.↓←→↑";

    [ContextMenu("SetWithF1To22")]
    public void SetWithF1To22() => m_whatToPush = m_testF1to22;
    public string m_testF1to22 = "F0,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22";
    [ContextMenu("SetAllChart")]
    public void SetAllChart() { m_whatToPush = string.Join(',', m_numbers, m_alphabet, m_testSpecial, m_testF1to22); }

    [ContextMenu("PushTests")]
    public void PushTests()
    {
        StartCoroutine(Coroutine_PushTest());
    }

    IEnumerator Coroutine_PushTest() {

        Eloi.E_DebugLog.A();
        m_pushFacade.GetDisplayFrameReferenceFromProcessId(m_processTarget, out m_reference);
        if (m_reference == null)
            m_processUsed = 0;
        else
            m_processUsed = m_reference.GetFrameIdAsInt();

        m_pushFacade.SetFocusOnFrameReference(m_reference);
        yield return new WaitForSeconds(m_timeBetween);

        string[] tokens = m_whatToPush.Split(',');
        foreach (var item in tokens)
        {
            Debug.Log("Test:" + item);
            if (m_testFrame) { 
                yield return new WaitForSeconds(m_timeBetween);
                m_pushFacade.FrameKeyStroke(in item, EnumPressType.Press, m_inputType, m_reference);
                yield return new WaitForSeconds(m_timeBetween);
                m_pushFacade.FrameKeyStroke(in item, EnumPressType.Release, m_inputType, m_reference);
            }
            if (m_testDesktop)
            {
                yield return new WaitForSeconds(m_timeBetween);
                m_pushFacade.DesktopKeyStroke(in item, EnumPressType.Press);
                yield return new WaitForSeconds(m_timeBetween);
                m_pushFacade.DesktopKeyStroke(in item, EnumPressType.Release);
            }
        }
    }

}
  