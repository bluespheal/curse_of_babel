using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_floor_detector : MonoBehaviour
{
    public Personaje jugador;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("platform") && jugador.velY <= 0)
        {
            jugador.knight_animation.SetBool("stomp", false);
            jugador.landing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("platform") && jugador.velY <= 0)
        {
            jugador.knight_animation.SetBool("on_air", false);
            jugador.Gdash = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("platform"))
        {
            //print("sali");
            jugador.knight_animation.SetBool("on_air", true);
            jugador.knight_animation.SetBool("Gdash", false);
        }
    }
}
