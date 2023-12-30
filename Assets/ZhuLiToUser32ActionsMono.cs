using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script is making the transition but should not be in any lib As it not belong to user nor to zhuli
/// </summary>
public class ZhuLiToUser32ActionsMono : MonoBehaviour
{
    public GenericInterpreterAuctionHouseMono<I_Interpreter<IZhuLiCommand>,IZhuLiCommand> m_commandReceived;

    public void OnEnable()
    {
        ZhuLi.AddListener(ReceivedCommands);
    }


    private void ReceivedCommands(IZhuLiCommand theThingToDo)
    {
        m_commandReceived.PushToAuction(theThingToDo);
    }

    public void OnDisable()
    {

        ZhuLi.AddListener(ReceivedCommands);
    }
}
 