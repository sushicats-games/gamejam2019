using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseOneAndSpawn : MonoBehaviour
{
    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        var chosen = Random.Range(0, gameObjects.Length);
        Instantiate(gameObjects[chosen], this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
