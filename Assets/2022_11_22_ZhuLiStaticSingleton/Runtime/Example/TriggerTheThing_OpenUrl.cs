using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTheThing_OpenUrl : MonoBehaviour
{
    public string m_urlToOpen;
    [ContextMenu("Open Given Url with what ever.")]
    public void OpenUrl()
    {
        ZhuLi.DoTheThing(new ZhuLi_OpenUrl() { m_urlToOpen =m_urlToOpen});
    }

}
