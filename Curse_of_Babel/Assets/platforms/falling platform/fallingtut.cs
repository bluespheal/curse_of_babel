using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingtut : MonoBehaviour
{
    public float fallingTime = 0.5f;
    public float fallingSpeed = 0.098f;
    public bool falling = false;
    public Vector3 ogPos;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
    }

    void Update()
    {
        if (falling)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed, transform.position.z);
        }
        if(transform.position.y <= 0)
        {
            falling = false;
            transform.position = ogPos;
        }
    }

    public void caer()
    {
        falling = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hit_point"))
        {
            Invoke("caer", fallingTime);
        }
    }
}
