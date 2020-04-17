using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    private AudioSource sfx;
    private Saved_variables saved_variables;
    // Start is called before the first frame update
    void Start()
    {
        saved_variables = GameObject.FindObjectOfType<Camera>().GetComponent<Saved_variables>();
        saved_variables.Cargar();
        sfx = GetComponent<AudioSource>();
        sfx.volume = saved_variables.progreso.SFX_Volume;
    }
    void Update()
    {
        sfx.volume = saved_variables.progreso.SFX_Volume;
    }
}
