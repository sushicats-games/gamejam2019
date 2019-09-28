using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLowEnergy : MonoBehaviour
{
    public float EnergyThreshold = .0f;

    EnergyState energyState;
    // Start is called before the first frame update
    void Start()
    {
        energyState = GetComponent<EnergyState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (energyState.Energy <= EnergyThreshold)
        {
            Destroy(gameObject);
        }
    }
}
