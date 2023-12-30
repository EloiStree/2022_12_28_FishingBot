using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest_ZhuLi_FrameActionsMono : MonoBehaviour
{
    public int[] m_processesId;
    public float m_timeBetforeStart = 5f;
    public float m_secondsBetweenPress = 0.1f;
    public float m_secondsBetweenKeys = 1f;
    public ZhuLi_SetFrameDisplayState.DisplayStateType[] m_displays;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_timeBetforeStart);

        
        ZhuLiStruct.User32.ProcessesArrayOfId targets = new ZhuLiStruct.User32.ProcessesArrayOfId()
        {
            m_processesId = m_processesId
        };
        foreach (var item in m_processesId)
        {
            ZhuLi.DoTheThing(new ZhuLi_SetFocusOnFrame()
            {
                m_processToApplyTo = new ZhuLiStruct.User32.ProcessUniqueId() { m_processId= item }
            });
            yield return new WaitForSeconds(2);
        }
        
        foreach (var d in m_displays)
            {
               
                ZhuLi.DoTheThing(new ZhuLi_SetFrameDisplayState()
                {
                    m_processToApplyTo = new ZhuLiStruct.User32.ProcessesArrayOfId() { m_processesId = m_processesId } ,
                    m_displayStateType = d
                });
            yield return new WaitForSeconds(2);    
        }


             
    }

    // Update is called once per frame
    void Update()
    {

    }
}
