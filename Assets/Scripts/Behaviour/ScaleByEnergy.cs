using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleByEnergy : MonoBehaviour
{
    public float ExtraScale = 0.0f;
    public float ScalingFactor = 1.0f;
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
        var scale = energyState.Energy * ScalingFactor + ExtraScale;
        var signs = transform.localScale;
        signs.x = Mathf.Sign(signs.x);
        signs.y = Mathf.Sign(signs.y);
        signs.z = Mathf.Sign(signs.z);
        var e = Mathf.Sqrt(Mathf.Max(.0f, scale));
        transform.localScale = (new Vector3(e * signs.x, e * signs.y, e * signs.z));
    }
}
