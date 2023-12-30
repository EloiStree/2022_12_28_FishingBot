using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowPetSeekAndFightMono : MonoBehaviour
{

    public int[] m_processesId;
    public ZhuLiEnum.KeyboardStableKey [] m_selectEnemy;
    public ZhuLiEnum.KeyboardStableKey m_pet_heal = ZhuLiEnum.KeyboardStableKey.F1;
    public ZhuLiEnum.KeyboardStableKey m_pet_attack = ZhuLiEnum.KeyboardStableKey.F2;
    public ZhuLiEnum.KeyboardStableKey m_pet_callback = ZhuLiEnum.KeyboardStableKey.F3;
    public ZhuLiEnum.KeyboardStableKey m_player_autoTarget = ZhuLiEnum.KeyboardStableKey.Tab;
    // Start is called before the first frame update

    public ZhuLiEnum.KeyboardStableKey[] m_playerAttackEnumList;

    public float m_secondsBetweenPress = 1;
    public float m_secondsBetweenKeys = 2;

    public float m_dogTimeToFetch = 5;
    public float m_dogTimeToComback = 3;


    public int m_jumpCount;
    public int m_jumpIndex;
    void Start()
    {
        StartCoroutine(Coroutine_PetFetch());
        StartCoroutine(Coroutine_TabPlayerAttack());
    }
    public bool m_useAutotarget;

    // Update is called once per frame
    IEnumerator Coroutine_PetFetch()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            ZhuLiStruct.User32.ProcessesArrayOfId targets = new ZhuLiStruct.User32.ProcessesArrayOfId()
            {
                m_processesId = m_processesId
            };


            Eloi.E_UnityRandomUtility.GetRandomOf(out ZhuLiEnum.KeyboardStableKey enemy, m_selectEnemy);
            Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = enemy, m_keyPressionType = ZhuLiEnum.User32PressType.Press, m_processesToApplyTo = targets });
            yield return new WaitForSeconds(m_secondsBetweenPress);
            Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = enemy, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });
            yield return new WaitForSeconds(0.2f);

            Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = m_pet_heal, m_keyPressionType = ZhuLiEnum.User32PressType.Press, m_processesToApplyTo = targets });
            yield return new WaitForSeconds(m_secondsBetweenPress);
            Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = m_pet_heal, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });
           
            yield return new WaitForSeconds(m_dogTimeToFetch);
            
            Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = m_pet_callback, m_keyPressionType = ZhuLiEnum.User32PressType.Press, m_processesToApplyTo = targets });
            yield return new WaitForSeconds(m_secondsBetweenPress);
            Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = m_pet_callback, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });

            yield return new WaitForSeconds(m_dogTimeToComback);
        }
    }

    IEnumerator Coroutine_TabPlayerAttack() {


        while (true)
        {
            yield return new WaitForEndOfFrame();
            //Eloi.E_EnumUtility.GetAllEnumOf(out ZhuLiEnum.KeyboardStableKey[] list);

            ZhuLiStruct.User32.ProcessesArrayOfId targets = new ZhuLiStruct.User32.ProcessesArrayOfId()
            {
                m_processesId = m_processesId
            };
            foreach (var item in m_playerAttackEnumList)
            {
                if(m_useAutotarget)
                { 
                    Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = m_player_autoTarget, m_keyPressionType = ZhuLiEnum.User32PressType.Press, m_processesToApplyTo = targets });
                    yield return new WaitForSeconds(m_secondsBetweenPress);
                    Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = m_player_autoTarget, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });
                    yield return new WaitForSeconds(0.1f);
                }
                Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = item, m_keyPressionType = ZhuLiEnum.User32PressType.Press, m_processesToApplyTo = targets });
                yield return new WaitForSeconds(m_secondsBetweenPress);
                Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = item, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });
                yield return new WaitForSeconds(m_secondsBetweenKeys);

            }
            Eloi.E_UnityRandomUtility.GetRandomOf(out int rp, m_processesId);
            m_jumpCount++;
            if (m_jumpCount % 20 == 0)
            {
                m_jumpIndex++;

                targets = new ZhuLiStruct.User32.ProcessesArrayOfId()
                {
                    m_processesId = new int[] { m_processesId[m_jumpIndex % m_processesId.Length] }
                };

                Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = ZhuLiEnum.KeyboardStableKey.Space, m_keyPressionType = ZhuLiEnum.User32PressType.Press, m_processesToApplyTo = targets });
                yield return new WaitForSeconds(m_secondsBetweenPress);
                Execute(new ZhuLi_FrameStableKeyPression() { m_keyboardKey = ZhuLiEnum.KeyboardStableKey.Space, m_keyPressionType = ZhuLiEnum.User32PressType.Release, m_processesToApplyTo = targets });

            }

        }
    }

    public void Execute(ZhuLi_FrameStableKeyPression request)
    {

        if (request.m_keyPressionType == ZhuLiEnum.User32PressType.Press)
        {

            foreach (var item in request.m_processesToApplyTo.m_processesId)
            {
                SendKeyMessageToWindows.SendKeyDown(User32ZhuliConverter.ConvertPostKey(request.m_keyboardKey)
                    , IntPtrProcessId.Int(item), true);
                // Debug.Log("Received:" + request.m_keyboardKey+"  -  "+ User32ZhuliConverter.ConvertPostKey(request.m_keyboardKey));
            }
        }
        else
        {
            foreach (var item in request.m_processesToApplyTo.m_processesId)
            {
                SendKeyMessageToWindows.SendKeyUp(User32ZhuliConverter.ConvertPostKey(request.m_keyboardKey)
                    , IntPtrProcessId.Int(item), true);
            }
        }

    }
}
