
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest_ZhuLi_MouseMove : MonoBehaviour
{
    public float m_timeBetforeStart = 5f;
    public float m_secondsBetweenMove = 1f;
    public ZhuLi_FrameMouseMoveAxis m_frameAxis;
    public ZhuLi_FrameMouseMoveAxes m_frameAxes;
    public ZhuLi_WindowMouseMoveMainWindowAxis m_computerAxis;
    public ZhuLi_WindowMouseMoveMainWindowAxes m_computerAxes;
    public ZhuLi_WindowMouseMoveAtAliasMonitorAxis m_monitorAxis;
    public ZhuLi_WindowMouseMoveAtAliasMonitorAxes m_monitorAxes;
    public string m_debugState;

    public ZhuLi_SetFrameDisplayState.DisplayStateType[] m_displays;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_timeBetforeStart);

        m_debugState = "Test Frame axis";
        ZhuLi.DoTheThing(m_frameAxis);
        yield return new WaitForSeconds(m_secondsBetweenMove);

        m_debugState = "Test Frame axis";
        ZhuLi.DoTheThing(m_frameAxes);
        yield return new WaitForSeconds(m_secondsBetweenMove);

        m_debugState = "Test Computer axis";
        ZhuLi.DoTheThing(m_computerAxis);
        yield return new WaitForSeconds(m_secondsBetweenMove);


        m_debugState = "Test Computer axis";
        ZhuLi.DoTheThing(m_computerAxes);
        yield return new WaitForSeconds(m_secondsBetweenMove);

        m_debugState = "Test Monitor axis";
        ZhuLi.DoTheThing(m_monitorAxis);
        yield return new WaitForSeconds(m_secondsBetweenMove);

        m_debugState = "Test Monitor axis";
        ZhuLi.DoTheThing(m_monitorAxes);
        yield return new WaitForSeconds(m_secondsBetweenMove);


    }

}
