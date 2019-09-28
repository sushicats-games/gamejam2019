using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random2DRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(.0f, 360.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
