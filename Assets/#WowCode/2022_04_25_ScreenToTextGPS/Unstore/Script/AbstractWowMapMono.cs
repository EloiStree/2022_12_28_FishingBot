using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractWowMapMono : MonoBehaviour
{

    public Transform m_centerRootMap;

    public void GetUnityWorldPosition(WowCoordinateRaw rawCoordinate, out Vector3 worldPosition) {


        rawCoordinate.GetPositionAsPercent(out Vector2 pourcent);
        pourcent.x -= 0.5f;
        pourcent.y -= 0.5f;
        pourcent.y *=-1f;

        Vector3 local = new Vector3(pourcent.x,0, pourcent.y);
        worldPosition = m_centerRootMap.TransformPoint(local);
    }
}
