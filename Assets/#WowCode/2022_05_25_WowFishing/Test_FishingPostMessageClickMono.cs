using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Test_FishingPostMessageClickMono : MonoBehaviour
{


    public void SetUseOfCameraViewes(bool useCameraViewes) {

        //Dirty code
        User32RelativePointsActionPusher.m_useCameraView = useCameraViewes;
    }

    public void SetFishingAsActive(bool active)
    {
        m_fishManager.m_fishingConfig.m_isActive = active;
    }
    public void SwitchFishingActivity()
    {
        m_fishManager.m_fishingConfig.m_isActive = !m_fishManager.m_fishingConfig.m_isActive;
    }


    public void SetAsHyperOcean(bool isHyperocean)
    {
        m_fishManager.m_fishingConfig.m_isFishingHyperOcean = isHyperocean; 
    }

    public void SetTimeBetweenCameraMove(float timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenCameraMoveMs = (int)timeInMs;
    }
    public void SetPressingTime(float timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenPressMs = (int)timeInMs;
    }
    public void SetTimeBeforeLineRelaunch(float timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenLineRelaunchMs = (int)timeInMs;
    }
    public void SetTimeBetweenCameraMove(int timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenCameraMoveMs = (int)timeInMs;
    }
    public void SetPressingTime(int timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenPressMs = (int)timeInMs;
    }
    public void SetTimeBeforeLineRelaunch(int timeInMs)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenLineRelaunchMs = (int)timeInMs;
    }


    public void SetRandomness(float rangeInMsToRandomize)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenRandomRangeMs = (int)rangeInMsToRandomize;
    }
    public void SetRandomness(int rangeInMsToRandomize)
    {
        m_fishManager.m_fishingConfig.m_timeBetweenRandomRangeMs = (int)rangeInMsToRandomize;
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
    public void LaunchTheLine(int processId)
    {
        LaunchTheLineWithProcess(IntPtrProcessId.Int(processId));
    }
    public void RecovertTheLineThread(int processId)
    {
       RecovertTheLineWithProcessId(IntPtrProcessId.Int(processId));
    }
    public UnityEvent m_onRecovertFishStart;
    public void Update()
    {
        if (m_fishManager.m_notifyRecoveredLaneOnUnityThread) {
            m_fishManager.m_notifyRecoveredLaneOnUnityThread = false;
            m_onRecovertFishStart.Invoke();
        }
    }
}


[System.Serializable]
public class FishingByPostMessageClicker
{
    public ThreadQueueDateTimeCallMono m_threadToSendActions;
    public Eloi.StringClampHistory m_acitonDebug = new StringClampHistory() { m_historySize = 50 };
    public FishingLineConfiguration m_fishingConfig = new FishingLineConfiguration();


    [System.Serializable]
    public class FishingLineConfiguration
    {
      
        public int m_timeBetweenCameraMoveMs = 800;
        public int m_timeBetweenPressMs = 80;
        public int m_timeBetweenLineRelaunchMs = 500;
        public bool m_isActive = true;
        internal int m_timeBetweenRandomRangeMs;
        public bool m_isFishingHyperOcean;
    }


    [Header("Debug info")]
    public long m_recallTimeInMs;

    public User32PostMessageKeyEnum m_keyToFish = User32PostMessageKeyEnum.VK_DIVIDE;
    public User32PostMessageKeyEnum m_keyToCancelCurrent = User32PostMessageKeyEnum.VK_SPACE;
    public User32PostMessageKeyEnum m_keyToDejunk = User32PostMessageKeyEnum.VK_K;
    public User32PostMessageKeyEnum m_keyToInteract = User32PostMessageKeyEnum.VK_SUBTRACT;
    public string m_scriptToResetView = "/run SetView(1)";
    public string m_scriptToSaveView = "/run SaveView(1)";
    public User32PostMessageKeyEnum[] m_keyToResetView = new User32PostMessageKeyEnum[] { User32PostMessageKeyEnum.VK_F1 , User32PostMessageKeyEnum.VK_F2, User32PostMessageKeyEnum.VK_F3};
    public float m_randomJump = 0.05f;

    public bool m_notifyRecoveredLaneOnUnityThread;
    public bool m_notifyLineLaunchLaneOnUnityThread;
    public void LaunchTheLine(IntPtrWrapGet processId)
    {
        if (processId.GetAsInt() == 0) return;
        if (m_fishingConfig.m_isActive)
        {
            //m_lineLaunched.Invoke();

            Eloi.E_UnityRandomUtility.GetRandomN2M(0f, 1f, out float r);
            if (r < m_randomJump)
            {
                m_threadToSendActions.AddFromNowInMs(0, () =>
                {
                    SendKeyMessageToWindows.SendKeyDown(User32PostMessageKeyEnum.VK_SPACE, processId, true);
                    SendKeyMessageToWindows.SendKeyUp(User32PostMessageKeyEnum.VK_SPACE, processId, true);
                });
                m_threadToSendActions.AddFromNowInMs(900, () =>
                {
                    m_acitonDebug.PushIn("Send Post Down ");
                    if(m_fishingConfig.m_isFishingHyperOcean)
                        SendKeyMessageToWindows.SendKeyDown(m_keyToInteract, processId, true);
                    else 
                        SendKeyMessageToWindows.SendKeyDown(m_keyToFish, processId, true);
                });
                m_threadToSendActions.AddFromNowInMs(1000, () =>
                {
                    m_acitonDebug.PushIn("Send Post Up ");
                    if (m_fishingConfig.m_isFishingHyperOcean)
                        SendKeyMessageToWindows.SendKeyUp(m_keyToInteract, processId, true);
                    else
                        SendKeyMessageToWindows.SendKeyUp(m_keyToFish, processId, true);
                });
            }
            else
            {

                m_threadToSendActions.AddFromNowInMs(0, () => {
                    m_acitonDebug.PushIn("Send Post Down ");

                    if (m_fishingConfig.m_isFishingHyperOcean)
                        SendKeyMessageToWindows.SendKeyDown(m_keyToInteract, processId, true);
                    else 
                        SendKeyMessageToWindows.SendKeyDown(m_keyToFish, processId, true);
                });
                m_threadToSendActions.AddFromNowInMs(10, () =>
                {
                    m_acitonDebug.PushIn("Send Post Up ");
                    if (m_fishingConfig.m_isFishingHyperOcean)
                        SendKeyMessageToWindows.SendKeyUp(m_keyToInteract, processId, true);
                    else 
                        SendKeyMessageToWindows.SendKeyUp(m_keyToFish, processId, true);
                });
            }
            m_notifyLineLaunchLaneOnUnityThread = true;
        }
    }
    
    public void RecovertTheLineThread(IntPtrWrapGet processId)
    {
        if (m_fishingConfig.m_isActive)
        {


            FetchWindowInfoUtility.Get(processId, out DeductedInfoOfWindowSizeWithSource m_deductedInfoTarget);
            //m_yBotToTopRelative.SetPercent(m_fishingConfig.m_yBotToTop);
            //m_xLeftToRightRelative.SetPercent((1f - m_fishingConfig.m_pourcentOfScreen) / 2f);
            //m_xRightToLeftRelative.SetPercent((1f - m_fishingConfig.m_pourcentOfScreen) / 2f);

            //User32WindowRectUtility.HorizontalLineLeftRightClick(in m_deductedInfoTarget,
            //    m_yBotToTopRelative,
            //    m_xLeftToRightRelative,
            //    m_xRightToLeftRelative,
            //    m_fishingConfig.m_numberOfClick,
            //    out m_points
            //     );

            //User32RelativePointsActionPusher.PointsListOfPressReleaseActions(
            //    m_deductedInfoTarget.m_pointer,
            //    m_threadToSendActions,
            //    StartTheWatch,
            //    LeftClickDown,
            //    LeftClickUp,
            //    MoveTo,
            //    StopTheWatch,
            //    m_points,
            //    out int msAtEnd,
            //    m_fishingConfig.m_timeToSwitchWindowMs,
            //    m_fishingConfig.m_timeBetweenClicksMs,
            //    m_fishingConfig.m_timePressedClicksMs,
            //    m_fishingConfig.m_timeToLetFinishPreviousAction,
            //    m_fishingConfig.m_moveInPrevisionAfterMs
            //    ); ;;

            User32RelativePointsActionPusher.RecovertFishWithAutoInteract(
               m_deductedInfoTarget.m_pointer,
                m_threadToSendActions,
                m_keyToFish,
                m_keyToInteract,
                m_keyToCancelCurrent,
                m_keyToDejunk,
                m_keyToResetView,
                out int msAtEnd,
                m_fishingConfig.m_isFishingHyperOcean,
                m_fishingConfig.m_timeBetweenCameraMoveMs,
                m_fishingConfig.m_timeBetweenPressMs,
                m_fishingConfig.m_timeBetweenLineRelaunchMs,
                m_fishingConfig.m_timeBetweenRandomRangeMs
                );

            m_notifyRecoveredLaneOnUnityThread = true;
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

                //m_threadToSendActions.AddFromNowInMs(0, () => {
                //    m_acitonDebug.PushIn("Send Post Down ");
                //    SendKeyMessageToWindows.SendKeyDown(m_keyToResetView, processId, true);
                //});
                //m_threadToSendActions.AddFromNowInMs(10, () =>
                //{
                //    m_acitonDebug.PushIn("Send Post Up ");
                //    SendKeyMessageToWindows.SendKeyUp(m_keyToResetView, processId, true);
                //});

                if (m_scriptToResetView.Length > 0) {


                    m_threadToSendActions.AddFromNowInMs(200, () =>
                    {
                            SendKeyMessageToWindows.SendKeyClick(User32PostMessageKeyEnum.VK_ACCEPT, processId, true);

                    });


                    m_threadToSendActions.AddFromNowInMs(250 ,() =>
                    {
                        m_acitonDebug.PushIn("Copy past ");
                        User32ClipboardUtility.CopyTextToClipboard(m_scriptToResetView);

                         SendKeyMessageToWindows.RequestPastActionBroadcast(processId, true);

                    });
                    m_threadToSendActions.AddFromNowInMs(300, () =>
                    {
                            SendKeyMessageToWindows.SendKeyClick(User32PostMessageKeyEnum.VK_ACCEPT, processId, true);

                    });


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
    public delegate void KeyPostAction(IntPtrWrapGet pointer, User32PostMessageKeyEnum postKey, float timeBetweenSeconds);

    public static void PointsListOfPressReleaseActions(
        IntPtrWrapGet pointer,
        ThreadQueueDateTimeCallMono threadToUse,
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
                threadToUse.AddFromNowInMs(ms, () => {
                    WindowIntPtrUtility.SetForegroundWindow(pointer);
                });

                ms += forgroundMsWait;
            }

            if (toDoAtTheStart != null)
                threadToUse.AddFromNowInMs(ms, () => {
                    toDoAtTheStart(pointer);
                });
            for (int i = 0; i < points.Length; i++)
            {
                User32RelativePixelPointLRTB p = new User32RelativePixelPointLRTB(points[i].m_pixelLeft2Right, points[i].m_pixelTop2Bot);

                if (i == 0)
                {
                    ms += (previsionMoveAfter);
                    threadToUse.AddFromNowInMs(ms, () => {
                        if (moveTo != null)
                            moveTo(pointer, p);
                    });
                    ms += (previsionMoveAfter);
                }

                ms += betweenActionMsWait;
                threadToUse.AddFromNowInMs(ms, () => {
                    if (actionDown != null)
                        actionDown(pointer, p);
                });
                ms += pressActionMsWait;
                threadToUse.AddFromNowInMs(ms, () => {
                    if (actionUp != null)
                        actionUp(pointer, p);
                });
                ms += (previsionMoveAfter);
                threadToUse.AddFromNowInMs(ms, () => {
                    if (moveTo != null)
                        moveTo(pointer, p);
                });
                ms += (previsionMoveAfter);


            }
            ms += betweenActionMsWait;
        
            if (useForgroundAtStart)
            {
                ms += forgroundMsWait;
                threadToUse.AddFromNowInMs(ms, () => {
                    WindowIntPtrUtility.SetForegroundWindow(pointer);
                });

                ms += forgroundMsWait;
            }
            ms += betweenActionMsWait;
            if (toDoAtTheEnd != null)
                threadToUse.AddFromNowInMs(ms, () => {

                    toDoAtTheEnd(pointer);
                });
            msCountAtEnd = ms;
        }

    }
    public static bool m_useCameraView;
    internal static void RecovertFishWithAutoInteract(
        IntPtrWrapGet pointer,
        ThreadQueueDateTimeCallMono threadToUse,
        User32PostMessageKeyEnum fishSendLineKey,
        User32PostMessageKeyEnum fishInteractKey,
        User32PostMessageKeyEnum fishCancelKey, 
        User32PostMessageKeyEnum fishDejunkKey,
        User32PostMessageKeyEnum[] cameraViewKey,

        out int msCountAtEnd,
        bool isHyperOcean,
        int timeBetweenCameraMoveMs = 250,
        int timeBetweenPressMs = 10,
        int timeBeforeRelaunchLineMs = 150,
        int randomnessBetweenActionsRange = 20
        )
    {
       // WindowIntPtrUtility.SetForegroundWindow(pointer);
        msCountAtEnd = 0;
        {
            lock (threadLock)
            {
                int ms = 0;


                if (m_useCameraView)
                {
                    for (int i = 0; i < cameraViewKey.Length; i++)
                    {


                        threadToUse.AddFromNowInMs(ms, () =>
                        {

                            User32KeyStrokeManager.SendKeyPostMessage(pointer, fishInteractKey, User32PressionType.Press);
                        });
                        ms += timeBetweenPressMs;

                        ms += UnityEngine.Random.Range(0, randomnessBetweenActionsRange);

                        threadToUse.AddFromNowInMs(ms, () =>
                        {

                            User32KeyStrokeManager.SendKeyPostMessage(pointer, fishInteractKey, User32PressionType.Release);
                        });
                        ms += 5;

                        PressKeyView(pointer, threadToUse, cameraViewKey[i], ms);
                        ms += timeBetweenPressMs;
                        ms += UnityEngine.Random.Range(0, randomnessBetweenActionsRange);
                        ReleaseKeyView(pointer, threadToUse, cameraViewKey[i], ms);

                        ms += (timeBetweenCameraMoveMs);
                        threadToUse.AddFromNowInMs(ms, () =>
                        {
                            User32KeyStrokeManager.SendKeyPostMessage(pointer, fishInteractKey, User32PressionType.Press);
                        });
                        ms += UnityEngine.Random.Range(0, randomnessBetweenActionsRange);
                        ms += timeBetweenPressMs;
                        threadToUse.AddFromNowInMs(ms, () =>
                        {
                            User32KeyStrokeManager.SendKeyPostMessage(pointer, fishInteractKey, User32PressionType.Release);
                        });
                        ms += 5;

                    }

                }
                else {
                    threadToUse.AddFromNowInMs(ms, () =>
                    {

                        User32KeyStrokeManager.SendKeyPostMessage(pointer, fishInteractKey, User32PressionType.Press);
                    });
                    ms += timeBetweenPressMs;

                    ms += UnityEngine.Random.Range(0, randomnessBetweenActionsRange);

                    threadToUse.AddFromNowInMs(ms, () =>
                    {

                        User32KeyStrokeManager.SendKeyPostMessage(pointer, fishInteractKey, User32PressionType.Release);
                    });
                    ms += 5;
                    
                }

                if (isHyperOcean)
                {
                    ms += 2000;

                }


                
                threadToUse.AddFromNowInMs(ms, () => {
                         User32KeyStrokeManager.SendKeyPostMessage(pointer, fishDejunkKey, User32PressionType.Press);
                     });
                ms += timeBetweenPressMs;
                threadToUse.AddFromNowInMs(ms, () => {
                    User32KeyStrokeManager.SendKeyPostMessage(pointer, fishDejunkKey, User32PressionType.Release);
                });

                ms += UnityEngine.Random.Range(0, randomnessBetweenActionsRange);
                ms += timeBeforeRelaunchLineMs;

                if (isHyperOcean)
                {
                    threadToUse.AddFromNowInMs(ms, () => {
                        User32KeyStrokeManager.SendKeyPostMessage(pointer, fishCancelKey, User32PressionType.Press);
                    });
                    ms += timeBetweenPressMs;
                    threadToUse.AddFromNowInMs(ms, () => {
                        User32KeyStrokeManager.SendKeyPostMessage(pointer, fishCancelKey, User32PressionType.Release);
                    });
                    ms += 1000;

                    ms += (timeBetweenCameraMoveMs); 
                    threadToUse.AddFromNowInMs(ms, () =>
                    {
                        User32KeyStrokeManager.SendKeyPostMessage(pointer, fishInteractKey, User32PressionType.Press);
                    });
                    ms += timeBetweenPressMs;
                    threadToUse.AddFromNowInMs(ms, () =>
                    {
                        User32KeyStrokeManager.SendKeyPostMessage(pointer, fishInteractKey, User32PressionType.Release);
                    });
                }
                else { 
                    threadToUse.AddFromNowInMs(ms, () => {
                        User32KeyStrokeManager.SendKeyPostMessage(pointer, fishSendLineKey, User32PressionType.Press);
                    });
                    ms += timeBetweenPressMs;
                    threadToUse.AddFromNowInMs(ms, () => {
                        User32KeyStrokeManager.SendKeyPostMessage(pointer, fishSendLineKey, User32PressionType.Release);
                    });
                }
                ms += 5;

                msCountAtEnd = ms;

            }

        }

    }

    private static void ReleaseKeyView(IntPtrWrapGet pointer, ThreadQueueDateTimeCallMono threadToUse, User32PostMessageKeyEnum cameraViewKey, int ms)
    {
        threadToUse.AddFromNowInMs(ms, () =>
        {
            User32KeyStrokeManager.SendKeyPostMessage(pointer, cameraViewKey, User32PressionType.Release);
        });
    }

    private static void PressKeyView(IntPtrWrapGet pointer, ThreadQueueDateTimeCallMono threadToUse, User32PostMessageKeyEnum cameraViewKey, int ms)
    {
        threadToUse.AddFromNowInMs(ms, () =>
        {
            User32KeyStrokeManager.SendKeyPostMessage(pointer, cameraViewKey, User32PressionType.Press);
        });
    }

    internal static void RecovertFishWithAutoInteract(IntPtrWrapGet m_pointer, ThreadQueueDateTimeCallMono m_threadToSendActions, User32PostMessageKeyEnum m_keyToFish, User32PostMessageKeyEnum m_keyToInteract, User32PostMessageKeyEnum[] m_keyToResetView, out int msAtEnd, object m_timeBetweenCameraMove, object m_timeBetweenPress, object m_timeBetweenLineRelaunch)
    {
        throw new NotImplementedException();
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