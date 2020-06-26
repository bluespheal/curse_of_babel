using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : enemy
{
    private Rigidbody rigi;
    
    public float bounceLenght;
    public int bounces;
    public int bouncesLeft;

    private float startY;

    void Awake()
    {
        rigi = transform.GetComponent<Rigidbody>();
        startY = transform.position.y;
        bouncesLeft = bounces;
    }

    void Update()
    {
        if (!stop) 
        {
            if (bounceLenght != 0) 
            {
                rigi.velocity = new Vector3(bounceLenght, rigi.velocity.y, rigi.velocity.z);
            }

            if (transform.position.y >= startY) {
                rigi.velocity = new Vector3(rigi.velocity.x,0,0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bounceLenght != 0) {
            if (collision.transform.CompareTag("platform")) {
                if (bouncesLeft <= 0)
                {
                    bounceLenght = -bounceLenght;
                    bouncesLeft = bounces;
                }
                bouncesLeft -= 1;
            }
        }
    }
}
