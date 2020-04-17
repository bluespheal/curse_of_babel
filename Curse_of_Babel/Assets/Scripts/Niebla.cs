using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niebla : MonoBehaviour
{
    //public Mono jugador;
    bool muevete =false;
    public float velocidad = 0.5f;
    void Start()
    {
        Rigidbody rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            muevete = !muevete;
        }
        if (muevete)
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other);
        /*jugador.muerto = true;*/
    }

}
