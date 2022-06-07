using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : enemy
{
    private Rigidbody rigi;
    private float startY;
    void Awake()
    {
        rigi = transform.GetComponent<Rigidbody>();
        startY = transform.position.y;
    }

    void Update()
    {
        if (!stop)
        {
            if (transform.position.y >= startY) {
                rigi.velocity = new Vector3(rigi.velocity.x,0,0);
            }
        }
    }
}
