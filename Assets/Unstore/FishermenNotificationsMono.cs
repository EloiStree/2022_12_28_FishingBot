using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FishermanActionEvent : UnityEvent<FishermanAction> { }

public class FishermanAction {
    public int m_processId;
    public DateTime m_whenToExecute;
}

public class FishermanAction_RequestNewLine : FishermanAction
{

}
public class FishermanAction_JumpToAvoidAfk : FishermanAction
{

}
public class FishermanAction_RequestRecallOfLine : FishermanAction
{

}
public class FishermanAction_RequestToResetView : FishermanAction
{

}