using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingtut : MonoBehaviour
{
    //El tiempo que tarda en caer la plataforma despues de tocarla
    public float fallingTime = 0.5f;

    //La velodidad a la que cae la plataforma
    public float fallingSpeed = 0.098f;

    //¿Esta cayendo?
    public bool falling = false;

    //Su posicion original
    public Vector3 ogPos;


    void Start()
    {
        //Obtiene la posicion inicial de la plataforma
        ogPos = transform.position;
    }

    void Update()
    {
        if (falling)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed, transform.position.z);
        }

        //Cuando la posicion en y de la plataforma sea menor o igual a 0, regresa a su posicion original.
        if(transform.position.y <= 0)
        {
            falling = false;
            transform.position = ogPos;
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
