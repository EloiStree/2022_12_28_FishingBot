using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace be.eloistree.generaltoolbox
{
    public class UISlidePlayerPrefs : MonoBehaviour
    {
        public UnityEngine.UI.Slider m_slider;
        public string m_id;
        public Eloi.PrimitiveUnityEvent_String m_onLoad;
        public bool m_notifyWhenReset = true;
        public float m_defaultValueIfEmpty=1;

        // Start is called before the first frame update
        void Awake()
        {
            string t = PlayerPrefs.GetString(m_id);
            if (string.IsNullOrEmpty(t))
                t = ""+m_defaultValueIfEmpty;
            if (float.TryParse(t, out float v)) {
                if (m_notifyWhenReset)
                    m_slider.value = v;
                else m_slider.SetValueWithoutNotify(v);
            }
            m_onLoad.Invoke(t);
        }

        private void OnDestroy()
        {
            SaveInputField();
        }

        private void SaveInputField()
        {
            PlayerPrefs.SetString(m_id, ""+m_slider.value);
        }

        private void OnApplicationPause(bool pause)
        {
            SaveInputField();

        }
        private void OnApplicationQuit()
        {

            SaveInputField();
        }


        private void Reset()
        {
            GenerateId();
            m_slider = GetComponent<UnityEngine.UI.Slider>();
        }

        [ContextMenu("Generate New ID")]
        private void GenerateId()
        {
            m_id = "" + Guid.NewGuid();
        }
    }
}
