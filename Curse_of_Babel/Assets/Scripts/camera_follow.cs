using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public GameObject player;
    Transform stop_signal;
    Vector3 pos_jugador;
    // Use this for initialization
    void Start()
    {
        stop_signal = GameObject.FindGameObjectWithTag("alto").transform;
    }

    void Update()
    {
        pos_jugador = player.transform.position;
        // Temporary vector
        if (stop_signal.position.y > pos_jugador.y )
        {            
            pos_jugador.x = 0;
            pos_jugador.z = pos_jugador.z - 10.0f;
            // Assign value to Camera position
            transform.position = pos_jugador;
        }
    }
    //Camera.main.GetComponent<camera_follow>().New_Stop_Signal(transform_esfera);
    public void New_Stop_Signal(Transform T)
    {
       
        stop_signal = T;
    }

}

