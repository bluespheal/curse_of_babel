using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroll_enemy : enemy
{
    public float slide_speed; 

    float distance; // distancia para encontrar el suelo
    RaycastHit hit; //Raycast para detectar el suelo
    bool left; // dirección a la que está avanzando
    Vector3 direction = new Vector3(-1, -1, 0); // downwards direction of raycast

    void Start()
    {
        distance = 1; // distancia para encontrar el suelo siempre es 1.
    }

    void Update()
    {
        if (!stop)
        {
            transform.Translate(Vector3.left * slide_speed * Time.deltaTime); // Función de movimiento

            if (Physics.Raycast(transform.position, direction, distance))
            {
                ////Descomentar línea siguiente para ver Raycast para debuggeo.
                // Debug.DrawRay(transform.position,direction*distance,Color.green);
            }
            else
            {
                // Si no encuentra suelo enfrente de él, cambia de dirección.
                if (left == true)
                {
                    direction = new Vector3(-1, -1, 0);
                    left = !left;
                }
                else
                {
                    direction = new Vector3(1, -1, 0);
                    left = !left;
                }
                slide_speed = slide_speed * -1;
            }
        }

    }

}
