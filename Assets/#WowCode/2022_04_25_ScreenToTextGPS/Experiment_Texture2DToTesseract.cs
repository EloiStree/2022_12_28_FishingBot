using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Experiment_Texture2DToTesseract : MonoBehaviour
{
    public RenderTexture m_source;
    public float m_timeBetweenCheck;
    public Rect m_pourcentToExtract = new Rect(0, 0, 0.1f, 0.1f);
    public Rect m_pixelExtract;
    public Texture2D m_cut;
    public Texture2D m_sourceAsCopy;
    public Texture2D m_cutByCopy;
    [TextArea(1, 20)]
    public string m_lastUpdate;

    public float m_leftCut;
    public float m_rightCut;
    public float m_topCut;
    public float m_downCut;
    IEnumerator Start()
    {
        while (true)
        {
            m_pourcentToExtract.x = Mathf.Clamp01(m_pourcentToExtract.x);
            m_pourcentToExtract.y = Mathf.Clamp01(m_pourcentToExtract.y);
            m_pourcentToExtract.width = Mathf.Clamp01(m_pourcentToExtract.width);
            m_pourcentToExtract.height = Mathf.Clamp01(m_pourcentToExtract.height);



            m_pixelExtract.x = m_pourcentToExtract.x * m_source.width;
            m_pixelExtract.y = m_pourcentToExtract.y * m_source.height;
            m_pixelExtract.width = m_pourcentToExtract.width * m_source.width;
            m_pixelExtract.height = m_pourcentToExtract.height * m_source.height;


            if (m_sourceAsCopy == null || (m_sourceAsCopy.width != (int)m_pixelExtract.width && m_sourceAsCopy.height != (int)m_pixelExtract.height))
                m_sourceAsCopy = new Texture2D((int)m_pixelExtract.width, (int)m_pixelExtract.height, TextureFormat.RGBA32, false);
      
            RenderTexture.active = m_source;
            m_sourceAsCopy.ReadPixels(new Rect((int)m_pixelExtract.x, (int)m_pixelExtract.y, (int)m_pixelExtract.width, (int)m_pixelExtract.height), 0, 0);
            m_sourceAsCopy.Apply();

            try
            {
                m_onCut.Invoke(m_sourceAsCopy);
            }
            catch (Exception e ) {
                Debug.Log("Shit happened:" + e.StackTrace);
            }
            //Recoginze(m_cut);
            m_lastUpdate = System.DateTime.Now.ToString();
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetweenCheck);
        }
    }

    public TestTexture m_onCut;
    [System.Serializable]
    public class TestTexture : UnityEvent<Texture2D>{}

   

    public static void FlipTexture(ref Texture2D texture)
    {
        int textureWidth = texture.width;
        int textureHeight = texture.height;

        Color32[] pixels = texture.GetPixels32();

        for (int y = 0; y < textureHeight; y++)
        {
            int yo = y * textureWidth;
            for (int il = yo, ir = yo + textureWidth - 1; il < ir; il++, ir--)
            {
                Color32 col = pixels[il];
                //Color32 col = new Color32(pixels[il].r, pixels[il].g, pixels[il].b, pixels[il].a);
                pixels[il] = pixels[ir];
                pixels[ir] = col;
            }
        }
        texture.SetPixels32(pixels);
        texture.Apply();
    }
    public static void FlipTexture(ref Color32[] pixels, int textureWidth, int textureHeight)
    {

        for (int y = 0; y < textureHeight; y++)
        {
            int yo = y * textureWidth;
            for (int il = yo, ir = yo + textureWidth - 1; il < ir; il++, ir--)
            {

                Color32 col = new Color32(pixels[il].r, pixels[il].g, pixels[il].b, pixels[il].a);
                pixels[il] = pixels[ir];
                pixels[ir] = col;
            }
        }
    }
}
