using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_platform : MonoBehaviour
{
    //El tiempo que tarda en caer la plataforma despues de tocarla
    public float fallingTime = 0.5f;

    //La velodidad a la que cae la plataforma
    public float fallingSpeed = 0.098f;

    //¿Esta cayendo?
    public bool falling = false;

    void Update()
    {
        if (falling) 
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed, transform.position.z);
        }   
    }

    public void caer() 
    {
        falling = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hit_point")) 
        {
            Invoke("caer", fallingTime);
        }
    }
}
