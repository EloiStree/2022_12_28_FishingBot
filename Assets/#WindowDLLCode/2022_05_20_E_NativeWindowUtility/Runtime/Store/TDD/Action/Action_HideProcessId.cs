using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Action_HideProcessId : IUser32Action
{
    public IntPtrProcessId m_processId;
}

