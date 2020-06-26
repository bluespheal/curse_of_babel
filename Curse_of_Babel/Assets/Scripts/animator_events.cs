using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animator_events : MonoBehaviour
{
    public Personaje personaje;

    public void idle_alt()//se comunica con el codigo del personaje para cambiar la animacion de idle
    {
        personaje.idle_alts();
    }
}
