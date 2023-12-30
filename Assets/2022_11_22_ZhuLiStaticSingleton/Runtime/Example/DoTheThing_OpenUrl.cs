using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTheThing_OpenUrl : MonoBehaviour
{

    public void OnEnable()
    {
        ZhuLi.AddListener(OpenUrl);
    }
    public void OnDisable()
    {
        ZhuLi.RemoveListener(OpenUrl);

    }

    private void OpenUrl(IZhuLiCommand theThingToDo)
    {
        if (theThingToDo != null && theThingToDo is ZhuLi_OpenUrl) {
            ZhuLi_OpenUrl toDo = (ZhuLi_OpenUrl) theThingToDo;
            Application.OpenURL(toDo.m_urlToOpen);
        }
    }
}
