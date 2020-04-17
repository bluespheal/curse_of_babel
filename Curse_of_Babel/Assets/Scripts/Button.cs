using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public Saved_variables Svar;
    public void restart(){
        Svar.progreso.score = 0;
        SceneManager.LoadScene("GameplayScene");
    }

    public void BackToTitle(){
        Svar.progreso.score = 0;
        Svar.Guardar();
        SceneManager.LoadScene("TitleScreen");
    }
    public void settings()
    {
        SceneManager.LoadScene("settings");
    }
}
