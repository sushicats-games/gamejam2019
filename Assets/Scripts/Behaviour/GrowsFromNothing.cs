using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowsFromNothing : MonoBehaviour
{
    public float GrowthRate = .1f;

    EnergyState energyState;

    // Start is called before the first frame update
    void Start()
    {
        energyState = GetComponent<EnergyState>();
    }

    // Update is called once per frame
    void Update()
    {
        energyState.Energy += Time.deltaTime * GrowthRate;
    }
}
