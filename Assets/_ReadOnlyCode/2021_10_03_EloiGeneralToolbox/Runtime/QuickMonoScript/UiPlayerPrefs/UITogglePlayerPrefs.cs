using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace be.eloistree.generaltoolbox
{
    public class UITogglePlayerPrefs : MonoBehaviour
    {
        public UnityEngine.UI.Toggle m_toggle;
        public string m_id;
        public Eloi.PrimitiveUnityEvent_String m_onLoad;
        public bool m_notifyWhenReset = true;
        public bool m_defaultValueIfEmpty=true;
        // Start is called before the first frame update
        void Awake()
        {
            string t = PlayerPrefs.GetString(m_id);

            if (string.IsNullOrEmpty(t))
                t = ""+(m_defaultValueIfEmpty ? 1 : 0);
            if (int.TryParse(t, out int v)) { 
                if(m_notifyWhenReset)
                    m_toggle.isOn = v==1;
                else m_toggle.SetIsOnWithoutNotify(v==1);
            }
            m_onLoad.Invoke(t);
        }

        private void OnDestroy()
        {
            SaveInputField();
        }

        private void SaveInputField()
        {
            PlayerPrefs.SetString(m_id, ""+(m_toggle.isOn ? 1 : 0));
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
            m_toggle = GetComponent<UnityEngine.UI.Toggle>();
        }

        [ContextMenu("Generate New ID")]
        private void GenerateId()
        {
            m_id = "" + Guid.NewGuid();
        }
    }
}
