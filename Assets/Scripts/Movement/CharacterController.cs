using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float xVelocity;
    public float yVelocity;

    private float previousXScale = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        if (xMovement != 0 || yMovement != 0)
        {
            float yRotation = xMovement < 0 ? 180 : 0;

            transform.Translate(new Vector3(xMovement * xVelocity, yMovement * yVelocity) * Time.deltaTime);
            //transform.Rotate(Vector3.up, yRotation); // = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);

            Vector3 scale = transform.localScale;
            scale.x *= xMovement < 0 ? -1 : 1;

            if (xMovement < 0)
                scale.x = -1;
            else if (xMovement > 0)
                scale.x = 1;
            else
                scale.x = previousXScale;

            previousXScale = scale.x;

            transform.localScale = scale;
        }
    }
}
