
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static ThreadDateTimeActionUtility;

public class ThreadDateTimeActionUtility {
    [System.Serializable]
    public struct DateTimeAction
    {
        public DateTime m_at;
        public Action m_toDo;
        public DateTimeAction(DateTime at, Action toDo)
        {
            this.m_at = at;
            this.m_toDo = toDo;
        }
    }
}


[System.Serializable]
public class AbstractQueueDateTimeCall 
{
    public  DateTime m_start;
    public Queue<DateTimeAction> m_waitToBeInjectedTothreadQueue = new Queue<DateTimeAction>();
  
   

    public void Add(DateTimeAction waitAction) {
        if(m_waitToBeInjectedTothreadQueue!=null)
        m_waitToBeInjectedTothreadQueue.Enqueue(waitAction);
        m_actionWaitingToBeTracked = m_waitToBeInjectedTothreadQueue.Count;
    }

    public bool HasSomethingInQueues()
    {
        return m_actionObservedToExecuteQueue.Count>0 || m_waitToBeInjectedTothreadQueue.Count>0;
    }

    public int m_actionInThreadWaitingToBeExecuted;
    public int m_actionWaitingToBeTracked;




    private DateTime m_previous;
    private DateTime m_current;
    public Eloi.StringClampHistory m_errorHistory = new Eloi.StringClampHistory();
    public Eloi.StringClampHistory m_pushHistory = new Eloi.StringClampHistory();
    static readonly object Identity = new object();
    List<DateTimeAction> m_actionObservedToExecuteQueue = new List<DateTimeAction>();

    public void ResetListToZero() {
        m_actionObservedToExecuteQueue.Clear();
        m_waitToBeInjectedTothreadQueue.Clear();
        m_actionInThreadWaitingToBeExecuted = 0;
        m_actionWaitingToBeTracked = 0;
    }
    public string m_exceptionHappenedStack;
    public double m_deltaOfTheadFrame;
    private readonly object balanceLock = new object();
    public DateTime m_2000= new DateTime(1970,1,1);
    public void CheckTheQueueForActionToPush() {

        if (m_start == null || m_start < m_2000)
            m_start = DateTime.Now;

        m_timeBetweenNowAndLastTick = (DateTime.Now - m_lastTickOfThread).TotalMilliseconds;
        Stopwatch watch = new Stopwatch();
         m_deltaOfTheadFrame = m_timeBetweenNowAndLastTick;
      
            try
            {
                lock (balanceLock)
                {
                    m_previous = m_current;
                    m_current = DateTime.Now;
                    m_deltaOfTheadFrame = (m_current - m_previous).TotalMilliseconds;
                    while (m_waitToBeInjectedTothreadQueue.Count > 0)
                    {
                        m_actionObservedToExecuteQueue.Insert(0, m_waitToBeInjectedTothreadQueue.Dequeue());
                }
                m_actionWaitingToBeTracked = m_waitToBeInjectedTothreadQueue.Count;
                m_actionInThreadWaitingToBeExecuted = m_actionObservedToExecuteQueue.Count;

                if (m_actionObservedToExecuteQueue.Count > 0)
                    {

                        for (int i = m_actionObservedToExecuteQueue.Count - 1; i >= 0; i--)
                        {
                            if ( m_actionObservedToExecuteQueue[i].m_at <= m_current)
                            {
                                if (m_actionObservedToExecuteQueue[i].m_toDo != null)
                            {
                                watch.Restart();
                                m_pushHistory.PushIn(i + "|IN|" + m_current.ToString());
                                    m_actionObservedToExecuteQueue[i].m_toDo.Invoke();
                                watch.Stop();
                                m_pushHistory.PushIn(i + "|OUT|" + watch.ElapsedMilliseconds +"|"+ m_current.ToString());
                            }
                                m_actionObservedToExecuteQueue.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_actionObservedToExecuteQueue.Clear();
                m_waitToBeInjectedTothreadQueue.Clear();
                m_errorHistory.PushIn(e.StackTrace);
                m_exceptionHappenedStack = e.StackTrace;
            }
            m_lastTickOfThread =(m_current);

    }
    public DateTime m_lastTickOfThread;
    public double m_timeBetweenNowAndLastTick;
    public void AddFromNowInMs(int milliseconds, Action action)
    {
        DateTime now = DateTime.Now;
        DateTime when= now.AddMilliseconds(milliseconds);
        DateTimeAction actionToExecuteLater = new DateTimeAction(when ,action);
        Add(actionToExecuteLater);
    }

    public int GetInQueueCount()
    {
        return m_actionWaitingToBeTracked+
        m_actionInThreadWaitingToBeExecuted;
    }
}
