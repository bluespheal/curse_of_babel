using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niebla : MonoBehaviour
{
    //bool muevete = false;
    public float velocidad = 0.5f;

    void Update()
    {
        //Mueve la niebla de forma vertical a una velocidad constante
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        //Codigo para debug, Presiona Q para detener la niebla o haver que se mueva
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            muevete = !muevete;
        }
        if (muevete)
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        }*/
    }
}
