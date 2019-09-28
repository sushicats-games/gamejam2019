using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatOnCollide : MonoBehaviour
{
    public string FoodClass;
    public float EatingSpeed;

    EnergyState energyState;
    // Start is called before the first frame update
    void Start()
    {
        energyState = GetComponent<EnergyState>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject;
        var edible = other.GetComponent<EdibleState>();
        if (edible == null || edible.FoodClass != FoodClass)
        {
            return;
        }

        var otherPlayer = other.GetComponent<PlayerController>();
        if (otherPlayer != null)
        {
            gameObject.AddComponent<PlayerController>();
        }

        var otherEnergy = collision.gameObject.GetComponent<EnergyState>();
        var transfer = Mathf.Min(EatingSpeed, otherEnergy.Energy);

        energyState.Energy += transfer;
        otherEnergy.Energy -= transfer;
    }
}
