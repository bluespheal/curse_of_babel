using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_over_scores : MonoBehaviour
{
    // Start is called before the first frame update
    public Text Score;
    public Text HScore;
    public Saved_variables var;
    // Start is called before the first frame update
    void Start()
    {
        var = GameObject.FindGameObjectWithTag("Player").GetComponent<Saved_variables>();
        var.Cargar();
        Score.text = "Score " + var.progreso.score.ToString();
        HScore.text = "High score " + var.progreso.hscore.ToString();
    }
}
