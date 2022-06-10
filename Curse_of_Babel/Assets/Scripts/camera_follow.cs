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
    void Start()
    {
        alive = true;
    }

    void Update()
    {
        //La camara sigue al personaje solo en su posicion Y y a una distanca X y Z fija
        pos_jugador = player.transform.position;
        //si la niebla se hacerca mucho al jugador, empieza a subir la camara para que no se vea la parte inferior de la niebla
        if(push_signal != null || stop_signal != null)
        {
            EncontrarLimites();
        }
        if (push_signal != null || stop_signal != null) { 
            if (push_signal.position.y > pos_jugador.y)
            {
                pos_niebla.y = push_signal.position.y;
                pos_niebla.x = -6.0f;
                pos_niebla.z = pos_jugador.z - 10.0f;
                transform.position = pos_niebla;
            }
            //Cuando el jugador llega a la cima del nivel, la camara ya no sigue subiendo
            else if (stop_signal.position.y > pos_jugador.y && alive && push_signal.position.y < pos_jugador.y)
                {            
                pos_jugador.x = -6.0f;
                pos_jugador.z = pos_jugador.z - 10.0f;
                transform.position = pos_jugador;
                }
        }
    }
    //se llama cada que se instancia un nuevo nivel para tener un nuevo punto de referencia para detenerse
    public void New_Stop_Signal(Transform T)
    {
        stop_signal = T;
    }

    void EncontrarLimites()
    {
        stop_signal = GameObject.FindGameObjectWithTag("alto").transform;
        push_signal = GameObject.FindGameObjectWithTag("push_signal").transform;
        alive = true;
    }

}

