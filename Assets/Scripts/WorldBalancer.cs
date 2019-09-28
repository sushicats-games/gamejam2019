using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBalancer : MonoBehaviour
{
    float rebalanceTimer = 29.0f;
    float initialTotalEnergy = 0.0f;
    public GameObject herbivoreSpawnLocation, carnivoreSpawnLocation, starterAlgaeLocation;
    public GameObject herbivorePrefab, carnivorePrefab, starterAlgaePrefab;

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

        if (rebalanceTimer > 30.0f)
        {
            rebalanceTimer -= 30.0f;
            RebalanceLevel();
        }

        try
        {
            if (PlayerController.Singleton == null || PlayerController.Singleton.enabled == false)
            {
                SpawnStarterAlgae();
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e);
            SpawnStarterAlgae();
        }

    }

    private void RebalanceLevel()
    {
        int herbivoreCount = 0;
        int carnivoreCount = 0;
        foreach (var fish in UnityEngine.Object.FindObjectsOfType<EatOnCollide>())
        {
            if (fish.FoodClass == "plants")
            {
                herbivoreCount++;
            }
            else if (fish.FoodClass == "herbivore")
            {
                carnivoreCount++;
            }
            else
            {
                Debug.LogWarning("invalid type" + fish.FoodClass);
            }
        }

        if (herbivoreCount == 0)
        {
            SpawnAHerbivore();
        }

        if (carnivoreCount == 0)
        {
            SpawnACarnivore();
        }

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

    private void SpawnACarnivore()
    {
        Instantiate(carnivorePrefab, 
            carnivoreSpawnLocation.transform.position, 
            carnivoreSpawnLocation.transform.rotation);
    }

    private void SpawnAHerbivore()
    {
        Instantiate(herbivorePrefab,
            herbivoreSpawnLocation.transform.position,
            herbivoreSpawnLocation.transform.rotation);
    }

    private void SpawnStarterAlgae()
    {
        Instantiate(starterAlgaePrefab,
            starterAlgaeLocation.transform.position,
            starterAlgaeLocation.transform.rotation);
    }
}
