using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_oclusion : MonoBehaviour
{
    // Start is called before the first frame update

    /*private void OnCollisionEnter(Collision collision)
    {
        print("Colision con: " + collision.gameObject.name);
        Renderer objeto = collision.gameObject.GetComponent<Renderer>();
        objeto.enabled = false;
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("decoration") || other.gameObject.CompareTag("platform") || other.gameObject.CompareTag("mist"))
        {
            Renderer objeto = other.gameObject.GetComponent<Renderer>();
            MeshRenderer piel = other.gameObject.GetComponent<MeshRenderer>();
            objeto.enabled = true;
            piel.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("decoration") || other.gameObject.CompareTag("platform") || other.gameObject.CompareTag("mist"))
        {
            Renderer objeto = other.gameObject.GetComponent<Renderer>();
            MeshRenderer piel = other.gameObject.GetComponent<MeshRenderer>();
            objeto.enabled = false;
            piel.enabled = false;

        }
    }
}
