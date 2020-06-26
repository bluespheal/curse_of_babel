using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_button : MonoBehaviour
{
    public Saved_variables saved_variables;

    void Start()
    {
        saved_variables.Cargar();
        if (saved_variables.progreso.Tutorial)
        {
            this.gameObject.SetActive(false);
        }
        else 
        {
            this.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            saved_variables.progreso.score = 0;
            saved_variables.progreso.hscore = 0;
            saved_variables.progreso.Tutorial = true;
        }
    }
}
