using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeOnCollide : MonoBehaviour
{
    public string MergeClass = "algae";
    public float EnergyTransfer = 1.0f;
    public float EnergyLoss = 1.0f;
    EnergyState energyState;

    // Start is called before the first frame update
    void Start()
    {
        energyState = GetComponent<EnergyState>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        var otherMerge = collision.gameObject.GetComponent<MergeOnCollide>();
        if (otherMerge == null || otherMerge.MergeClass != MergeClass)
        {
            return;
        }
        var otherEnergy = collision.gameObject.GetComponent<EnergyState>();
        {
            if (energyState.Energy <= otherEnergy.Energy)
            {
                var transfer = energyState.Energy * EnergyTransfer;
                energyState.Energy -= transfer + EnergyLoss;
                otherEnergy.Energy += transfer;
                if (energyState.Energy < 0)
                {
                    otherEnergy.Energy += energyState.Energy;
                    energyState.Energy = .0f;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
