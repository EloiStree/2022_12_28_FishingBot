using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Test_FishingPostMessageClickMono : MonoBehaviour
{

    public void SetFishingAsActive(bool active)
    {
        m_fishManager.m_fishingConfig.m_isActive = active;
    }
    public void SwitchFishingActivity()
    {
        m_fishManager.m_fishingConfig.m_isActive = !m_fishManager.m_fishingConfig.m_isActive;
    }

    public void SetMouseTypeAsRealMouse(bool useRealMouse)
    {
        if (useRealMouse)
            SetMouseTypeAsRealMouse();
        else
            SetMouseTypeAsPostMessageMouse();
    }
    public void SetMouseTypeAsRealMouse() =>
        m_fishManager.m_useRealMouse = true;

    public void SetMouseTypeAsPostMessageMouse() =>
        m_fishManager.m_useRealMouse = false;

    public void SetNumberOfClick(int clickCount)
    {
        m_fishManager.m_fishingConfig.m_numberOfClick = clickCount;
    }


    public void SetPourcentScreenHorizontal(float percentHorizontal)
    {
        m_fishManager.m_fishingConfig.m_pourcentOfScreen = percentHorizontal;
    }
    public void SetPourcentVerticalBotToTop(float bot2TopPercent)
    {
        m_fishManager.m_fishingConfig.m_yBotToTop = bot2TopPercent;

    }


    public void SetTimeForFocus(float timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeToSwitchWindowMs = (int)timeInMs;
    }
    public void SetTimeBetweenClicks(float timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenClicksMs = (int)timeInMs;
    }
    public void SetPressingTime(float timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timePressedClicksMs = (int)timeInMs;
    }
    public void SetBeforeTime(float timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeToLetFinishPreviousAction = (int)timeInMs;
    }

    public IntPtrTemp m_processTarget;
    public FishingByPostMessageClicker m_fishManager = new FishingByPostMessageClicker();

    [ContextMenu("Launch the Line")]
    public void LaunchTheLineWithPreviousProcess()
    {
        m_fishManager.LaunchTheLine(m_processTarget);
    }
    [ContextMenu("Launch the Line")]
    public void LaunchTheLineWithProcess(IntPtrWrapGet processId)
    {
        m_processTarget.Set(processId.GetAsInt());
        m_fishManager.LaunchTheLine(processId);
    }
    [ContextMenu("Recovert the line")]
    public void RecovertTheLinePreviousProcess()
    {
        m_fishManager.RecovertTheLineThread(m_processTarget);
    }
    [ContextMenu("Recovert the line")]
    public void RecovertTheLineWithProcessId(IntPtrWrapGet processId)
    {
        m_processTarget.Set(processId.GetAsInt());
        m_fishManager.RecovertTheLineThread(processId);
    }
}


[System.Serializable]
public class FishingByPostMessageClicker
{
    public Eloi.StringClampHistory m_acitonDebug = new StringClampHistory() { m_historySize = 50 };
    public FishingLineConfiguration m_fishingConfig = new FishingLineConfiguration();
    [System.Serializable]
    public class FishingLineConfiguration
    {
        [Range(0f, 1f)]
        public float m_yBotToTop = 0.5f;
        [Range(0f, 1f)]
        public float m_pourcentOfScreen = 0.7f;
        [Range(1, 50)]
        public int m_numberOfClick = 15;
        public int m_timeBetweenClicksMs = 80;
        public int m_timePressedClicksMs = 3;
        public int m_timeToSwitchWindowMs = 150;
        public int m_timeToLetFinishPreviousAction = 15;
        public int m_moveInPrevisionAfterMs = 15;
        public bool m_isActive = true;
    }

    public bool m_useRealMouse;

    [Header("Debug info")]
    Left2RightPercent01 m_xLeftToRightRelative = new Left2RightPercent01();
    Right2LeftPercent01 m_xRightToLeftRelative = new Right2LeftPercent01();
    Bot2TopPercent01 m_yBotToTopRelative = new Bot2TopPercent01();
    public DeductedInfoOfWindowSizeWithSource m_deductedInfoTarget;

    public long m_recallTimeInMs;
    public bool m_useForground;

    public User32PostMessageKeyEnum m_keyToFish;
    public string m_scriptToResetView = "/run SetView(1)";
    public string m_scriptToSaveView = "/run SaveView(1)";
    public User32PostMessageKeyEnum m_keyToResetView= User32PostMessageKeyEnum.VK_HOME;
    public float m_randomJump = 0.05f;
    public void LaunchTheLine(IntPtrWrapGet processId)
    {
        if (m_fishingConfig.m_isActive)
        {

            Eloi.E_UnityRandomUtility.GetRandomN2M(0f, 1f, out float r);
            if (r < m_randomJump)
            {
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
                {
                    SendKeyMessageToWindows.SendKeyDown(User32PostMessageKeyEnum.VK_SPACE, processId, true);
                    SendKeyMessageToWindows.SendKeyUp(User32PostMessageKeyEnum.VK_SPACE, processId, true);
                });
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(900, () =>
                {
                    m_acitonDebug.PushIn("Send Post Down ");
                    SendKeyMessageToWindows.SendKeyDown(m_keyToFish, processId, true);
                });
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(1000, () =>
                {
                    m_acitonDebug.PushIn("Send Post Up ");
                    SendKeyMessageToWindows.SendKeyUp(m_keyToFish, processId, true);
                });
            }
            else
            {

                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                    m_acitonDebug.PushIn("Send Post Down ");
                    SendKeyMessageToWindows.SendKeyDown(m_keyToFish, processId, true);
                });
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(10, () =>
                {
                    m_acitonDebug.PushIn("Send Post Up ");
                    SendKeyMessageToWindows.SendKeyUp(m_keyToFish, processId, true);
                });
            }
        }
    }
    public User32RelativePixelPointLRTB[] m_points;
    public void RecovertTheLineThread(IntPtrWrapGet processId)
    {
        if (m_fishingConfig.m_isActive)
        {


            FetchWindowInfoUtility.Get(processId, out m_deductedInfoTarget);
            m_yBotToTopRelative.SetPercent(m_fishingConfig.m_yBotToTop);
            m_xLeftToRightRelative.SetPercent((1f - m_fishingConfig.m_pourcentOfScreen) / 2f);
            m_xRightToLeftRelative.SetPercent((1f - m_fishingConfig.m_pourcentOfScreen) / 2f);

            User32WindowRectUtility.HorizontalLineLeftRightClick(in m_deductedInfoTarget,
                m_yBotToTopRelative,
                m_xLeftToRightRelative,
                m_xRightToLeftRelative,
                m_fishingConfig.m_numberOfClick,
                out m_points
                 );

            User32RelativePointsActionPusher.PointsListOfPressReleaseActions(
                m_deductedInfoTarget.m_pointer,
                StartTheWatch,
                LeftClickDown,
                LeftClickUp,
                MoveTo,
                StopTheWatch,
                m_points,
                out int msAtEnd,
                m_fishingConfig.m_timeToSwitchWindowMs,
                m_fishingConfig.m_timeBetweenClicksMs,
                m_fishingConfig.m_timePressedClicksMs,
                m_fishingConfig.m_timeToLetFinishPreviousAction,
                m_fishingConfig.m_moveInPrevisionAfterMs
                ); ;;
        }
    }

    private void MoveTo(IntPtrWrapGet pointer, User32RelativePixelPointLRTB point)
    {
        if (m_fishingConfig.m_isActive)
        {

            if (m_useRealMouse)
            {

                m_deductedInfoTarget.m_frameSize.GetAbsoluteFromRelativePixelLeft2Right(point.m_pixelLeft2Right, out int x);
                m_deductedInfoTarget.m_frameSize.GetAbsoluteFromRelativePixelTop2Bot(point.m_pixelTop2Bot, out int y);
                m_px = x;
                m_py = y;

                m_acitonDebug.PushIn("Move Mouse A "+x+" "+y);
                MouseOperations.SetCursorPosition( x, y);
                
            }
            else
            {
                m_acitonDebug.PushIn("Move Post R "+ point.m_pixelLeft2Right + " "+ point.m_pixelTop2Bot);
                PostMouseUtility.MoveTo(
                         pointer, point.m_pixelLeft2Right
                         , point.m_pixelTop2Bot, false,true);
               
            }
        }
    }

    private Stopwatch m_watch;

    private void StopTheWatch(IntPtrWrapGet pointer)
    {
        if (m_fishingConfig.m_isActive)
        {

            m_watch.Stop();
            m_recallTimeInMs = m_watch.ElapsedMilliseconds;
            LaunchTheLine(pointer);
            ResetView(pointer);
        }
    }

    private void ResetView(IntPtrWrapGet processId)
    {
        if (m_fishingConfig.m_isActive)
        {

           
            {

                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                    m_acitonDebug.PushIn("Send Post Down ");
                    SendKeyMessageToWindows.SendKeyDown(m_keyToResetView, processId, true);
                });
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(10, () =>
                {
                    m_acitonDebug.PushIn("Send Post Up ");
                    SendKeyMessageToWindows.SendKeyUp(m_keyToResetView, processId, true);
                });

                if (m_scriptToResetView.Length > 0) {


                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(200, () =>
                    {

                        if (m_useRealMouse)
                            SendKeyStrokeToComputerDLL.KeyPressAndRelease(User32KeyboardStrokeCodeEnum.ENTER);
                        else
                            SendKeyMessageToWindows.SendKeyClick(User32PostMessageKeyEnum.VK_ACCEPT, processId, true);

                    });


                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(250 ,() =>
                    {
                        m_acitonDebug.PushIn("Copy past ");
                        User32ClipboardUtility.CopyTextToClipboard(m_scriptToResetView);

                        if (m_useRealMouse)
                            SendKeyStrokeToComputerDLL.Past(true);
                        else SendKeyMessageToWindows.RequestPastActionBroadcast(processId, true);

                    });
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(300, () =>
                    {

                        if (m_useRealMouse)
                            SendKeyStrokeToComputerDLL.KeyPressAndRelease(User32KeyboardStrokeCodeEnum.ENTER);
                        else
                            SendKeyMessageToWindows.SendKeyClick(User32PostMessageKeyEnum.VK_ACCEPT, processId, true);

                    });


                }

            }
        }
    }

    public int m_px, m_py;
    public int m_nx, m_ny;
    private void LeftClickDown(IntPtrWrapGet pointer, User32RelativePixelPointLRTB point)
    {
        if (m_fishingConfig.m_isActive)
        {

            if (m_useRealMouse)
            {

                m_deductedInfoTarget.m_frameSize.GetAbsoluteFromRelativePixelLeft2Right(point.m_pixelLeft2Right, out int x);
                m_deductedInfoTarget.m_frameSize.GetAbsoluteFromRelativePixelTop2Bot(point.m_pixelTop2Bot, out int y);
                m_px = x;
                m_py = y;

                m_acitonDebug.PushIn("Send Mouse Down ");
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown,x,y);
                if (m_fishingConfig.m_timePressedClicksMs <= 0f)
                {
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp,x,y);
                    m_acitonDebug.PushIn("Send Mouse UP Direct ");
                }
            }
            else
            {
                m_acitonDebug.PushIn("Send Post Down ");
                PostMouseUtility.SendMouseLeftDownDirect(
                         pointer, point.m_pixelLeft2Right
                         , point.m_pixelTop2Bot, false);
                if (m_fishingConfig.m_timePressedClicksMs <= 0f)
                {
                    PostMouseUtility.SendMouseLeftUpDirect(
                             pointer, point.m_pixelLeft2Right
                             , point.m_pixelTop2Bot, false);
                    m_acitonDebug.PushIn("Send Post UP Direct ");
                }
            }
        }
    }

    private void LeftClickUp(IntPtrWrapGet pointer, User32RelativePixelPointLRTB point)
    {
        if (m_fishingConfig.m_isActive)
        {

            if (m_fishingConfig.m_timePressedClicksMs > 0f)
            {
                if (m_useRealMouse)
                {

                    m_acitonDebug.PushIn("Send Mouse Up ");
                    m_deductedInfoTarget.m_frameSize.GetAbsoluteFromRelativePixelLeft2Right(point.m_pixelLeft2Right, out int x);
                    m_deductedInfoTarget.m_frameSize.GetAbsoluteFromRelativePixelTop2Bot(point.m_pixelTop2Bot, out int y);
                    m_nx = x;
                    m_ny = y;
                    if (m_px != m_nx || m_py != m_ny)
                        throw new Exception("Aha !!!");

                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp, x,y);
                }
                else
                {
                    m_acitonDebug.PushIn("Send Post up  Direct ");
                    PostMouseUtility.SendMouseLeftUpDirect(
                             pointer, point.m_pixelLeft2Right
                             , point.m_pixelTop2Bot, false);
                }
            }
        }
    }


    private void StartTheWatch(IntPtrWrapGet pointer)
    {
        if (m_fishingConfig.m_isActive)
        {

            if (m_watch == null)
                m_watch = new Stopwatch();
            m_watch.Restart();
            //WindowIntPtrUtility.SetForegroundWindow(pointer);
        }
    }
}


public class User32RelativePointsActionPusher
{

    private readonly static object threadLock = new object();
    public delegate void MouseActionBasedOnRelativePoints(IntPtrWrapGet pointer, User32RelativePixelPointLRTB point);
    public delegate void MouseActionBasedOnRelativeKey(IntPtrWrapGet pointer);
    public static void PointsListOfPressReleaseActions(
        IntPtrWrapGet pointer,
        MouseActionBasedOnRelativeKey toDoAtTheStart,
        MouseActionBasedOnRelativePoints actionDown,
        MouseActionBasedOnRelativePoints actionUp,
        MouseActionBasedOnRelativePoints moveTo,
        MouseActionBasedOnRelativeKey toDoAtTheEnd,
        User32RelativePixelPointLRTB[] points,
        out int msCountAtEnd,
        int forgroundMsWait = 150,
        int betweenActionMsWait = 90,
        int pressActionMsWait = 0,
        int tempTimeBeforeStartMs = 0,
        int previsionMoveAfter = 30,
        bool useForgroundAtStart = true
        )
    {
        lock (threadLock) 
        { 
        int ms = tempTimeBeforeStartMs;
        if (useForgroundAtStart)
        {
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                WindowIntPtrUtility.SetForegroundWindow(pointer);
            });

            ms += forgroundMsWait;
        }

        if (toDoAtTheStart != null)
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                toDoAtTheStart(pointer);
            });
        for (int i = 0; i < points.Length; i++)
            {
            User32RelativePixelPointLRTB p = new User32RelativePixelPointLRTB(points[i].m_pixelLeft2Right, points[i].m_pixelTop2Bot);

                if (i == 0)
                {
                    ms += (previsionMoveAfter);
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                        if (moveTo != null)
                            moveTo(pointer, p);
                    });
                    ms += (previsionMoveAfter);
                }

            ms += betweenActionMsWait;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                if (actionDown != null)
                    actionDown(pointer, p);
            });
            ms += pressActionMsWait;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                if (actionUp != null)
                    actionUp(pointer, p);
            });
            ms +=  (previsionMoveAfter);
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                if (moveTo != null)
                    moveTo(pointer, p);
            });
            ms += (previsionMoveAfter );


        }
        ms += betweenActionMsWait;
        if (toDoAtTheEnd != null)
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {

                toDoAtTheEnd(pointer);
            });
        msCountAtEnd = ms;
        }

    }
}



public class User32WindowRectUtility
{

    public static void RefreshInfoOf(ref DeductedInfoOfWindowSizeWithSource rectInfo)
    {
        WindowIntPtrUtility.RefreshInfoOf(ref rectInfo);
    }

    public static void HorizontalLineLeftRightClick(
        in DeductedInfoOfWindowSizeWithSource rectInfo,
        in Bot2TopPercent01 marginDown,
        in Left2RightPercent01 marginLeft,
        in Right2LeftPercent01 marginRight,
        int pointCount,
        out User32RelativePixelPointLRTB[] points
        )
    {
        points = new User32RelativePixelPointLRTB[pointCount];
        rectInfo.m_frameSize.GetBottomToTopToRelative(marginDown.GetPercent(), out int y);
        rectInfo.m_frameSize.GetLeftToRightToRelative((marginLeft.GetPercent()), out int xl);
        rectInfo.m_frameSize.GetRightToLeftToRelative((marginRight.GetPercent()), out int xr);
        for (int i = 0; i < pointCount; i++)
        {
            float percent = i / (float)pointCount;
            points[i] = new User32RelativePixelPointLRTB((int)Mathf.Lerp(xl, xr, percent), y);
        }
    }
    public static void VerticalLineBotTopClick(in DeductedInfoOfWindowSizeWithSource rectInfo, in Left2RightPercent01 marginLeft, in Bot2TopPercent01 marginDown, in Right2LeftPercent01 marginTop)
    {
        Eloi.E_ThrowException.ThrowNotImplemented();
    }

    public enum HeightMeasure { BasedOnWidth, BasedOnHeight, BasedOnMagnitude }
    public static void RandomSphereClickAroundPoint(in DeductedInfoOfWindowSizeWithSource rectInfo,
        in Left2RightPercent01 marginLeft,
        in Bot2TopPercent01 marginDown,
        in Percent01 screenPercent,
        HeightMeasure heightMeasure,
        int pointCount,
        out User32RelativePixelPointLRTB[] points)
    {
        rectInfo.m_frameSize.GetBottomToTopToRelative(marginDown.GetPercent(), out int y);
        rectInfo.m_frameSize.GetLeftToRightToRelative((marginLeft.GetPercent()), out int x);
        int radiusInPixel = 1;
        if (heightMeasure == HeightMeasure.BasedOnWidth)
            radiusInPixel = (int)(rectInfo.m_frameSize.m_innerWidth * screenPercent.GetPercent());
        if (heightMeasure == HeightMeasure.BasedOnHeight)
            radiusInPixel = (int)(rectInfo.m_frameSize.m_innerHeight * screenPercent.GetPercent());

        points = new User32RelativePixelPointLRTB[pointCount];
        for (int i = 0; i < points.Length; i++)
        {
            Eloi.E_UnityRandomUtility.GetRandomN2M(-radiusInPixel, radiusInPixel, out int rx);
            Eloi.E_UnityRandomUtility.GetRandomN2M(-radiusInPixel, radiusInPixel, out int ry);
            points[i].m_pixelLeft2Right = x + rx;
            points[i].m_pixelTop2Bot = y + ry;
        }
    }
}

[System.Serializable]
public struct User32RelativePixelPointLRTB
{
    public int m_pixelLeft2Right;
    public int m_pixelTop2Bot;

    public User32RelativePixelPointLRTB(int pixelLeft2Right, int pixelTop2Bot)
    {
        m_pixelLeft2Right = pixelLeft2Right;
        m_pixelTop2Bot = pixelTop2Bot;
    }
}