using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : enemy
{
    // Start is called before the first frame update
    private float startY;
    private Rigidbody rigi;

    void Awake()
    {
        rigi = transform.GetComponent<Rigidbody>();
        startY = transform.position.y;
    }

    // Update is called once per frame
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
