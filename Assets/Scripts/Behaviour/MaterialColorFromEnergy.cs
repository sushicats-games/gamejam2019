using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorFromEnergy : MonoBehaviour
{
    public Color ZeroColor;

    public Color MaxColor;
    public float MaxLevel = 10.0f;
    private float prevEnergy =-1.0f;

    EnergyState energyState;
    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        energyState = GetComponent<EnergyState>();
        UpdateMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        var energy = energyState.Energy;
        energy = Mathf.Min(MaxLevel, energy);
        if (prevEnergy != energy)
        {
            mesh.material.SetColor("_BaseColor", Color.Lerp(ZeroColor, MaxColor, energy / MaxLevel));
            prevEnergy = energy;
        }
    }
}
