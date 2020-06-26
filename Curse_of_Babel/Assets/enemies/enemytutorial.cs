using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytutorial : MonoBehaviour
{
    public AudioSource death_sound;
    private Rigidbody rb;
    public ParticleSystem explosion;
    public GameObject tryagain; 
    public Animator transitionAnim;// Una animación de transición al momento de morir por este enemigo


    // Start is called before the first frame update
    void Start()
    {
        death_sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador lo golpea con su hitpoint, manda a llamar el sonido y efectos solamente.
        if (other.gameObject.CompareTag("hit_point")) 
        {
            explosion.GetComponent<ParticleSystem>().Play();
            other.GetComponent<hitpoint>().player.enemy_bounce();
            death_sound.Play();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Si el jugador colisiona con el enemigo, lo manda a donde esté el objeto tryagain.
        GameObject other = collision.gameObject;
        if (collision.transform.CompareTag("Player"))
        {   
            other.GetComponent<Personaje>().goto_origin();
            // muestra una transición a negro cuando el enemigo daña al jugador.
            transitionAnim.SetTrigger("tutorial_fade_out");
        }
    }
}
