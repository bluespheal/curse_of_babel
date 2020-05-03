using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_mist : MonoBehaviour
{
    public Niebla mist;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mist.velocidad = 0;
        }
        print(other);
        /*jugador.muerto = true;*/
    }
}
