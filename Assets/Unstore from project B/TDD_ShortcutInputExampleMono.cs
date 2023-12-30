using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ➤ ☗ | ↓ ← → ↑ _ ‾ ∨ ∧ ¬ ⊗ ≡ ≤ ≥ ⌃ ⌄ ⊓⇅ ⊔⇵ ⊏ ⊐ ↱↳ ∑ -no unity ⤒ ⤓ ⌈ ⌊ 🀲 🀸 ⌛ ⏰ ▸ ▹ 🐁 🖱 💾 ↕ ♺ 💻 🗔 🖧 Ḇ ↕ ◩◪⬔⬕⬓⬒◧◨◰◱◲◳⯐
 
public class TDD_ShortcutInputExampleMono : MonoBehaviour
{
    public TextAsset m_fileToPush;
    public string m_textToPush;

    public string m_focusId;

    public string m_processName;

    public List<WindowIntPtrUtility.ProcessInformation> m_wowProcess;



    [ContextMenu("Refresh")]
    public void RefreshWowProcess()
    {
        WindowIntPtrUtility.FrechWindowWithExactProcessName(in m_processName, out List<WindowIntPtrUtility.ProcessInformation> p, out m_wowProcess);
    }


    public void PushText()
    {
        PushCommandLine(m_textToPush);
    }
    public void PushFile()
    {
        PushCommandLine(m_fileToPush.text);

    }

    public void PushCommandLine(string command) { 

        
      //  Experiment_BoardcastKeyToWow

    }

    void Start()
    {




 /*
 🗔45646:0-9 //Send Input to frame with id 45646
 🗔Wow:0-9 //Send Input to registered by user as wow
 🗔🔎mspaint:0-9 //Make search on a frame call mspaint send to first found
 🗔🔎📡mspaint:0-9 //Make search on a frame call mspaint send to all found
 🗔📡Wow:0-9 // Send input to all frame call exactly wow
 💻0-9 // Input to the computer 
 ☕0-9 // Input to JOMI localy on the computer
 🔌🤖0-9 // Input to USB port robot // Will be use with arduino later on the project

 */

        SendShortCut ("💻A", "Should press A as window Input");
        SendShortCut ("💻0", "Should press 0 alpha as window Input");
        SendShortCut ("💻0", "Should press 0 alpha as window Input"); 
        SendShortCut ("🗔46542 🖮B", "Should press 0 alpha as window Input"); 
        SendShortCut ("🗔🔎Wow 🖮B", "Should press 0 alpha as window Input"); 
        SendShortCut ("🗔🔎Wow:0 🖮B", "Should press 0 alpha as window Input");
        SendShortCut ("🗔📕Wow 🖮B", "Should press 0 alpha as window Input");  
        SendShortCut ("💾Bonjour>💬","");  
        SendShortCut ("🗔🔎Wow:0", "Should press 0 alpha as window Input"); 


        //🖧 On The know Network
        //🗔Frame
        //💻Computer
        //Ӕ Arduino
        //ᛒᛡ Bluetooh keyboard
        // 📲 Android ADB
        //🖱🐁Mouse
        //⌨🖮 Keyboard   
        //💾Clipboard 
        //🍪Permanant clipboard
        //🗎🗋🖹file direction
        //🔒 Lock
        //🔌 Port USB 
        //🎹 Midi
        //🎛 Midi Knobs
        //🌐 internet
        // 🎮 xbox
        // 🕹 Direct Input Controller
        //🎲 Random Number

    }

    private void SendShortCut(string v1, string v2)
    {
       

        //bool sendToAll = v1.IndexOf("📡") > -1;
        //bool frameSymbol = v1.IndexOf("🗔") > -1;
        //if (v1.StartsWith("🗔"))
        //{

        //}
        //if (v1.StartsWith("🗔📡"))
        //{

        //}

     //   SendKeyMessageToWindows.
    }

    // Update is called once per frame
    void Update()
    {
        
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


    public void BroadcastPastAction()
    {

        for (int i = 0; i < m_wowProcess.Count; i++)
        {

            // SendKeyToWindow.RequestPastAction( m_wowProcess[i].m_processId);
            //Thread.Sleep(100);
        }

    }
}
