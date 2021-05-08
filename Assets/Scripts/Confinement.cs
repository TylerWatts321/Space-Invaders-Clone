using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confinement : MonoBehaviour
{
    public float MIN_X = -7f;
    public float MAX_X = 7f;
    public float MIN_Y = -10f;
    public float MAX_Y = 7f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
            Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
            0);
    }
}
