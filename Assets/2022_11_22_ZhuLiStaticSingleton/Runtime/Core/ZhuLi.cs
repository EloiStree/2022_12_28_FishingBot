using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ZhuLi
{
    static TheThingToDo m_listeners;
    public delegate void TheThingToDo(IZhuLiCommand theThingToDo);
    public static void DoTheThing(IZhuLiCommand theThingToDo) {
        if (m_listeners != null)
            m_listeners(theThingToDo);
    }
    public static void AddListener(TheThingToDo listenerThatMayKnowHowToDoTheThing)
    {
        if (listenerThatMayKnowHowToDoTheThing != null)
            m_listeners += listenerThatMayKnowHowToDoTheThing;
    }
    public static void RemoveListener(TheThingToDo listenerThatMayKnowHowToDoTheThing)
    {
        if (listenerThatMayKnowHowToDoTheThing != null)
            m_listeners -= listenerThatMayKnowHowToDoTheThing;
    }


}
