using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wander_behavior : MonoBehaviour
{
    public float velocidad;

    public float limite_izquierdo = 0.0f;
    public float limite_derecho = 0.0f;
    public float limite_top = 0.0f;
    public float limite_bot = 0.0f;

    bool izquierda = true; 
    bool derecha = false;
    bool arriba = true;
    bool abajo = false;
    bool retrato = false;
    bool horizonte = false;
    // Start is called before the first frame update
    void Start()
    {
        if (limite_top == limite_bot)
        {
            arriba = false;
            horizonte = true;
        }
        if (limite_izquierdo == limite_derecho)
        {
            izquierda = false;
            retrato = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(horizonte)
        {
            if (izquierda && transform.position.x > limite_izquierdo)
            {
                transform.position += Vector3.left * Time.deltaTime * velocidad;
            }
            if (derecha && transform.position.x < limite_derecho)
            {
                transform.position += Vector3.right * Time.deltaTime * velocidad;
            }
            if (transform.position.x >= limite_derecho)
            {
                derecha = false;
                izquierda = true;
            }
            if (transform.position.x <= limite_izquierdo)
            {
                derecha = true;
                izquierda = false;
            }
        }
        
        if(retrato)
        {
            if (arriba && transform.position.y < limite_top)
            {
                transform.position += Vector3.up * Time.deltaTime * velocidad;
            }
            if (abajo && transform.position.y > limite_bot)
            {
                transform.position += Vector3.down * Time.deltaTime * velocidad;
            }
            if (transform.position.y >= limite_top)
            {
                arriba = false;
                abajo = true;
            }
            if (transform.position.y <= limite_bot)
            {
                arriba = true;
                abajo = false;
            }
        }
        
    }
}
