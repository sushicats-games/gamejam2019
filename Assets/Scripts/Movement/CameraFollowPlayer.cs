using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float distanceFromPlayer;
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraPosition();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        //transform.position = new Vector3(
        //    player.position.x * followSpeed * Time.deltaTime,
        //    player.position.y * followSpeed * Time.deltaTime,
        //    distanceFromPlayer * -1f);

        var lerpedPosition = Vector2.Lerp(transform.position, player.position, followSpeed * Time.deltaTime);
        transform.position = new Vector3(lerpedPosition.x, lerpedPosition.y, distanceFromPlayer * -1f);
    }
}
