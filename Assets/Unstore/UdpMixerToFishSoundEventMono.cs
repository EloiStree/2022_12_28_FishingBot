using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UdpMixerToFishSoundEventMono : MonoBehaviour
{
    public string[] m_messageToFishermenIndex = new string[] {
    "Ḇ1wowAudio0",
    "Ḇ1wowAudio1",
    "Ḇ1wowAudio2",
    "Ḇ1wowAudio3",
    "Ḇ1wowAudio4",
    "Ḇ1wowAudio5",
    "Ḇ1wowAudio6",
    "Ḇ1wowAudio7",
    "Ḇ1wowAudio8",
    "Ḇ1wowAudio9",
    "Ḇ1wowAudio10"
    };

    public Eloi.PrimitiveUnityEvent_Int m_onFishermenIndexHadSound;

    public void UdpReceivedMessage(string udpMessage) {

        for (int i = 0; i < m_messageToFishermenIndex.Length; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(udpMessage,
                m_messageToFishermenIndex[i], 
                true, true)) {
                m_onFishermenIndexHadSound.Invoke(i);
            }
        }
    }
}
