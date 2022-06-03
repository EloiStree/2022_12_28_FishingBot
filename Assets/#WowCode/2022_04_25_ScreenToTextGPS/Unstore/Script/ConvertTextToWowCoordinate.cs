using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class ConvertTextToWowCoordinate : MonoBehaviour
{

    [TextArea(0,5)]
    public string m_previousText;

    [TextArea(0, 5)]
    public string m_currentText;


    public string m_mapStringId;
    public string m_mapSubInfoId;
    public string m_xString;
    public string m_yString;
    public float m_x;
    public float m_y;

    public WowCoordinateRaw m_lastCoordinate;
    public WowCoordinateRawEvent m_lastTextAsCoordinate;

    public UnityEvent m_lastTextFailed;

    public void TryToPushTextAsCoordinate(string text) {
        m_previousText = m_currentText;
        m_currentText = text.Replace("\n", "").Replace("\r", "");
        if (m_previousText != m_currentText) {

            TryToConvert(m_currentText);

        }
    
    }

    private void TryToConvert(string text)
    {
        m_lastCoordinate = new WowCoordinateRaw();
        MatchCollection m = Regex.Matches(text, "[0-9]+\\.[0-9]*");
        m_mapStringId = ""; 
        m_mapSubInfoId = "";
        m_xString = "";
        m_yString = "";
        int i = 0;
        foreach (Match item in m)
        {
            if (i == 0)
                m_xString = item.Value;
            if (i == 1)
                m_yString = item.Value;
            i++;
        }

        m_lastCoordinate.m_foundX = float.TryParse(m_xString, out m_x);
        m_lastCoordinate.m_foundY = float.TryParse(m_yString, out m_y);

        int splitIndex = text.IndexOf(":");

        if (splitIndex>-1) {
            string mapId = text.Substring(0,splitIndex);

            int subinfoIndex = mapId.IndexOf("-");
            if (subinfoIndex < 0)
            {
                m_mapStringId = mapId;
                m_mapSubInfoId = "";
            }
           else
            {
                m_mapStringId = mapId.Substring(0, subinfoIndex);
                m_mapSubInfoId = mapId.Replace(m_mapStringId,"");
            }
        }

        m_lastCoordinate.m_mapId = m_mapStringId;
        m_lastCoordinate.m_subInfo = m_mapSubInfoId;
        m_lastCoordinate.m_mapX = m_x;
        m_lastCoordinate.m_mapY = m_y;
        m_lastTextAsCoordinate.Invoke(m_lastCoordinate);
    }
}


[System.Serializable]
public class WowCoordinateRawEvent : UnityEvent<WowCoordinateRaw>{ }

[System.Serializable]
public class WowCoordinateRaw
{
    public string m_mapId;
    public string m_subInfo;
    public bool m_foundX;
    public float m_mapX;
    public bool m_foundY;
    public float m_mapY;

    public void GetPositionAsPercent(out Vector2 pourcent)
    {
        pourcent = new Vector2(m_mapX * 0.01f, m_mapY*0.01f);
    }

    internal bool IsBothCoordinateValide()
    {
        return m_foundX && m_foundY;
    }

}


public class WowCoordinateXY2XY
{
    public WowMapCoordinateLRDTUnity m_from;
    public WowMapCoordinateLRDTUnity m_to;
}
public class WowCoordinateWithDirectionXY
{
    public WowMapCoordinateLRDTUnity m_from;
    public WowMapDirectionLRDTUnity m_to;
}
