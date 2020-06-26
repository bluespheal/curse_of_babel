using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public ParticleSystem explosion; //Explosión de cuando muere el enemigo.
    public Personaje player;
    public bool stop; // detiene al enemigo si es derrotado.

    private Rigidbody rb;
    private Transform skull; //modelo del enemigo.
    private Saved_variables saved_variables; // Obtiene información para determinar el volumen de los sonidos del enemigo.
    private AudioSource death_sound; //Sonido cuando muere el enemigo.

    void Start() {
        saved_variables = GameObject.FindObjectOfType<Camera>().GetComponent<Saved_variables>();
        death_sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        skull = this.gameObject.transform.GetChild(0);
        stop = false; // Se asegura que el enemigo se mueva.
        death_sound.volume = saved_variables.progreso.SFX_Volume; //Obtiene el volumen guardado y lo asigna.

    }
   
    public void die() { //Elimina al enemigo una vez se corre su animación de muerte.
        Destroy(this.gameObject);
        death_sound.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Personaje>(); //Se asegura que el que colisiona sea el jugador
        if (player.alive)
        { 
            if (other.gameObject.CompareTag("hit_point")) //Si choca con el hitpoint del personaje.
            {   
                stop = true;
                // Desabilita muchos componentes del enemigo en lo que se elimina
                skull.GetComponent<MeshRenderer>().enabled = false; 
                this.GetComponent<BoxCollider>().enabled = false;
                //activa la explosión del enemigo y su sonido
                explosion.GetComponent<ParticleSystem>().Play();
                death_sound.Play();
                //Le da 10 puntos al jugador y hace que rebote
                other.GetComponent<hitpoint>().player.score_up(10);
                other.GetComponent<hitpoint>().player.enemy_bounce();
                //Tras un segundo, invoca el método de morir
                Invoke("die", 1f);
            }
        }
    }
}
