using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleByEnergy : MonoBehaviour
{
    EnergyState energyState;
    // Start is called before the first frame update
    void Start()
    {
        energyState = GetComponent<EnergyState>();
        UpdateScale();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScale();
    }

    void UpdateScale()
    {
        var e = Mathf.Sqrt(energyState.Energy);
        transform.localScale = new Vector3(e, e, e);
    }
}
