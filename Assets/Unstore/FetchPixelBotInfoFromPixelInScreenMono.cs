using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FetchPixelBotInfoFromPixelInScreenMono : MonoBehaviour
{
    public Vector2 m_pourcentScreenXAndSpeed;
    public Vector2 m_pourcentScreenYAndFace;
    public Vector2 m_pixelScreenXAndSpeedPX;
    public Vector2 m_pixelScreenYAndFacePX;

    public RenderTexture m_screenAsTexture;
    public Texture2D m_targetAstexture2D;

    public Color m_xAndSpeed;
    public Color m_yAndFace;

    public PixelBotCoordinateRaw m_lastValue;
    public PixelBotCoordinateRawEvent m_onPixelRecovered;

    public void SetScreenTexture(Texture2D texture) {
        m_targetAstexture2D = texture;
    }
    public int width;
    public int height;
    private void Update()
    {
        if (m_screenAsTexture == null)
            return;

         width = m_screenAsTexture.width;
         height = m_screenAsTexture.height;

        RenderTexture.active = m_screenAsTexture;
        if(m_targetAstexture2D==null)
        m_targetAstexture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false, true);

        int x = (int)(width * m_pourcentScreenXAndSpeed.x);
        int y = (int)(height * (1f-m_pourcentScreenXAndSpeed.y));
        x = (Mathf.Clamp(x, 0, width-1));
        y = (Mathf.Clamp(y, 0, height - 1));
        m_pixelScreenXAndSpeedPX.x = x;
        m_pixelScreenXAndSpeedPX.y = y;
        m_targetAstexture2D.ReadPixels(new Rect(x, y, 1, 1), 0, 0);
        m_xAndSpeed = m_targetAstexture2D.GetPixels()[0];

        x = (int)(width *(float)( m_pourcentScreenYAndFace.x));
        y = (int)(height * (float)(1f-m_pourcentScreenYAndFace.y));
        x = (Mathf.Clamp(x, 0, width - 1));
        y = (Mathf.Clamp(y, 0, height - 1));
        m_pixelScreenYAndFacePX.x = x;
        m_pixelScreenYAndFacePX.y = y;
        m_targetAstexture2D.ReadPixels(new Rect(x, y, 1, 1), 0,0);
        m_yAndFace = m_targetAstexture2D.GetPixels()[0];

        m_lastValue = new PixelBotCoordinateRaw();
        m_lastValue.m_position.m_xLeftRight = m_xAndSpeed.r * 255f + m_xAndSpeed.g * 255f / 100f;
        m_lastValue.m_position.m_yDownTop =100f-( m_yAndFace.r * 255f + m_yAndFace.g * 255f / 100f);
        m_lastValue.m_speedAsPercent = m_xAndSpeed.b ;
        m_lastValue.m_left2RightPercentRotation = m_yAndFace.b;
        Vector3 dir = Quaternion.Euler(0, -m_lastValue.m_left2RightPercentRotation*360f, 0) * Vector3.forward;
        m_lastValue.m_direction.m_xLeftRight = dir.x;
        m_lastValue.m_direction.m_yDownTop = dir.z;
        m_onPixelRecovered.Invoke(m_lastValue);
    }
}


[System.Serializable]
public class PixelBotCoordinateRaw
{
    public WowMapCoordinateLRDTUnity m_position = new WowMapCoordinateLRDTUnity();
    public float m_speedAsPercent;
    public float m_left2RightPercentRotation;
    public WowMapDirectionLRDTUnity m_direction = new WowMapDirectionLRDTUnity();

    public void GetDirectionAsVector2(out Vector2 direction)
    {
        direction.x = m_direction.m_xLeftRight;
        direction.y = m_direction.m_yDownTop;
    }
    public void GetPositionAsVector2(out Vector2 position)
    {
        position.x = m_position.m_xLeftRight;
        position.y = m_position.m_yDownTop;
    }

    public WowMapCoordinateLRDTUnity GetForwardPoint(float forwardDistance=1f)
    {
        Vector2 point  = new Vector2(m_position.m_xLeftRight, m_position.m_yDownTop);
        Vector2 dir = new Vector2(m_direction.m_xLeftRight, m_direction.m_yDownTop)*forwardDistance;
        return new WowMapCoordinateLRDTUnity(point.x + dir.x, point.y + dir.y);
    }
}
[System.Serializable]
public class PixelBotCoordinateRawEvent : UnityEvent <PixelBotCoordinateRaw>
{
}
[System.Serializable]
public class WowMapCoordinateLRDTUnity
{
    public float m_xLeftRight;
    public float m_yDownTop;
    public WowMapCoordinateLRDTUnity(float xleftRight, float yDownTop)
    {
        m_xLeftRight = xleftRight;
        m_yDownTop = yDownTop;
    }
    public WowMapCoordinateLRDTUnity()
    {
    }

    public float GetLeftRightPercent() { return m_xLeftRight * 0.01f; }
    public float GetTopDownPercent() { return m_yDownTop * 0.01f; }
    public float GetDownTopPercent() { return (1f - m_yDownTop * 0.01f); }

    public void GetPercent(out Vector2 valueAsPercent)
    {
        valueAsPercent = new Vector2(m_xLeftRight * 0.01f, m_yDownTop * 0.01f);
    }

    internal void GetAsVector2(out Vector2 position)
    {
        position = new Vector2(m_xLeftRight, m_yDownTop);
    }
}
[System.Serializable]
public class WowMapDirectionLRDTUnity
{
    public float m_xLeftRight;
    public float m_yDownTop;

    public float GetLeftRightPercent() { return m_xLeftRight * 0.01f; }
    public float GetTopDownPercent() { return m_yDownTop * 0.01f; }

    internal void GetDirectionAsVector2(out Vector2 dirCurrent)
    {
        dirCurrent.x = m_xLeftRight;
        dirCurrent.y = m_yDownTop;
    }
}



[System.Serializable]
public class WowMapCoordinateLRTDOrigin
{
    public float m_xLeftRight;
    public float m_yTopDown;
    

}
[System.Serializable]
public class WowMapDirectionLRTDOrigin
{
    public float m_xLeftRight;
    public float m_yTopDown;

}