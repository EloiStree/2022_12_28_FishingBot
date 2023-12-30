using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U32Interpreter_ClipboardActions : AbstractGenericIntepreterMono<IZhuLiCommand>
{

    public override bool CanInterpreterUnderstand(in IZhuLiCommand value)
    {

        return value is ZhuLi_ClipboardTextAsEmpty ||
            value is ZhuLi_SetClipboardWithText ||
            value is ZhuLi_SetClipboardImageWithTexture2D;   

    }

    public override void TryTranslate(out bool succedToTranslate, in IZhuLiCommand value)
    {
        if (value is ZhuLi_ClipboardTextAsEmpty)
        {
            ZhuLi_ClipboardTextAsEmpty request = (ZhuLi_ClipboardTextAsEmpty)value;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32ClipboardUtility.SetClipboardToEmpty(out bool fail);
             });
            succedToTranslate = true;
        }
        else if (value is ZhuLi_SetClipboardWithText)
        {
            ZhuLi_SetClipboardWithText request = (ZhuLi_SetClipboardWithText)value;
            ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () => {
                User32ClipboardUtility.CopyTextToClipboard(request.m_textToSendToClipboard, out bool fail);
            });
         
            succedToTranslate = true;
        }
        else if (value is ZhuLi_SetClipboardImageWithTexture2D)
        {

            ZhuLi_SetClipboardImageWithTexture2D request = (ZhuLi_SetClipboardImageWithTexture2D)value;

            Debug.LogWarning("Not managed yet");
        }
      

        succedToTranslate = false;
    }
}
