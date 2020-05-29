using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytutorial : MonoBehaviour
{
    public float slide_speed; //Move speed
    public AudioSource death_sound;
    RaycastHit hit; //Raycast for floor detection
    public float distance; // distance to catch floor
    public bool left; // if it's going to the left
    Vector3 direction = new Vector3(-1, -1, 0); // downwards direction of raycast
    private Rigidbody rb;
    public ParticleSystem explosion;
    public GameObject tryagain;


    // Start is called before the first frame update
    void Start()
    {
        death_sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * slide_speed * Time.deltaTime); // movement function

        if (Physics.Raycast(transform.position, direction, distance))
        {
            ////Uncomment following line for debug line
            // Debug.DrawRay(transform.position,direction*distance,Color.green);
        }
        else
        {
            // if moving to left and finds a hole, turns vector and movement direction the opposite way
            if (left == true)
            {
                direction = new Vector3(-1, -1, 0);
                left = !left;
            }
            else
            {
                direction = new Vector3(1, -1, 0);
                left = !left;
            }
            slide_speed = slide_speed * -1;
        }

    }

    public void die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hit_point"))
        {
            explosion.GetComponent<ParticleSystem>().Play();
            other.GetComponent<hitpoint>().player.score_up(10);
            other.GetComponent<hitpoint>().player.bounce();
            death_sound.Play();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.position = tryagain.transform.position;
        }
    }
}
