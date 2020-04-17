using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource bgm;
    private Saved_variables saved_variables;
    // Start is called before the first frame update
    void Start()
    {
        saved_variables = GameObject.FindObjectOfType<Camera>().GetComponent<Saved_variables>();
        saved_variables.Cargar();
        bgm = GetComponent<AudioSource>();
        bgm.volume = saved_variables.progreso.BGM_Volume;
    }
    void Update()
    {
        bgm.volume = saved_variables.progreso.BGM_Volume;
    }
}
