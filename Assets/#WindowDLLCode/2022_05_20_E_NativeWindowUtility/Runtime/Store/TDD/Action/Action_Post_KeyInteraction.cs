using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_Post_KeyInteraction :  IUser32Action
{
    public IntPtrProcessId m_processId;
    public User32PostMessageKeyEnum m_targetKey;
    public User32PressionType m_pressionType;
}
