using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class start_button : MonoBehaviour
{
	  public Saved_variables Svar;

    public void Start_game()
    {
        //Carga la escena de gameplay
        SceneManager.LoadScene("GameplayScene");
    }
}