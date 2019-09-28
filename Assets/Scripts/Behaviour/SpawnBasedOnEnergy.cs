using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBasedOnEnergy : MonoBehaviour
{
    public GameObject Blueprint;
    public float minEnegy;
    public float transferedEnergy;
    public float lostEnergy;
    public float SpawnTimer = 1.0f; 
    public float Chance = 1.0f;
    EnergyState energyState;
    public Vector2 spawnDirectionBias;
    public float spawnDistance;
    private float spawnTimerAcc = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        energyState = GetComponent<EnergyState>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimerAcc += Time.deltaTime;
        while (spawnTimerAcc >= SpawnTimer)
        {
            spawnTimerAcc -= SpawnTimer;
            DoSpawn();
        }
    }

    void DoSpawn()
    {
        if (energyState.Energy > minEnegy)
        {
            if (Chance < Random.Range(.0f, 1.0f))
            {
                return;
            }

            var scale = gameObject.transform.localScale.x;

            var rand = Random.insideUnitCircle;
            rand += spawnDirectionBias;
            rand.Normalize();
            rand *= spawnDistance * scale;
            var obj = Instantiate(Blueprint,
                transform.localPosition + new Vector3(rand.x, rand.y, .0f),
                Quaternion.identity);
            energyState.Energy -= transferedEnergy;
            energyState.Energy -= lostEnergy;

            obj.GetComponent<EnergyState>().Energy = transferedEnergy;
            obj.GetComponent<GrowsFromParent>().ParentObject = gameObject;
        }
    }
}
