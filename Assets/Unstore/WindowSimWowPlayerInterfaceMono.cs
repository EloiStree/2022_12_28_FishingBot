using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSimWowPlayerInterfaceMono : AbstractWowPlayerInterfaceMono
{
    public WindowSimWriter m_window;
    public KeyboardTouch m_autoRun= KeyboardTouch.WinUS_OEM_7_Quote;
    public KeyboardTouch m_forward=KeyboardTouch.Z;
    public KeyboardTouch m_backward = KeyboardTouch.S;
    public KeyboardTouch m_turnLeft = KeyboardTouch.Q;
    public KeyboardTouch m_turnRight = KeyboardTouch.D;
    public KeyboardTouch m_strafeLeft = KeyboardTouch.W;
    public KeyboardTouch m_strafeRight = KeyboardTouch.X;
    public KeyboardTouch m_jumpGoUp= KeyboardTouch.Space;
    public KeyboardTouch m_goDown= KeyboardTouch.C;

    //public void SetMovingForward(bool moveForward);
    public override void ActivateAutoRun(bool autoRun)
    {
        Press(autoRun, m_autoRun);
    }

    private void Press(bool isOn, KeyboardTouch touch)
    {
        if (isOn)
            m_window.RealPressDown(touch);
        else
            m_window.RealPressUp(touch);
    }

    public override void GoDown(bool goDown)
    {
        Press(goDown, m_goDown);
    }

  
    public override void Jump(bool jumpOn)
    {
        Press(jumpOn, m_jumpGoUp);
    }

    public override void SetMovingBackward(bool moveBackward)
    {
        Press(moveBackward, m_backward);
    }

    public override void SetMovingForward(bool moveForward)
    {
        Press(moveForward, m_forward);
    }

    public override void SetStrafeLeft(bool strafeLeft)
    {
        Press(strafeLeft, m_strafeLeft);
    }

    public override void SetStrafeRight(bool strafeRight)
    {
        Press(strafeRight, m_strafeRight);
    }

    public override void SetTurnLeft(bool turnLeft)
    {
        Press(turnLeft, m_turnLeft);
    }

    public override void SetTurnRight(bool turnRight)
    {
        Press(turnRight, m_turnRight);
    }
    public void ReleaseAll() {

        SetMovingForward(false );
        SetMovingBackward(false);
        SetTurnLeft(false);
        SetTurnRight(false);
        SetStrafeLeft(false);
        SetStrafeRight(false);
        Jump(false);
        GoDown(false);
        ActivateAutoRun(false);
    }
}


public abstract class AbstractWowPlayerInterfaceMono : MonoBehaviour {

    public abstract void SetMovingForward(bool moveForward);
    public abstract void SetMovingBackward(bool moveBackward);
    public abstract void SetTurnLeft(bool turnLeft);
    public abstract void SetTurnRight(bool turnRight);
    public abstract void SetStrafeLeft(bool strafeLeft);
    public abstract void SetStrafeRight(bool strafeRight);
    public abstract void Jump(bool jumpOn);
    public abstract void GoDown(bool goDown);
    public abstract void ActivateAutoRun(bool autoRun);
}