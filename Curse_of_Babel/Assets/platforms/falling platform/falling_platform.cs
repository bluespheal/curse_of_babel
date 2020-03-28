using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_platform : MonoBehaviour
{
    public float fallingTime=0.5f;
    public float fallingSpeed = 0.098f;
    public bool falling = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (falling) {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed, transform.position.z);
        }   
    }

    public void caer() {
        falling = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            print("voy a caer");
            Invoke("caer", fallingTime);
        }
    }
    //private void OnCollisionExit(Collision collision)
    //{
        //if (collision.gameObject.CompareTag("Player"))
        //{
            //CancelInvoke("caer");
        //}
    //}
}
