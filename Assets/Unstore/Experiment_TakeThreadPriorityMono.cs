using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using ThreadPriority = System.Threading.ThreadPriority;

public class Experiment_TakeThreadPriorityMono : MonoBehaviour
{


    void Start()
    {
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
    }

}
