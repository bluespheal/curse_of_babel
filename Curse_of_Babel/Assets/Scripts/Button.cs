using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public Saved_variables Svar;
    public void restart(){
        SceneManager.LoadScene("GameplayScene");
    }

    public void BackToTitle(){
        SceneManager.LoadScene("TitleScreen");
    }
}
