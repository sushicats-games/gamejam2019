using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMotion : MonoBehaviour
{
    public Vector2 Speed , Magnitude;
    Vector2 phase;
    const float twoPi = Mathf.PI * 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        phase.x = Random.Range(.0f, twoPi);
        phase.y = Random.Range(.0f, twoPi);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        phase += Speed * Time.fixedDeltaTime;
        if (phase.x > twoPi) phase.x -= twoPi;
        if (phase.y > twoPi) phase.y -= twoPi;
        var position = transform.localPosition;
        position.x += Mathf.Sin(phase.x) * Time.fixedDeltaTime * Magnitude.x;
        position.y += Mathf.Sin(phase.y) * Time.fixedDeltaTime * Magnitude.y;
        transform.localPosition = position;
    }
}
