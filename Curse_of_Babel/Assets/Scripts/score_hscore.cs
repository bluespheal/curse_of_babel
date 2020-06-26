using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_hscore : MonoBehaviour
{
    public Text Score;
    public Text HScore;
    public Saved_variables var;

    void Start()
    {
        var = GameObject.FindGameObjectWithTag("Player").GetComponent<Saved_variables>(); //Saca las variables guardadas del player.
        var.Cargar(); // carga las variables de score guardadas en var
    }

    void Update()
    {
        var.Guardar(); // Guarda las variables a var
        if (var.progreso.score > var.progreso.hscore) {
            var.progreso.hscore = var.progreso.score; //Actualiza el hscore si score es mayor
        }
        //Muestra los score en pantalla, en los Text correspondientes.
        Score.text = "Score "+var.progreso.score.ToString(); 
        HScore.text = "High score "+var.progreso.hscore.ToString();
    }
}
