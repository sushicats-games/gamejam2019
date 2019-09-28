using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowsFromParent : MonoBehaviour
{
    public float GrowthRate = .1f;
    public float MoveToParent = 0.0f;
    public float MinParentEnergy = .5f;

    public GameObject ParentObject;

    EnergyState energyState;
    EnergyState parentEnergyState;
    // Start is called before the first frame update
    void Start()
    {
        energyState = GetComponent<EnergyState>();
        parentEnergyState = ParentObject.GetComponent<EnergyState>();
    }

    void FixedUpdate()
    {

        if (ParentObject == null)
        {
            return;
        }
        var parentPos = ParentObject.transform.localPosition;
        var pos = Vector3.Lerp(transform.localPosition, parentPos, MoveToParent);
    }
    // Update is called once per frame
    void Update()
    {
        if (ParentObject == null || parentEnergyState == null)
        {
            return;
        }
        if (parentEnergyState.Energy < MinParentEnergy)
        {
            return;
        }
        var transfer = Time.deltaTime * GrowthRate;
        transfer = Mathf.Min(transfer, parentEnergyState.Energy);
        energyState.Energy += transfer;
        parentEnergyState.Energy -= transfer;
    }
}
