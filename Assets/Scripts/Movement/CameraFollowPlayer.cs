using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public float distanceFromPlayer = 4.0f;
    public float followSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            UpdateCameraPosition();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var characterController = PlayerController.Singleton;
        if (characterController != null)
        {
            target = characterController.transform;
        }
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        //transform.position = new Vector3(
        //    player.position.x * followSpeed * Time.deltaTime,
        //    player.position.y * followSpeed * Time.deltaTime,
        //    distanceFromPlayer * -1f);

        var lerpedPosition = Vector2.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
        transform.position = new Vector3(lerpedPosition.x, lerpedPosition.y, distanceFromPlayer * -1f);
    }
}
