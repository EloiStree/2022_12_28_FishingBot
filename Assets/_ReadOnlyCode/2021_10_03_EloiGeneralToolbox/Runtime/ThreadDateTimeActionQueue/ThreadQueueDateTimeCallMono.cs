using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using static ThreadDateTimeActionUtility;

public class ThreadQueueDateTimeCall {
    private static SingletonThreadQueueDateTimeCallMono m_instanceInScene;
    
    public static ThreadQueueDateTimeCallMono Instance {
        get {
            if (m_instanceInScene == null)
                Eloi.E_SearchInSceneUtility.TryToFetchWithActiveInScene<SingletonThreadQueueDateTimeCallMono>(ref m_instanceInScene,true);

            if (m_instanceInScene == null)
            {
                GameObject created = new GameObject("Thread Queue Call");
                SingletonThreadQueueDateTimeCallMono script = created.AddComponent<SingletonThreadQueueDateTimeCallMono>();
                m_instanceInScene = script;
            }

            return m_instanceInScene;
        }
    }

    public static void SetInstance(SingletonThreadQueueDateTimeCallMono overrideWith) 
        => m_instanceInScene = overrideWith;

}
public class SingletonThreadQueueDateTimeCallMono : ThreadQueueDateTimeCallMono {

    public override void Awake()
    {
        ThreadQueueDateTimeCall.SetInstance(this);
        m_autoStartAtAwake = true;
        base.Awake();
    }
}

public class ThreadQueueDateTimeCallMono : MonoBehaviour
{
    public AbstractQueueDateTimeCall m_threadDateExecuter = new AbstractQueueDateTimeCall();

    public System.Threading.ThreadPriority m_threadPriority = System.Threading.ThreadPriority.AboveNormal;
    public bool m_threadNeedToBeAlive=true;
    public bool m_autoStartAtAwake = true;

    public Thread m_currentThreadRunning;
    public bool m_setAsInstanceOfScene;

    [ContextMenu("Reset at zero Queue")]
    public void ResetWaitingQueueToZero()
    {
        m_threadDateExecuter.ResetListToZero();
    }
    public virtual void Awake()
    {
        if(m_autoStartAtAwake)
             RestartThread();
    }

    private void RestartThread()
    {
        KillCurrentThread();
        m_threadNeedToBeAlive = true;
        Thread t = new Thread(Thread_RefreshActionToPush);
        t.Priority = m_threadPriority;
        t.Start();
    }

    public void KillCurrentThread()
    {
        m_threadNeedToBeAlive = false;
        if (m_currentThreadRunning != null)
            m_currentThreadRunning.Abort();
        m_currentThreadRunning = null;
    }

    private void OnApplicationQuit()
    {
        m_threadNeedToBeAlive = false;
        
    }

    private void OnDestroy()
    {
        m_threadNeedToBeAlive = false;

    }
    void Thread_RefreshActionToPush()
    {
        while (m_threadNeedToBeAlive)
        {
            if (m_threadNeedToBeAlive)
                m_threadDateExecuter.CheckTheQueueForActionToPush();
            Thread.Sleep(1);
        }
    }
    public void AddFromNowInMs(int milliseconds, Action action)
    => m_threadDateExecuter.AddFromNowInMs(milliseconds, action);
    public void Add(DateTimeAction waitAction)
        => m_threadDateExecuter.Add(waitAction);

    public bool HasSomethingInQueue()
    {
        if (m_threadDateExecuter == null || m_currentThreadRunning == null)
            return false;
       return  m_threadDateExecuter.HasSomethingInQueues();
    }
    public int GetInQueueCount()
    {
        return m_threadDateExecuter.GetInQueueCount();
    }
}