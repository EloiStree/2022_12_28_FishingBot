using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporary_SetFishermenWithInputIds : MonoBehaviour
{
    public InputField [] m_processIdOfFishmen;
    public IndexToProcessIdCollectionMono m_indexToProcess;

    private void Start()
    {
        for (int i = 0; i < m_processIdOfFishmen.Length; i++)
        {
            m_processIdOfFishmen[i].onValueChanged.AddListener(Refresh);
        }
    }

    public void Refresh(string value)
    {
        Refresh();
    }
        public void Refresh()
        {

            for (int i = 0; i < m_processIdOfFishmen.Length; i++)
        {

                if (int.TryParse(m_processIdOfFishmen[i].text, out int id))
                {
                m_indexToProcess.SetProcessIdOf(i, id);
                }
                else
                {
                    m_indexToProcess.SetProcessIdOf(i, 0);
                }
            }
        }
    

}
