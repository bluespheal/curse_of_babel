using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public Saved_variables Svar;
    public void restart(){
        Svar.progreso.nivelActual= Random.Range(0, 4);
        Svar.Guardar();
        SceneManager.LoadScene(Svar.progreso.nivelActual);
    }

    public void BackToTitle(){
        SceneManager.LoadScene("TitleScreen");
    }
}
