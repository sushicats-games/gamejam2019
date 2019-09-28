using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatOnCollide : MonoBehaviour
{
    public AudioClip EatAudioClip;
    public string FoodClass;
    public float EatingSpeed;

    EnergyState energyState;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        energyState = GetComponent<EnergyState>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject;
        var edible = other.GetComponent<EdibleState>();
        if (edible == null || edible.FoodClass != FoodClass)
        {
            return;
        }

        var otherEnergy = collision.gameObject.GetComponent<EnergyState>();
        var transfer = Mathf.Min(EatingSpeed, otherEnergy.Energy);

        var otherPlayer = other.GetComponent<PlayerController>();
        if (otherPlayer != null && otherPlayer.enabled)
        {
            gameObject.AddComponent<PlayerController>();
            transfer = otherEnergy.Energy;
        }

        energyState.Energy += transfer;
        otherEnergy.Energy -= transfer;

        audioSource.PlayOneShot(EatAudioClip);
    }
}
