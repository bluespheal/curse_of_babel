using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_mist : MonoBehaviour
{
    public Niebla mist;

    private void OnTriggerEnter(Collider other)
    {
        //Si la niebla toca al jugador se detiene
        if (other.gameObject.CompareTag("Player"))
        {
            mist.velocidad = 0;
        }
    }
}
