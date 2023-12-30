using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using WindowNativeInput.Abstract;

public class SleppyPushTointefaceFacade : MonoBehaviour
{

    public int m_processTarget;
    public int m_processUsed;
    public bool m_isParentProcess;
    public int m_childActiveId;
    public WindowIntPtrUtility.ProcessInformation m_processInfo;
    public string m_whatToPress = " ";
    public EnumInputMessageType m_inputType;
    public TDD_InterfaceFacadeBuilder m_facade;
    public DelegateFrameReference m_frameReference;

    public bool m_useDesktopInput;

    public void Update()
    {
        WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(IntPtrProcessId.Int(m_processTarget), out bool found
            , out IntPtrWrapGet childActive);
        if (found == false || childActive == null) return;
        m_processUsed = childActive.GetAsInt();
        m_frameReference = new DelegateFrameReference(GetProcessId);
        m_facade.GetFrameReferenceFromProcessId(m_frameReference.GetFrameIdAsInt(), out IFrameReference frame);

        ProcessesAccessInScene.Instance.GetProcessInfoFor(IntPtrProcessId.Int(m_frameReference.GetFrameIdAsInt()),out  found, out m_processInfo);
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_useDesktopInput)
            {

                m_facade.SetFocusOnFrameReference(frame);
                Thread.Sleep(1000);
                m_facade.DesktopKeyStroke(m_whatToPress, EnumPressType.Press);
            }
            else { 
                m_facade.FrameKeyStroke(m_whatToPress, EnumPressType.Press, m_inputType, frame);
            }
            Eloi.E_DebugLog.LogParams(" -- ", "Test", m_whatToPress, m_inputType.ToString(), frame);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (m_useDesktopInput)
            {
                m_facade.SetFocusOnFrameReference(frame);
                Thread.Sleep(1000);
                m_facade.DesktopKeyStroke(m_whatToPress, EnumPressType.Release);
            }
            else
            {
                m_facade.FrameKeyStroke(m_whatToPress, EnumPressType.Release, m_inputType, frame);
            }
        }
    }
    public void GetProcessId(out int id) { id = m_processUsed; }
}
