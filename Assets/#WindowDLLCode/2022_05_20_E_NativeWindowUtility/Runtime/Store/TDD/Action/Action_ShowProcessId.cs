using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Action_ShowProcessId : IUser32Action
{
    public IntPtrProcessId m_processId;
}
