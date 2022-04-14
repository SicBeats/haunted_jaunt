using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLERP : MonoBehaviour
{
    private Vector3 rotate;

    public Transform primary;
    public Transform secondary;

    void Start()
    {
        rotate = new Vector3(0.0F, 1.0F, 0.0F);
    }

    void FixedUpdate()
    {
        Vector3 distance = primary.position - secondary.position;

        if (distance.magnitude < 3.1)
        {
            float minSpeed = 400.0F;
            float maxSpeed = 2000.0F;
            float alpha = 0.1F;
            float interpolate = ((1.0F - alpha) * minSpeed) + (alpha * maxSpeed);
            transform.Rotate(rotate * interpolate * Time.deltaTime);
        }
    }
}
