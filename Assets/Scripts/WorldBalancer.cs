using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBalancer : MonoBehaviour
{
    float rebalanceTimer = 0.0f;
    float initialTotalEnergy = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var energyState in UnityEngine.Object.FindObjectsOfType<EnergyState>())
        {
            initialTotalEnergy += energyState.Energy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rebalanceTimer += Time.deltaTime;

        if (rebalanceTimer > 60.0f)
        {
            rebalanceTimer -= 60.0f;
            RebalanceLevel();
        }

    }

    private void RebalanceLevel()
    {
        var currentTotalEnergy = 0.0f;
        var gatherers = 0;
        foreach (var energyState in UnityEngine.Object.FindObjectsOfType<EnergyState>())
        {
            if (energyState.GathersLostEnergy)
            {
                gatherers++;
            }
            currentTotalEnergy += energyState.Energy;
        }

        var shortage = initialTotalEnergy - currentTotalEnergy;
        var resupplyGatherer = shortage / gatherers;

        foreach (var energyState in UnityEngine.Object.FindObjectsOfType<EnergyState>())
        {
            if (energyState.GathersLostEnergy)
            {
                energyState.Energy += resupplyGatherer;
            }
        }

    }
}
