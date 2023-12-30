using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugZhuLiReceiverMono : MonoBehaviour
{
    public string m_typeReceived;
    [TextArea(2,8)]
    public string m_receivedThingToDo;
    public bool m_listenToStaticInstance;

    public void OnEnable()
    {
        if(m_listenToStaticInstance)
        ZhuLi.AddListener(DebugTheThing);
    }

    public void OnDisable()
    {
        if(m_listenToStaticInstance)
        ZhuLi.RemoveListener(DebugTheThing);
    }

    public void DebugTheThing(IZhuLiCommand theThingToDo)
    {
        m_receivedThingToDo= JsonUtility.ToJson(theThingToDo);
        m_typeReceived = theThingToDo.GetType().ToString();
    }
    
   
}
