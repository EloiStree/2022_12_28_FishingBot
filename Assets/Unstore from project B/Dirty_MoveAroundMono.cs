using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using WindowNativeInput.Abstract;

public class Dirty_MoveAroundMono : MonoBehaviour
{
    public TDD_InterfaceFacadeBuilder m_pushFacade;
    public int m_processTarget;
    public int m_processUsed;
    public EnumInputMessageType m_inputType;
    public IFrameReference m_reference;

    public Eloi.PrimitiveUnityEventExtra_Bool m_left;
    public Eloi.PrimitiveUnityEventExtra_Bool m_right;
    public Eloi.PrimitiveUnityEventExtra_Bool m_forward;
    public Eloi.PrimitiveUnityEventExtra_Bool m_backward;

    public void Awake()
    {
        m_left.m_valueEvent.AddListener(MoveLeft);
        m_right.m_valueEvent.AddListener(MoveRight);
        m_forward.m_valueEvent.AddListener(MoveForward);
        m_backward.m_valueEvent.AddListener(MoveBackward);
    }
    public void MoveLeft(bool value)
        => m_pushFacade.FrameKeyStroke("←", value ? EnumPressType.Press : EnumPressType.Release, m_inputType, m_reference);
    public void MoveRight(bool value)
           => m_pushFacade.FrameKeyStroke("→", value ? EnumPressType.Press : EnumPressType.Release, m_inputType, m_reference);
    public void MoveForward(bool value)
           => m_pushFacade.FrameKeyStroke("↑", value ? EnumPressType.Press : EnumPressType.Release, m_inputType, m_reference);
    public void MoveBackward(bool value)
           => m_pushFacade.FrameKeyStroke("↓", value ? EnumPressType.Press : EnumPressType.Release, m_inputType, m_reference);

    public float h;
    public float v;
    private void SetWithProcess(int m_processTarget)
    {
        m_pushFacade.GetDisplayFrameReferenceFromProcessId(m_processTarget,out m_reference);
        if ( m_reference == null) 
            m_processUsed =0;
        else 
            m_processUsed = m_reference.GetFrameIdAsInt();
    }

    public void Update()
    {
        SetWithProcess(m_processTarget); 
        WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(IntPtrProcessId.Int(m_processTarget),
            out bool found   , out IntPtrWrapGet childActive);

         h = Input.GetAxis("Horizontal");
         v = Input.GetAxis("Vertical");
        m_left.Invoke(h < -0.1f);
        m_right.Invoke(h > 0.1f);
        m_forward.Invoke(v  > 0.1f);
        m_backward.Invoke(v < -0.1f);
        
    }
    public void GetProcessId(out int id) { id = m_processUsed; }
}
