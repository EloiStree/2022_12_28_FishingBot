using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_RecordPositionInProcessFrame : MonoBehaviour
{
    public int m_proccessId;

    public ProcessesFocusHistoryMono m_processFocus;
    public ProcessesAccessMono m_processAccess;

    public IntPtrWrapGet m_targetProcess;
    public IntPtrWrapGet m_displayTargetProcess;
    public DeductedInfoOfWindowSizeWithSource m_windowInfo;
    public int m_cusorPositionX;
    public int m_cusorPositionY;
    public float m_relativeCusorPositionXPct;
    public float m_relativeCusorPositionYPct;


    public float m_percentHorizontalToAbsoluteX;
    public int m_percentHorizontalToAbsolutePixelX;
    public float m_percentHorizontalToAbsoluteY;
    public int m_percentHorizontalToAbsolutePixelY;

    public float m_percentInFrameX;
    public float m_percentInFrameY;
    private void Awake()
    {
        m_isLeftMousePressed.m_onChanged.AddListener((bool value) => {
            m_keyRecord.Add(new ScreenRawPositionAndFramePercentLRBT()
            {
                m_buttonType = User32MouseClassicButton.Left,
                m_bot2Top = m_percentInFrameY,
                m_left2Right = m_percentInFrameX,
                m_winX = m_percentHorizontalToAbsolutePixelX,
                m_winY = m_percentHorizontalToAbsolutePixelY
            }); ;
        });
        m_isMiddleMousePressed.m_onChanged.AddListener((bool value) => {
            m_keyRecord.Add(new ScreenRawPositionAndFramePercentLRBT()
            {
                m_buttonType = User32MouseClassicButton.Middle,
                m_bot2Top = m_percentInFrameY,
                m_left2Right = m_percentInFrameX,
                m_winX = m_percentHorizontalToAbsolutePixelX,
                m_winY = m_percentHorizontalToAbsolutePixelY
            }); ;
        });
        m_isRightMousePressed.m_onChanged.AddListener((bool value) => {
            m_keyRecord.Add(new ScreenRawPositionAndFramePercentLRBT()
            {
                m_buttonType = User32MouseClassicButton.Right,
                m_bot2Top = m_percentInFrameY,
                m_left2Right = m_percentInFrameX,
                m_winX = m_percentHorizontalToAbsolutePixelX,
                m_winY = m_percentHorizontalToAbsolutePixelY
            }); ;
        });
    }

    void Update()
    {
        m_targetProcess = IntPtrProcessId.Int(m_proccessId);
        m_processAccess.GetChildrenThatDisplay(m_targetProcess, out bool found, out m_displayTargetProcess);
        m_windowInfo = new DeductedInfoOfWindowSizeWithSource(m_displayTargetProcess, new WindowIntPtrUtility.RectPadValue());
        User32WindowRectUtility.RefreshInfoOf(ref  m_windowInfo);
        WindowIntPtrUtility.GetCursorPos(out m_cusorPositionX, out m_cusorPositionY);


        m_windowInfo.m_frameSize.GetLeftToRightToAbsolute(m_percentHorizontalToAbsoluteX, out m_percentHorizontalToAbsolutePixelX);
        m_windowInfo.m_frameSize.GetTopToBottomToAbsolute(m_percentHorizontalToAbsoluteY, out m_percentHorizontalToAbsolutePixelY);



        m_windowInfo.m_frameSize.GetBottomToTopPercentFor(m_cusorPositionX, out m_percentInFrameX);
        m_windowInfo.m_frameSize.GetBottomToTopPercentFor(m_cusorPositionY, out m_percentInFrameY);


        m_isLeftMousePressed.SetBoolean(WindowIntPtrUtility.IsMousePressingLeft());
        m_isMiddleMousePressed.SetBoolean(WindowIntPtrUtility.IsMousePressingMiddle());
        m_isRightMousePressed.SetBoolean(WindowIntPtrUtility.IsMousePressingRight());
       

    }

    public List<ScreenRawPositionAndFramePercentLRBT> m_keyRecord = new List<ScreenRawPositionAndFramePercentLRBT>();

    [System.Serializable]
    public class ScreenRawPositionAndFramePercentLRBT {
        public User32MouseClassicButton m_buttonType;
        public int m_winX;
        public int m_winY;
        public float m_left2Right;
        public float m_bot2Top;
    }


    public DefaultBooleanChangeListener m_isLeftMousePressed;
    public DefaultBooleanChangeListener m_isMiddleMousePressed;
    public DefaultBooleanChangeListener m_isRightMousePressed;
}
