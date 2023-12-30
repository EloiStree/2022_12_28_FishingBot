using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using static ThreadDateTimeActionUtility;

public class UnityThreadQueueDateTimeCall {
    private static UnityThreadQueueDateTimeCallMono m_instanceInScene;
    
    public static UnityThreadQueueDateTimeCallMono Instance {
        get {
            if (m_instanceInScene == null)
               Eloi.E_SearchInSceneUtility.TryToFetchWithActiveInScene<UnityThreadQueueDateTimeCallMono>(ref m_instanceInScene,true);

            if (m_instanceInScene == null)
            {
                GameObject created = new GameObject("Unity Thread Queue Call");
                UnityThreadQueueDateTimeCallMono script = created.AddComponent<UnityThreadQueueDateTimeCallMono>();
                m_instanceInScene = script;
            }

            return m_instanceInScene;
        }
    }
   

        public static void SetInstance(UnityThreadQueueDateTimeCallMono overrideWith) => m_instanceInScene = overrideWith;

}

public class UnityThreadQueueDateTimeCallMono : MonoBehaviour
{
    public AbstractQueueDateTimeCall m_threadDateExecuter; 
    public int m_inWaitingToBeExecuted;

    public bool m_useUpdateToCheck;


    [ContextMenu("Reset at zero Queue")]
    public void ResetWaitingQueueToZero() {
        m_threadDateExecuter.ResetListToZero();
    }
    public void Awake()
    {
        UnityThreadQueueDateTimeCall.SetInstance(this);
    }
    public void Update()
    {
        m_inWaitingToBeExecuted = m_threadDateExecuter.m_waitToBeInjectedTothreadQueue.Count;
        if (m_useUpdateToCheck)
            m_threadDateExecuter.CheckTheQueueForActionToPush();
    }

    public void AddFromNowInMs(int milliseconds, Action action)
    => m_threadDateExecuter.AddFromNowInMs(milliseconds, action);
    public void Add(DateTimeAction waitAction)
        => m_threadDateExecuter.Add(waitAction);
    }
