
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U32Interpreter_WindowFrameActions : AbstractGenericIntepreterMono<IZhuLiCommand>
{

    public override bool CanInterpreterUnderstand(in IZhuLiCommand value)
    {

        return value is ZhuLi_SetFocusOnFrame ||
            value is ZhuLi_SetFrameDisplayState;

    }

    public override void TryTranslate(out bool succedToTranslate, in IZhuLiCommand value)
    {
        if (value is ZhuLi_SetFocusOnFrame)
        {
            ZhuLi_SetFocusOnFrame request = (ZhuLi_SetFocusOnFrame)value;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                WindowIntPtrUtility.SetForegroundWindow(request.m_processToApplyTo.m_processId);
            });
            succedToTranslate = true;
        }
        else if (value is ZhuLi_SetFrameDisplayState)
        {
            ZhuLi_SetFrameDisplayState request = (ZhuLi_SetFrameDisplayState)value;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                foreach (var item in request.m_processToApplyTo.m_processesId)
                {
                    WindowIntPtrUtility.ShowWindow(item, Convert(request.m_displayStateType));
                }
            });

            succedToTranslate = true;
        }
        


        succedToTranslate = false;
    }

    private WindowIntPtrUtility.WindowDisplayType Convert(ZhuLi_SetFrameDisplayState.DisplayStateType m_displayStateType)
    {
        switch (m_displayStateType)
        {
            case ZhuLi_SetFrameDisplayState.DisplayStateType.TotallyHidden:
                return WindowIntPtrUtility.WindowDisplayType.Hide;
            case ZhuLi_SetFrameDisplayState.DisplayStateType.HiddenInBar:
                return WindowIntPtrUtility.WindowDisplayType.SmallSize;
            case ZhuLi_SetFrameDisplayState.DisplayStateType.Normal:
                return WindowIntPtrUtility.WindowDisplayType.RestoreToDefault;
            case ZhuLi_SetFrameDisplayState.DisplayStateType.Maximized:
                return WindowIntPtrUtility.WindowDisplayType.MaxSize;
            default:
                break;
        }
        throw new Exception("Should not reach");
    }
}
