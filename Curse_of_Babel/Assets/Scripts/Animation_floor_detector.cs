using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_floor_detector : MonoBehaviour
{
    public Personaje jugador;
    private void OnTriggerEnter(Collider other)
    {
        //Al entrar en contacto con la plataforma  le indica al jugador que ya no esta en el aire
        //y al cambiar landing a true empieza una funcion de "aterrizage" para que el jugador no pueda saltar en cuanto toca una plataforma
        if (other.gameObject.CompareTag("platform") && jugador.velY <= 0)
        {
            //Si el jugador aterrizo con un stomp reproduce unas particulas y quita la animacion de Stomp
            if(jugador.stomp)
            {
                jugador.particlesStomp.Play();
                jugador.stomp = false;
                jugador.knight_animation.SetBool("stomp", false);
            }
            jugador.landing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Mientras este en contacto con una plataforma y no se este moviendo
        //el siguiente dash que no lo separe de la plataforma sera un "Ground Dash"
        if (other.gameObject.CompareTag("platform") && jugador.velY <= 0)
        {
            jugador.knight_animation.SetBool("on_air", false);
            jugador.Gdash = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Al salir de la plataforma interrumpe animaciones de Dash y pone la animacion de estar en el aire
        if (other.gameObject.CompareTag("platform"))
        {
            jugador.knight_animation.SetBool("on_air", true);
            jugador.knight_animation.SetBool("Gdash", false);
        }
    }
}
