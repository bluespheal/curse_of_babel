using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public Personaje jugador;
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("platform") && jugador.velY <= 0)
         {
             jugador.knight_animation.SetBool("stomp", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("platform") && jugador.velY <= 0)
        {
            jugador.knight_animation.SetBool("on_air", false);
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
