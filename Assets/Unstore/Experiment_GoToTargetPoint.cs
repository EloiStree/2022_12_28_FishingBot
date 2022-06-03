using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Experiment_GoToTargetPoint : MonoBehaviour
{

    public AbstractWowPlayerInterfaceMono m_playerInteraction;
    public PixelBotCoordinateRaw m_playerPosition;
    public WowMapCoordinateLRDTUnity m_whereToGo;


    public WowMapDirectionLRDTUnity directionOfPlayer;
    public WowMapDirectionLRDTUnity directionOfPoint;
    public float m_signeAngle;
    public float m_distanceBetween;

    public void SetDestination(WowMapCoordinateLRDTUnity position)
    {
        m_whereToGo = position;
    }
    public void SetCurrentPosition(PixelBotCoordinateRaw position)
    {
        m_playerPosition = position;
    }

    public void SetAsActive(bool isActive)
    {
        m_isActive = isActive;
    }
    public void SwitchActive()
    {
        m_isActive = !m_isActive;
    }


    public float m_maxAngle = 5f;
    public float m_closeEnought = 0.3f;

    public bool m_isActive;
    public bool m_isArrived;

    public bool m_disableIfArrived=true;
    public UnityEvent m_isArrivedEvent;
    public DefaultBooleanChangeListener m_isArrivedListener;
    void Update()
    {
        m_signeAngle = WowMapUtility.GetSigneAngle(m_whereToGo, m_playerPosition);
        m_distanceBetween = WowMapUtility.GetDistanceBetween(m_whereToGo, m_playerPosition);

        if (m_isActive)
        {
            m_isArrived = m_distanceBetween < m_closeEnought;
            m_isArrivedListener.SetBoolean(m_isArrived);
            if (m_disableIfArrived && m_isArrived) {
                m_isActive = false;
            }
            m_playerInteraction.SetMovingForward(!m_isArrived);

            if (!m_isArrived)
            {
                if (m_signeAngle < -m_maxAngle & m_signeAngle > -180f)
                    m_playerInteraction.SetTurnLeft(true);
                else
                    m_playerInteraction.SetTurnLeft(false);

                if (m_signeAngle > m_maxAngle & m_signeAngle <= 180f)
                    m_playerInteraction.SetTurnRight(true);
                else
                    m_playerInteraction.SetTurnRight(false);
            }


        }


    }

   

    public Vector2 pointDirection;
    public Vector2 forwardDirection;

    
}

public class WowMapUtility
{

    public static void GetDirectionSegment(WowMapCoordinateLRDTUnity from, WowMapCoordinateLRDTUnity to, out Vector2 direction)
    {
        direction = new Vector2();
        direction.x = to.m_xLeftRight - from.m_xLeftRight;
        direction.y = to.m_yDownTop - from.m_yDownTop;


    }
    public static void GetMapDirection(WowMapDirectionLRDTUnity from, WowMapDirectionLRDTUnity to, out float directionAsAngle)
    {
        from.GetDirectionAsVector2(out Vector2 dirPrevious);
        to.GetDirectionAsVector2(out Vector2 dirCurrent);
        directionAsAngle = Vector2.SignedAngle(dirPrevious.normalized, dirCurrent.normalized);
    }

    public static float GetDistanceBetween(WowMapCoordinateLRDTUnity pointA, PixelBotCoordinateRaw pointB)
    {
        return GetDistanceBetween(pointA, pointB.m_position);
    }
    public static float GetDistanceBetween(WowMapCoordinateLRDTUnity pointA, WowMapCoordinateLRDTUnity pointB)
    {
        pointA.GetAsVector2(out Vector2 positionA);
        pointB.GetAsVector2(out Vector2 positionB);
        return Vector2.Distance(positionA, positionB);
    }


    public static float GetSigneAngle(WowMapCoordinateLRDTUnity whereToGo, PixelBotCoordinateRaw playerPosition)
    {
        WowMapUtility.GetDirectionSegment(playerPosition.m_position, playerPosition.GetForwardPoint(1), out Vector2 forwardDirection);
        WowMapUtility.GetDirectionSegment(playerPosition.m_position, whereToGo, out Vector2 pointDirection);
        forwardDirection.Normalize(); pointDirection.Normalize();
        return -Vector2.SignedAngle(forwardDirection, pointDirection);

    }

    public static void Convert(WowMapCoordinateLRTDOrigin originePoint, out WowMapCoordinateLRDTUnity unityPoint)
    {
        unityPoint = new WowMapCoordinateLRDTUnity(originePoint.m_xLeftRight, 100f - originePoint.m_yTopDown);
    }
}
