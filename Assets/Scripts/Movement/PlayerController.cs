using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float previousXScale = 1f;

    public static PlayerController Singleton;

    public static bool IsSingletonEnabled()
    {
        if (Singleton == null)
        {
            return false;
        }
        return Singleton.enabled;
    }

    MovementCapability movementCapability;

    void Start()
    {
        EnableThisOne();
        movementCapability = GetComponent<MovementCapability>();
    }

    void Update()
    {
        float xVelocity = movementCapability.MovementX;
        float yVelocity = movementCapability.MovementY;
        
        float xMovement = Input.GetAxis("Horizontal") * xVelocity;
        float yMovement = Input.GetAxis("Vertical") * yVelocity;

        if (xMovement != 0 || yMovement != 0)
        {
            transform.Translate(new Vector3(xMovement, yMovement) * Time.deltaTime);

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

    internal bool IsEnabled()
    {
        return this.enabled && Singleton == this;
    }

    public void EnableThisOne()
    {
        if (Singleton != null)
        {
            Singleton.enabled = false; // disable previous player controller...
        }
        Singleton = this;
        this.enabled = true;
    }
}
