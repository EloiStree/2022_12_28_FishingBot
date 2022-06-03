using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_GuestDirectionWowMove : MonoBehaviour
{
    public WowCoordinateRaw m_previousValide;
    public WowCoordinateRaw m_currentValide;

    public Vector2 m_wowDirection;

    public void TryToPushWowCoordinate(WowCoordinateRaw wowCoordinate) {

        if (wowCoordinate.IsBothCoordinateValide())
        {
            m_previousValide = m_currentValide;
            m_currentValide = wowCoordinate;
            m_wowDirection = new Vector2(m_currentValide.m_mapX - m_previousValide.m_mapX,
                m_currentValide.m_mapY - m_previousValide.m_mapY);
        }
    }
}
