using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public AudioSource death_sound;
    private Saved_variables saved_variables;

    private Rigidbody rb;
    public ParticleSystem explosion;
    public Transform skull;

    public bool stop;
    public Personaje player;

    // Start is called before the first frame update
    void Start() {
        saved_variables = GameObject.FindObjectOfType<Camera>().GetComponent<Saved_variables>();
        death_sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        skull = this.gameObject.transform.GetChild(0);
        stop = false;
        death_sound.volume = saved_variables.progreso.SFX_Volume;

    }
   
    public void die() {
        Destroy(this.gameObject);
        death_sound.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Personaje>();
        if (player.alive)
        { 
            if (other.gameObject.CompareTag("hit_point"))
            {   
                stop = true;
                skull.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<BoxCollider>().enabled = false;
                explosion.GetComponent<ParticleSystem>().Play();
                other.GetComponent<hitpoint>().player.score_up(10);
                other.GetComponent<hitpoint>().player.enemy_bounce();
                death_sound.Play();
                Invoke("die", 1f);
            }
        }
    }
}
