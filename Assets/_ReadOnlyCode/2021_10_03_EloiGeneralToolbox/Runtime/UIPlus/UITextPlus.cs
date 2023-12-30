using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Eloi
{
    public class UITextPlus : MonoBehaviour
    {
        public Text m_text;
        public void SetWith(int value) => m_text.text = "" + value;

        private void Reset()
        {
            m_text = GetComponent<Text>();
        }
    }
}
