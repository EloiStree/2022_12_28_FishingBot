using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_RNGSimulatorMono : MonoBehaviour
{
    [Header("What to RNG")]
    public float m_dropEveryNAttemps =4000;
    public float m_timeFarmed=3600;
    public int m_numberOfMobsKilled = 120;
    public float m_lootGoldProfitOnThatTime = 100;

    [Header("How many sim do you want")]
    public int m_simulationToDo = 10;


    [Header("Estimate the cost")]
    public float m_wattHour = 200;
    public float m_unityCostPerKWattHour = 0.27f;

    [Header("Deducted")]
    public float m_pourcentChangeToDrop = 0f;
    public float m_mobPerHours;
    public float m_goldPerHours;
    public float m_kWattUsePerMonth;
    public float m_electricityCostPerMonth;

    int m_simulationCount;
     float m_simulationEstimateFarmingTimeHour;
     float m_simulationEstimateFarmingTimeDay;
     float m_sideGoldWon;

    public List<SimulationResult> m_results= new List<SimulationResult>();

    [ContextMenu("Electricity cost in Belgium")]
    public void CheckBelgiumElectricityCost()
    {
        Application.OpenURL("https://www.statista.com/statistics/418067/electricity-prices-for-households-in-belgium/");
    }
    [ContextMenu("Electricity cost in USA")]
    public void CheckUSAElectricityCost()
    {
        Application.OpenURL("https://www.statista.com/statistics/183700/us-average-retail-electricity-price-since-1990/");
    }
    [ContextMenu("Electricity cost in World Rank")]
    public void CheckWorldRankElectricityCost()
    {
        Application.OpenURL("https://www.statista.com/statistics/263492/electricity-prices-in-selected-countries/");
    }

    [System.Serializable]
    public class SimulationResult {
        public int m_simulationCount;
        public float m_simulationEstimateFarmingTimeHour;
        public float m_simulationEstimateFarmingTimeDay;
        public float m_sideGoldWon;
        public float m_kiloWattUsed;
        public float m_cost;
    }

    [ContextMenu("Simulate RNG")]
    public void SimulateRNG() {

        m_results.Clear();
        for (int j = 0; j < m_simulationToDo; j++)
        {
            SimulationResult result = new SimulationResult();
            ValideRefresh();
            for (int i = 0; i < int.MaxValue; i++)
            {
                float changeToDrop = UnityEngine.Random.value;
                if (changeToDrop < m_pourcentChangeToDrop)
                {
                    m_simulationCount = i;
                    float estimateTimeToHaveMountSeconds = m_simulationCount * (3600f / (float)m_mobPerHours);
                    float estimateTimeToHaveMountHours = estimateTimeToHaveMountSeconds/3600f;
                    m_simulationEstimateFarmingTimeHour = (estimateTimeToHaveMountSeconds) / (3600f);
                    m_simulationEstimateFarmingTimeDay = (estimateTimeToHaveMountSeconds) / (3600f * 24);
                    m_sideGoldWon = (estimateTimeToHaveMountHours) * m_goldPerHours;
                    result.m_kiloWattUsed = estimateTimeToHaveMountHours * (m_wattHour/1000f);
                    result.m_cost = result.m_kiloWattUsed * m_unityCostPerKWattHour;
                    result.m_simulationCount = m_simulationCount;
                    result.m_simulationEstimateFarmingTimeHour = m_simulationEstimateFarmingTimeHour;
                    result.m_simulationEstimateFarmingTimeDay = m_simulationEstimateFarmingTimeDay;
                    result.m_sideGoldWon = m_sideGoldWon;
                    m_results.Add(result);
                    break;
                }
            }
        }
        
        
    
    }

    public void OnValidate()
    {
        ValideRefresh();
    }

    private void ValideRefresh()
    {
        if (m_dropEveryNAttemps <= 0)
            m_dropEveryNAttemps = 1;
        m_pourcentChangeToDrop = 1f / m_dropEveryNAttemps;

        if (m_timeFarmed <= 0)
            m_timeFarmed = 1f;
        if (m_lootGoldProfitOnThatTime <= 0)
            m_lootGoldProfitOnThatTime = 1f;
        m_mobPerHours = m_numberOfMobsKilled / m_timeFarmed * 3600f;
        m_goldPerHours = m_lootGoldProfitOnThatTime / m_timeFarmed * 3600f;

        m_kWattUsePerMonth = 30 * 24 * (m_wattHour/1000f);
        m_electricityCostPerMonth = m_kWattUsePerMonth*m_unityCostPerKWattHour;
    }
}
