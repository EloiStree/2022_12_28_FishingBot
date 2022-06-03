using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TesseractDemoScript : MonoBehaviour
{
    [SerializeField] private Texture2D imageToRecognize;
    [SerializeField] private Text displayText;
    [SerializeField] private RawImage outputImage;
    private TesseractDriver _tesseractDriver;
    private string _text = "";
    public Texture2D _textureReceived;
    public Texture2D _texture;
    public Eloi.PrimitiveUnityEvent_String m_textFound;


    public Coroutine m_translating;

    private void Start()
    {
        _tesseractDriver = new TesseractDriver();
        Recoginze(imageToRecognize);
    }

    public void Recoginze(Texture2D outputTexture)
    {
        if(m_translating==null)
            m_translating = StartCoroutine(RcognizeCoroutine(outputTexture));
    }

    public IEnumerator RcognizeCoroutine(Texture2D given) {
        Texture2D texture = new Texture2D(given.width, given.height, TextureFormat.ARGB32, false);
        texture.SetPixels(given.GetPixels());
        texture.Apply();

        _texture = texture;
        ClearTextDisplay();
        //        AddToTextDisplay(_tesseractDriver.CheckTessVersion());
        _tesseractDriver.Setup(OnSetupCompleteRecognize);
        yield return new WaitForEndOfFrame();
        m_translating = null;
    }

   

    private void OnSetupCompleteRecognize()
    {
        AddToTextDisplay(_tesseractDriver.Recognize(_texture));
        AddToTextDisplay(_tesseractDriver.GetErrorMessage(), true);
        SetImageDisplay();
        m_textFound.Invoke(_text);
    }

    private void ClearTextDisplay()
    {
        _text = "";
    }

    private void AddToTextDisplay(string text, bool isError = false)
    {
        if (string.IsNullOrWhiteSpace(text)) return;

        _text += (string.IsNullOrWhiteSpace(displayText.text) ? "" : "\n") + text;

        if (isError)
            Debug.LogError(text);
        else
            Debug.Log(text);
    }

    private void LateUpdate()
    {
        displayText.text = _text;
    }

    private void SetImageDisplay()
    {
        RectTransform rectTransform = outputImage.GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
            rectTransform.rect.width * _tesseractDriver.GetHighlightedTexture().height / _tesseractDriver.GetHighlightedTexture().width);
        outputImage.texture = _tesseractDriver.GetHighlightedTexture();
    }
}