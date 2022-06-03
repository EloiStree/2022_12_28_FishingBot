using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowMapPointMono : MonoBehaviour
{
    public Transform m_toAffect;
    public AbstractWowMapMono m_inMapTarget;
    public WowCoordinateRaw m_given;

    public bool m_hideIfNotValide;
    public Eloi.PrimitiveUnityEventExtra_Bool m_onDisplayRequest;

    public void SetWithWowCoordinateRaw(WowCoordinateRaw wowPosition) {

        bool isValide = wowPosition.IsBothCoordinateValide();
        if(m_hideIfNotValide)
            m_onDisplayRequest.Invoke(isValide);
        if (isValide) { 
            m_inMapTarget.GetUnityWorldPosition(wowPosition, out Vector3 position);
            m_toAffect.position = position;
        }
    }

    private void Reset()
    {
        m_toAffect = gameObject.transform;
        m_inMapTarget = GetComponentInParent<AbstractWowMapMono>();
    }
}
