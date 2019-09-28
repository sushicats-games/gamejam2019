using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBasedOnMass : MonoBehaviour
{
    public float XMovespeedAtUnitMass;
    public float YMovespeedAtUnitMass;
    MovementCapability movementCapability;
    new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        movementCapability = GetComponent<MovementCapability>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementCapability.MovementX = XMovespeedAtUnitMass / rigidbody2D.mass;
        movementCapability.MovementY = YMovespeedAtUnitMass / rigidbody2D.mass;
    }
}
