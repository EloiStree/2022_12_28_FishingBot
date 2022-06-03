using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WowCoordinateRawAsTextMono : MonoBehaviour
{
    public InputField m_displayText;
    public string m_format = "MAP|SUBMAP|MAPX|MAPY";

    public void SetWithWowRaw(WowCoordinateRaw raw)
    {
        m_displayText.text = m_format.Replace("MAP", raw.m_mapId)
            .Replace("MAP", raw.m_mapId)
            .Replace("MAPX",string.Format("{0:00.00000}", raw.m_mapX))
            .Replace("MAPY", string.Format("{0:00.00000}", raw.m_mapY))
            .Replace("SUBMAP", raw.m_subInfo);

    }
}
