using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class start_button : MonoBehaviour
{
	  public Saved_variables Svar;

    public void Start_game(){
        Svar.progreso.nivelActual= Random.Range(0, 4);
        Svar.Guardar();
        SceneManager.LoadScene(Svar.progreso.nivelActual);
    }
}