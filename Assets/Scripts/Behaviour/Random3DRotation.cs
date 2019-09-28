using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random3DRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Random.rotationUniform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
