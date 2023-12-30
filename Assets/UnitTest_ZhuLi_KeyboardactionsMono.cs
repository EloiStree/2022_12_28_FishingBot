using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest_ZhuLi_KeyboardactionsMono : MonoBehaviour
{
    public int [] m_processesId;
    public float m_timeBetforeStart=5f;
    public float m_secondsBetweenPress=0.1f;
    public float m_secondsBetweenKeys=1f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_timeBetforeStart);

        Debug.Log("A");

        ZhuLi.DoTheThing(new ZhuLi_SetClipboardWithText()
        {
            m_textToSendToClipboard = "Test Frame Keys"
        });
        yield return new WaitForSeconds(0.1f);
        ZhuLi.DoTheThing(new ZhuLi_WindowCopyPast()
        {
            m_copyPastType = ZhuLi_WindowCopyPast.CopyPastType.Past
        });
        Debug.Log("B");
        yield return new WaitForSeconds(0.1f);
        Eloi.E_EnumUtility.GetAllEnumOf(out ZhuLiEnum.KeyboardStableKey[] list);
        ZhuLiStruct.User32.ProcessesArrayOfId targets = new ZhuLiStruct.User32.ProcessesArrayOfId()
        {
            m_processesId = m_processesId
        };
        foreach (var item in list)
        {
            Debug.Log("C");
            ZhuLi.DoTheThing(new ZhuLi_FrameStableKeyPression()
            {
                m_keyboardKey = item,
                m_keyPressionType = ZhuLiEnum.User32PressType.Press,
                m_processesToApplyTo = targets
            });
            yield return new WaitForSeconds(m_secondsBetweenPress);
            ZhuLi.DoTheThing(new ZhuLi_FrameStableKeyPression()
            {
                m_keyboardKey = item,
                m_keyPressionType = ZhuLiEnum.User32PressType.Release,
                m_processesToApplyTo = targets
            });
            yield return new WaitForSeconds(m_secondsBetweenKeys);
        }

        Debug.Log("D");

        ZhuLi.DoTheThing(new ZhuLi_SetClipboardWithText()
        {
            m_textToSendToClipboard = "Test Window Key input to Frame Keys"
        });
        yield return new WaitForSeconds(0.1f);
        ZhuLi.DoTheThing(new ZhuLi_WindowCopyPast()
        {
            m_copyPastType = ZhuLi_WindowCopyPast.CopyPastType.Past
        });
        yield return new WaitForSeconds(0.1f);
        foreach (var processID in m_processesId)
        {

            Debug.Log("E");
            foreach (var item in list)
            {
                ZhuLi.DoTheThing(new ZhuLi_SetFocusOnFrame() { 
                    m_processToApplyTo = new ZhuLiStruct.User32.ProcessUniqueId() { m_processId = processID} }); 
                ZhuLi.DoTheThing(new ZhuLi_WindowKeyStablePression()
                {
                    m_keyboardKey = item,
                    m_keyPressionType = ZhuLiEnum.User32PressType.Press
                });
                yield return new WaitForSeconds(m_secondsBetweenPress);
                ZhuLi.DoTheThing(new ZhuLi_WindowKeyStablePression()
                {
                    m_keyboardKey = item,
                    m_keyPressionType = ZhuLiEnum.User32PressType.Release
                });
                yield return new WaitForSeconds(m_secondsBetweenKeys);
            }
        }


        Debug.Log("F");

        ZhuLi.DoTheThing(new ZhuLi_SetClipboardWithText()
        {
            m_textToSendToClipboard = "Test Send input to Frame Keys"
        });
        yield return new WaitForSeconds(0.1f);
        ZhuLi.DoTheThing(new ZhuLi_WindowCopyPast()
        {
            m_copyPastType = ZhuLi_WindowCopyPast.CopyPastType.Past
        });
        yield return new WaitForSeconds(0.1f);
        foreach (var item in list)
        {

            Debug.Log("G");
            ZhuLi.DoTheThing(new ZhuLi_FrameStableKeyPression()
            {
                m_keyboardKey = item,
                m_keyPressionType = ZhuLiEnum.User32PressType.Press,
                m_processesToApplyTo = targets
            });
            yield return new WaitForSeconds(m_secondsBetweenPress);
            ZhuLi.DoTheThing(new ZhuLi_FrameStableKeyPression()
            {
                m_keyboardKey = item,
                m_keyPressionType = ZhuLiEnum.User32PressType.Release,
                m_processesToApplyTo = targets
            });
            yield return new WaitForSeconds(m_secondsBetweenKeys);
        }

        Debug.Log("H");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
