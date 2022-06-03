using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WowCoordinateRawMono : MonoBehaviour
{
    public InputField m_mapId;
    public InputField m_mapInfo;
    public InputField m_mapX;
    public InputField m_mapY;
    public string m_format = "MAP|SUBMAP|MAPX|MAPY";

    public void SetWithWowRaw(WowCoordinateRaw raw)
    {
        if(m_mapId)
             m_mapId.text = raw.m_mapId;
        if (m_mapInfo)
            m_mapInfo.text = raw.m_subInfo;
        if (m_mapX)
            m_mapX.text = string.Format("{0:00.00000}", raw.m_mapX);
        if (m_mapY)
            m_mapY.text = string.Format("{0:00.00000}", raw.m_mapY);

    }
}
