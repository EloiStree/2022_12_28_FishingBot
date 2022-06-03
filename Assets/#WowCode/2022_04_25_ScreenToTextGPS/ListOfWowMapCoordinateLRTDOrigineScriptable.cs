using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListOfWowMapCoordLRTD", menuName = "Wow/List of Wow Map Coordinate", order = 1)]
public class ListOfWowMapCoordinateLRTDOrigineScriptable : ScriptableObject
{
    public WowMapCoordinateLRTDOriginGroup m_data = new WowMapCoordinateLRTDOriginGroup();

}
[System.Serializable]
public class WowMapCoordinateLRTDOriginGroup {
    public List<WowMapCoordinateLRTDOrigin> m_coordinates= new List<WowMapCoordinateLRTDOrigin>();
} 