using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public GameObject player;
    public Transform stop_signal;
    public Transform push_signal;
    Vector3 pos_jugador;
    Vector3 pos_niebla;
    public bool alive = true;
    // Use this for initialization
    void Start()
    {
        alive = true;
        //stop_signal = GameObject.FindGameObjectWithTag("alto").transform;
        //push_signal = GameObject.FindGameObjectWithTag("push_signal").transform;
    }

    void Update()
    {
        pos_jugador = player.transform.position;
        if(push_signal.position.y > pos_jugador.y)
        {
            pos_niebla.y = push_signal.position.y;
            pos_niebla.x = -6.0f;
            pos_niebla.z = pos_jugador.z - 10.0f;
            transform.position = pos_niebla;
        }
        else if (stop_signal.position.y > pos_jugador.y && alive && push_signal.position.y < pos_jugador.y)
         {            
            pos_jugador.x = -6.0f;
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

