using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI2User32_ButtonToBroadcastToProcessName : MonoBehaviour
{
    public string m_processName;
    public string m_whatToBroadcast;
    public StringType m_typeOfPush;
    public float m_chatDelayBetweenPush=300;
    public enum StringType { Chat, Keystroke}

        public void Push()
    {
        if (m_typeOfPush == StringType.Chat) {
            CoroutinePushChatToAll();
                }
        if (m_typeOfPush == StringType.Keystroke)
            User32BoardcastUtilityToThread.HeavyTryParseAndSendToProcesses(m_processName, m_whatToBroadcast);
    }
    private void CoroutinePushChatToAll() {
        StartCoroutine(     User32BoardcastUtilityToThread.Coroutine_CopyPastChatTextToProcesses(m_processName, m_whatToBroadcast, m_chatDelayBetweenPush / 1000f));
    }
    
}
