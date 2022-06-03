using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;


public class Experiment_BoardcastKeyToWow : MonoBehaviour
{
    public bool m_useKeycode;
    public KeyCode m_keyCodeLoop = KeyCode.Space;
    public KeyCode m_keyCodeFollow = KeyCode.Space;
    public string m_wowProcessName = "Wow";
    public List<WindowIntPtrUtility.ProcessInformation> m_wowProcess;

    public BoolSwitchableCoroutine m_doStuffEvery5MinutesHolder;
    public BoolSwitchableCoroutine m_doStuffEvery15MinutesHolder;
    public BoolSwitchableCoroutine m_demoTutorialSkip;
    public BoolSwitchableCoroutine m_demoDoubleTutorialSkip;
    public void Start()
    {
        m_doStuffEvery5MinutesHolder = new BoolSwitchableCoroutine(this, Start_DoStuffsEvery5Minutes(), false);
        m_doStuffEvery15MinutesHolder = new BoolSwitchableCoroutine(this, Start_DoStuffsEvery15Minutes(), false);
        m_demoTutorialSkip = new BoolSwitchableCoroutine(this, Start_DemoSkipTutorial(), false);
        m_demoDoubleTutorialSkip = new BoolSwitchableCoroutine(this, Start_DemoDoubleSkipTutorial(), false);

        RefreshWowProcess();
        ExperimentCoroutine();
        ExperimentCoroutineHunter();
        StartCoroutine(Start_TurnAround());


    }

    [ContextMenu("Refresh")]
    public void RefreshWowProcess()
    {
        WindowIntPtrUtility.FrechWindowWithExactProcessName(in m_wowProcessName, out List<WindowIntPtrUtility.ProcessInformation> p, out m_wowProcess);
    }

    public void Update()
    {
        if (m_useKeycode) { 
            if (Input.GetKeyDown(m_keyCodeLoop))
            {
                m_loopActive = !m_loopActive;
            }
            if (Input.GetKeyDown(m_keyCodeFollow))
            {
                m_useFollow = !m_useFollow;
            }
        }
    }

    public void SetLoopOn(bool value)
    {
        m_loopActive = value;
    }
    public void SetFollowOn(bool value)
    {
        m_useFollow = value;
    }
    public void SetHunterLoop(bool value)
    {
        m_useHunter = value;
    }
    public void SetHunterPetLoop(bool value)
    {
        m_useHunterPet = value;
    }

    private void ExperimentCoroutine()
    {
        StartCoroutine(Start_ExperimentCoroutine());

    }
    private void ExperimentCoroutineHunter()
    {
        StartCoroutine(Start_ExperimentCoroutineHunter());
        StartCoroutine(Start_ExperimentCoroutineHunterPet());
    }

  
    public bool m_useFollow;
    public bool m_useHunter;
    public bool m_useHunterPet;
    public bool m_loopActive;
    public float m_jumpChance=0.05f;
    public float m_timeBetweenPower=2;

    public void SetTimeBetweenPower(string timeBetweenPower)
    {
        float.TryParse(timeBetweenPower, out float t);
        SetTimeBetweenPower(t);
    }

    public void SetTimeBetweenPower(float timeBetweenPower)
    {
        m_timeBetweenPower = timeBetweenPower < 0.1f ? 0.1f : timeBetweenPower;
    }
    public void SetTurnAround(string timeBetweenPower)
    {
        float.TryParse(timeBetweenPower, out float t);
        SetTurnAround(t);
    }

    public void SetTurnAround(float timeBetweenPower)
    {
        m_turnAroundValue = timeBetweenPower < 0.1f ? 0.1f : timeBetweenPower;
    }
    public void SetTurnAroundBetween(string timeBetweenPower)
    {
        float.TryParse(timeBetweenPower, out float t);
        SetTurnAroundBetween(t);
    }

    public void SetTurnAroundBetween(float timeBetweenPower)
    {
        m_turnAroundBetweenValue = timeBetweenPower < 0.1f ? 0.1f : timeBetweenPower;
    }


    public Eloi.PrimitiveUnityEvent_String m_setClipboardTo;

    public void TryBroadcastMessage_Slash(string textFollowingSlash) => TryBroadcastMessage("/"+textFollowingSlash);
    public void TryBroadcastMessage_Hi() => TryBroadcastMessage_Slash("hi");
    public void TryBroadcastMessage_Follow(string toFollowName) => TryBroadcastMessage("/follow "+ toFollowName);
    public void TryBroadcastMessage_Target(string toTargetName) => TryBroadcastMessage("/target [nodead] "+ toTargetName);
    public void TryBroadcastMessage(string textToPast, float delayToWaitSetOfClipboard=0.1f) {

        m_setClipboardTo.Invoke(textToPast);
        Invoke("BroadcastPastAction", delayToWaitSetOfClipboard);
    }

    public IEnumerator Start_ExperimentCoroutine()
    {
        while (true) {

            if (m_useFollow)
            {
                yield return FollowTarget();
            }

            if (m_loopActive) {


                if (m_useFollow) yield return FollowTarget();
                yield return Loot();
                yield return lookForEnemy();
                yield return SpamJump1();

                if (m_useFollow) yield return FollowTarget();
                yield return Loot();
                yield return lookForEnemy();
                yield return SpamJump1();

                if (m_useFollow) yield return FollowTarget();
                yield return Loot();
                yield return lookForEnemy();
                yield return SpamJump1();



                yield return Loot();
                yield return DoExtraStuff();


            }
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator Start_ExperimentCoroutineHunter()
    {
        while(true) {

            if (m_useFollow)
            {
                yield return FollowTarget();
            }

            if (m_useHunter)
            {


                if (m_useFollow) yield return FollowTarget();
                yield return Loot();
                yield return lookForEnemy();
                yield return Attack1234Jump();

                if (m_useFollow) yield return FollowTarget();
                yield return Loot();
                yield return lookForEnemy();
                yield return Attack1234Jump();

                if (m_useFollow) yield return FollowTarget();
                yield return Loot();
                yield return lookForEnemy();
                yield return Attack1234Jump();



                yield return Loot();
                yield return DoExtraStuff();


            }
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator Start_ExperimentCoroutineHunterPet()
    {

        while (true) { 
            if (m_useHunterPet)
            {
                Debug.Log("Pet Start loop");
                yield return PetAttack();
                yield return new WaitForSeconds(10);
                yield return PetBringBack();
                yield return new WaitForSeconds(10);
                yield return PetRevivePlus();
                yield return new WaitForSeconds(4);
            }
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetTurnAroundValue(bool value) {
        m_useTurnAround = value;
    }
    public bool m_useTurnAround;
    public float m_turnAroundValue = 0.123f;
    public float m_turnAroundBetweenValue = 0.123f;
    private IEnumerator Start_TurnAround()
    {
        while (true)
        {
            if (m_useTurnAround)
            {
                BroadcastMessageDown(User32PostMessageKeyEnum.VK_D);
                yield return new WaitForSeconds(m_turnAroundValue);
                BroadcastMessageUp(User32PostMessageKeyEnum.VK_D);
                yield return new WaitForSeconds(m_turnAroundBetweenValue);
            }
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForEndOfFrame();
        }
    }


    public void SetDemoSkipTutorial(bool activeDemo)
    {
        m_demoTutorialSkip.SetActiveState(activeDemo);
    }
    public void SetDemoDoubleSkipTutorial(bool activeDemo)
    {

        m_demoDoubleTutorialSkip.SetActiveState(activeDemo);
    }
    public bool m_useDemoSkip;
    private IEnumerator Start_DemoSkipTutorial()
    {
        while (true)
        {
                BroadcastMessageDown(User32PostMessageKeyEnum.VK_1);
                yield return new WaitForSeconds(0.1f);
                BroadcastMessageUp(User32PostMessageKeyEnum.VK_1);
                yield return new WaitForSeconds(0.1f);
                BroadcastMessageDown(User32PostMessageKeyEnum.VK_TAB);
                yield return new WaitForSeconds(0.1f);
                BroadcastMessageUp(User32PostMessageKeyEnum.VK_TAB);
                yield return new WaitForSeconds(1.7f);
          
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Start_DemoDoubleSkipTutorial()
    {
        while (true)
        {
            BroadcastMessageDown(User32PostMessageKeyEnum.VK_1);
            yield return new WaitForSeconds(0.1f);
            BroadcastMessageUp(User32PostMessageKeyEnum.VK_1);
            yield return new WaitForSeconds(m_timeBetweenPower);
            BroadcastMessageDown(User32PostMessageKeyEnum.VK_2);
            yield return new WaitForSeconds(0.1f);
            BroadcastMessageUp(User32PostMessageKeyEnum.VK_2);
            yield return new WaitForSeconds(0.1f);
            BroadcastMessageDown(User32PostMessageKeyEnum.VK_TAB);
            yield return new WaitForSeconds(0.1f);
            BroadcastMessageUp(User32PostMessageKeyEnum.VK_TAB);
            yield return new WaitForSeconds(0.05f);
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetDoStuffEvery5Minutes(bool setTrue)
    {
        m_doStuffEvery15MinutesHolder.SetActiveState(setTrue);
    }
   
    private IEnumerator Start_DoStuffsEvery5Minutes()
    {
        while (true)
        {
                for (int i = 0; i < 3; i++)
                {
                    BroadcastMessageDown(User32PostMessageKeyEnum.VK_9);
                    yield return new WaitForSeconds(0.5f);
                    BroadcastMessageUp(User32PostMessageKeyEnum.VK_9);
                    yield return new WaitForSeconds(0.1f);
                }
            yield return new WaitForSeconds(60*5);
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetDoStuffEvery15Minutes(bool setTrue)
    {

        m_doStuffEvery15Minutes = setTrue;
    }
    public bool m_doStuffEvery15Minutes;
    private IEnumerator Start_DoStuffsEvery15Minutes()
    {
        while (true)
        {
           
                for (int i = 0; i < 3; i++)
                {
                    BroadcastMessageDown(User32PostMessageKeyEnum.VK_9);
                    yield return new WaitForSeconds(0.5f);
                    BroadcastMessageUp(User32PostMessageKeyEnum.VK_9);
                    yield return new WaitForSeconds(0.1f);
                }
            
            yield return new WaitForSeconds(60*15);
            yield return new WaitForEndOfFrame();
        }
    }



    private IEnumerator FollowTarget()
    {
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_F);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_F);
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator PetAttack()
    {
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_F5);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_F5);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_F6);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_F6);
        yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator PetBringBack()
    {
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_F7);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_F7);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator FollowSelectedTarget()
    {
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_G);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_G);
        yield return new WaitForSeconds(0.1f);
    }
   
    private IEnumerator PetRevivePlus()
    {
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_F8);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_F8);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator Loot() {
        yield return new WaitForSeconds(0.05f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_F9);
        yield return new WaitForSeconds(0.05f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_F9);
        yield return new WaitForSeconds(0.05f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_F10);
        yield return new WaitForSeconds(0.05f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_F10);
        yield return new WaitForSeconds(0.05f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_F11);
        yield return new WaitForSeconds(0.05f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_F11);
    }

    private IEnumerator SpamJump1()
    {
        yield return lookForEnemy();
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_1);
        float chance = UnityEngine.Random.value;
        if (chance < m_jumpChance)
        {
            yield return new WaitForSeconds(0.1f);
            BroadcastMessageDown(User32PostMessageKeyEnum.VK_SPACE);
            yield return new WaitForSeconds(0.1f);
            BroadcastMessageUp(User32PostMessageKeyEnum.VK_SPACE);
        }
        yield return new WaitForSeconds(m_timeBetweenPower);



    }


    private IEnumerator Attack1234Jump()
    {
        yield return lookForEnemy();
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_1);
        float chance = UnityEngine.Random.value;
        if (chance < m_jumpChance)
        {
            yield return new WaitForSeconds(0.1f);
            BroadcastMessageDown(User32PostMessageKeyEnum.VK_SPACE);
            yield return new WaitForSeconds(0.1f);
            BroadcastMessageUp(User32PostMessageKeyEnum.VK_SPACE);
        }

        BroadcastMessageUp(User32PostMessageKeyEnum.VK_1);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_2);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_2);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_3);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_3);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_4);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_4);

        yield return new WaitForSeconds(m_timeBetweenPower);

    }

    private IEnumerator lookForEnemy()
    {
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_8);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_8);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_TAB);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_TAB);
    }
    private IEnumerator DoExtraStuff()
    {
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_5);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_5);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_6);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_6);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageDown(User32PostMessageKeyEnum.VK_7);
        yield return new WaitForSeconds(0.1f);
        BroadcastMessageUp(User32PostMessageKeyEnum.VK_7);
        yield return new WaitForSeconds(m_timeBetweenPower);
    }

    private void BroadcastMessageUp(User32PostMessageKeyEnum key)
    {
        for (int i = 0; i < m_wowProcess.Count; i++)
        {
            SendKeyMessageToWindows.SendKeyUpToProcessChildren(key, m_wowProcess[i].GetAsChildren());
            //Debug.Log("Try down " + m_wowProcess[i].m_processId +" - "+ key);
            //Thread.Sleep(100);
        }
    }

    private void BroadcastMessageDown(User32PostMessageKeyEnum key)
    {
        for (int i = 0; i < m_wowProcess.Count; i++)
        {

            SendKeyMessageToWindows.SendKeyDownToProcessChildren(key, m_wowProcess[i].GetAsChildren());
            //Thread.Sleep(100);
        }
    }


    public void BroadcastPastAction() {

        for (int i = 0; i < m_wowProcess.Count; i++)
        {

           // SendKeyToWindow.RequestPastAction( m_wowProcess[i].m_processId);
            //Thread.Sleep(100);
        }

    }

}
