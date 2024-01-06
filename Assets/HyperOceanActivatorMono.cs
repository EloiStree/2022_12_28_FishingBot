using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperOceanActivatorMono : MonoBehaviour
{


    public HyperOceanMemory m_memory = new HyperOceanMemory();
    [System.Serializable]
    public class HyperOceanMemory { 
        public long m_last;
    }

    public string m_key = "HyperMemory";


    [ContextMenu("")]
    public void NotifyOceanPushed() {
        m_memory.m_last = DateTime.Now.Ticks;

    }

    public long GetTickLeft() {

        long now = DateTime.Now.Ticks;
        return m_hyperOceanLifeTime-(now - m_memory.m_last);
    }


    void Start()
    {
        string text = "";

        text = LoadIfExists(text);

    }
    public long m_hyperOceanLifeTime=(long )( 9000000000);
    public long m_timeLeft;
    private void Update()
    {
        m_timeLeft = GetTickLeft();
        if (m_timeLeft < 0)
            NotifyHyperOceanAsFinished();
    }

    private void NotifyHyperOceanAsFinished()
    {
      //  throw new NotImplementedException();
    }

    private string LoadIfExists(string text)
    {
        if (PlayerPrefs.HasKey(m_key))
        {
            text = PlayerPrefs.GetString(m_key);
            m_memory = JsonUtility.FromJson<HyperOceanMemory>(text);
        }

        if (m_memory.m_last <= 0)
            m_memory.m_last = DateTime.Now.Ticks - 100000000;

        return text;
    }

    private void OnDestroy()
    {
        Save();

    }

    private void Save()
    {
        PlayerPrefs.SetString(m_key, JsonUtility.ToJson(m_memory));
    }

    private void OnApplicationQuit()
    {
        Save();

    }

}
