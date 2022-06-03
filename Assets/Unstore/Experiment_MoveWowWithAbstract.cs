using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_MoveWowWithAbstract : MonoBehaviour
{
    public AbstractWowPlayerInterfaceMono m_wowPlayerInterface;
    public PixelBotCoordinateRaw m_wowCurrentCoordinate;
    public PixelBotCoordinateRaw m_previousValue;
    public float m_runningSpeedPerSecond;
    public float m_backwalkSpeedPerSecond;
    public float m_turningSpeedPerSecond;
    public float m_strafeSpeedPerSecond;



    public void SetPixelCoordinate(PixelBotCoordinateRaw wowCurrentCoordinate) {
        m_wowCurrentCoordinate = wowCurrentCoordinate;

    }
    public void Start()
    {
        if (m_invokeAtStart)
            StartCalibration();
    }

    public bool m_invokeAtStart=true;
    public float m_delayBeforeApplying=5f;
    public float m_turnTime=1f;
    public float m_moveTime=1f;
    [ContextMenu("Start Moving")]
    public void StartCalibration() {
        StartCoroutine(Calibration());
    }

    public IEnumerator Calibration() {
        Vector2 direction;
        yield return new WaitForSeconds(m_delayBeforeApplying);

        m_previousValue = m_wowCurrentCoordinate;
        m_wowPlayerInterface.SetMovingForward(true);
        yield return new WaitForSeconds(m_moveTime);
        m_wowPlayerInterface.SetMovingForward(false);
         direction = GetDirectionOf(in m_wowCurrentCoordinate, in m_previousValue);
        m_runningSpeedPerSecond = direction.magnitude/ m_moveTime;


        yield return new WaitForSeconds(1f);

        m_previousValue = m_wowCurrentCoordinate;
        m_wowPlayerInterface.SetMovingBackward(true);
        yield return new WaitForSeconds(m_moveTime);
        m_wowPlayerInterface.SetMovingBackward(false);
         direction = GetDirectionOf(in m_wowCurrentCoordinate, in m_previousValue);
        m_backwalkSpeedPerSecond = direction.magnitude/ m_moveTime;


        yield return new WaitForSeconds(1f);

        m_previousValue = m_wowCurrentCoordinate;
        m_wowPlayerInterface.SetTurnLeft(true);
        yield return new WaitForSeconds(m_turnTime);
        m_wowPlayerInterface.SetTurnLeft(false); 
        m_turningSpeedPerSecond = GetRotationBetween(in m_wowCurrentCoordinate, in m_previousValue)/m_turnTime;

        yield return new WaitForSeconds(1f);

        m_previousValue = m_wowCurrentCoordinate;
        m_wowPlayerInterface.SetStrafeLeft(true);
        yield return new WaitForSeconds(m_moveTime);
        m_wowPlayerInterface.SetStrafeLeft(false);
         direction = GetDirectionOf(in m_wowCurrentCoordinate, in m_previousValue);
        m_strafeSpeedPerSecond = direction.magnitude/ m_moveTime;

    }

    private float GetRotationBetween(in PixelBotCoordinateRaw wowCurrentCoordinate, in PixelBotCoordinateRaw previousValue)
    {
        //float cValue = wowCurrentCoordinate.m_left2RightPercentRotation;
        //float pValue = previousValue.m_left2RightPercentRotation;

        //Eloi.E_CodeTag.ToCodeLater.Info("It should use vector2 instead of float to avoid bug");
        //return wowCurrentCoordinate.m_left2RightPercentRotation - previousValue.m_left2RightPercentRotation;

        WowMapUtility.GetMapDirection(previousValue.m_direction, wowCurrentCoordinate.m_direction,  out float angle );
        return angle;

    }

    private Vector2 GetDirectionOf(in PixelBotCoordinateRaw wowCurrentCoordinate, in PixelBotCoordinateRaw previousValue)
    {
        return new Vector2(wowCurrentCoordinate.m_position.m_xLeftRight - previousValue.m_position.m_xLeftRight,
            wowCurrentCoordinate.m_position.m_yDownTop - previousValue.m_position.m_yDownTop);
    }
}
