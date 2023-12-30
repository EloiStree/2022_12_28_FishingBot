using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm_HunterPetFetch : MonoBehaviour
{

    public IndexToProcessIdCollectionMono m_processes;
    public User32PostMessageKeyEnum m_recallPet = User32PostMessageKeyEnum.VK_V;
    public User32PostMessageKeyEnum m_healRevivePet = User32PostMessageKeyEnum.VK_N;
    public User32PostMessageKeyEnum m_fetchNewTarget = User32PostMessageKeyEnum.VK_C;
    public User32PostMessageKeyEnum m_sentPetAttack = User32PostMessageKeyEnum.VK_V;
    public User32PostMessageKeyEnum m_lootrang = User32PostMessageKeyEnum.VK_F12;
    public User32PostMessageKeyEnum m_championAttackOne = User32PostMessageKeyEnum.VK_1;
    public User32PostMessageKeyEnum m_championAttackTwo = User32PostMessageKeyEnum.VK_2;
    public User32PostMessageKeyEnum m_championAttackThree = User32PostMessageKeyEnum.VK_3;
    public User32PostMessageKeyEnum m_dualhealPet = User32PostMessageKeyEnum.VK_4;
    public float m_pressTime = 0.1f;

    public IntPtrWrapGet m_targetProcess;

    IEnumerator Start()
    {

        while (true) {

            yield return new WaitForEndOfFrame();
            yield return PushKey(m_recallPet);
            yield return PushKey(m_lootrang);
            yield return new WaitForSeconds(1);
            yield return PushKey(m_healRevivePet);
            ;
            yield return new WaitForSeconds(5);
            yield return PushKey(m_sentPetAttack);
            yield return new WaitForSeconds(0.1f);
            ;
            yield return new WaitForSeconds(3);
            yield return PushKey(User32PostMessageKeyEnum.VK_TAB);
            yield return PushKey(m_championAttackOne);
            ;
            yield return new WaitForSeconds(3);
            yield return PushKey(User32PostMessageKeyEnum.VK_TAB);
            yield return PushKey(m_championAttackTwo);
            ;
            yield return new WaitForSeconds(3);
            yield return PushKey(User32PostMessageKeyEnum.VK_TAB);
            yield return PushKey(m_championAttackThree);
            ;
            yield return new WaitForSeconds(1.5f);
            yield return PushKey(m_dualhealPet);
            yield return new WaitForSeconds(1.5f);
            yield return PushKey(m_lootrang);



        }
    }

    private IEnumerator PushKey(User32PostMessageKeyEnum key)
    {
        for (int i = 0; i < m_processes.GetCount(); i++)
        {
            m_processes.GetProcessIdOf(i, out int processId);
            if (processId == 0) continue;
            m_targetProcess = IntPtrProcessId.Int(processId);

            User32KeyStrokeManager.SendKeyPostMessage(m_targetProcess, key, User32PressionType.Press);



        }
        yield return new WaitForSecondsRealtime(m_pressTime);
        for (int i = 0; i < m_processes.GetCount(); i++)
        {
            m_processes.GetProcessIdOf(i, out int processId);
            if (processId == 0) continue;
            m_targetProcess = IntPtrProcessId.Int(processId);


            User32KeyStrokeManager.SendKeyPostMessage(m_targetProcess, key, User32PressionType.Release);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
