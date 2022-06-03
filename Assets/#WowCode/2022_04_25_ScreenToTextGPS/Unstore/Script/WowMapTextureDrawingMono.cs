using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowMapTextureDrawingMono : MonoBehaviour
{
    public Texture2D m_drawing;
    public Renderer m_debug;
    public Color m_backgroundColor = Color.black * 0.0f;
    public Color m_drawColor = Color.green * 0.5f;
    public int m_resolution=1024;
    public void Awake()
    {
        m_drawing = new Texture2D(m_resolution, m_resolution, TextureFormat.ARGB32,false);
        Color[] c = m_drawing.GetPixels();
        for (int i = 0; i < c.Length; i++)
        {
            c[i] = m_backgroundColor;
        }
        m_drawing.SetPixels(c);
        m_drawing.Apply();
        m_debug.material.mainTexture = m_drawing;
    }
    public void DrawPixelAt(WowCoordinateRaw coordinate) {

        coordinate.GetPositionAsPercent(out Vector2 pourcent);
        m_drawing.SetPixel((int)(pourcent.x *(float) m_drawing.width), (int)(pourcent.y *(float) m_drawing.height), m_drawColor);
        m_drawing.Apply();
        m_debug.material.mainTexture = m_drawing;
    }
}
