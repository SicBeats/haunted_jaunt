using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotator : MonoBehaviour
{

    public GameObject player;
    private Vector3 rotate;
    
    void Start()
    {
        rotate = new Vector3(0.0F, 1.0F, 0.0F);
    }

    
    void FixedUpdate()
    {
        //Vector math B(Gargoyle) - A(Player) == the vector from A to B
        Vector3 direction = player.transform.position - transform.position;
        //Maintain the direction since we don't care about magnitude
        direction.Normalize();

        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(Vector3.forward, direction));

        Vector3 cross_prod = Vector3.Cross(Vector3.forward, direction);

        if (cross_prod.y < 0.0F)
        {
            angle = -angle;
        }

        rotate.y = angle;
        transform.eulerAngles = rotate;
    }
}
