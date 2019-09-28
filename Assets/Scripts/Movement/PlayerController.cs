using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float previousXScale = 1f;

    public static PlayerController Singleton;

    MovementCapability movementCapability;

    void Start()
    {
        if (Singleton != null)
        {
            Singleton.enabled = false; // disable previous player controller...
        }
        Singleton = this;
        movementCapability = GetComponent<MovementCapability>();
    }

    void Update()
    {
        float xVelocity = movementCapability.MovementX;
        float yVelocity = movementCapability.MovementY;
        
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        if (xMovement != 0 || yMovement != 0)
        {
            transform.Translate(new Vector3(xMovement * xVelocity, yMovement * yVelocity) * Time.deltaTime);

            Vector3 scale = transform.localScale;

            if (xMovement < 0)
                scale.x = -Mathf.Abs(scale.x);
            else if (xMovement > 0)
                scale.x = Mathf.Abs(scale.x);
            else
                scale.x = Mathf.Abs(scale.x) * Mathf.Sign(previousXScale);

            previousXScale = scale.x;

            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(previousXScale);
            transform.localScale = scale;
        }
    }
}
