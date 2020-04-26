using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_hscore : MonoBehaviour
{
    public Text Score;
    public Text HScore;
    public Saved_variables var;
    // Start is called before the first frame update
    void Start()
    {
        var=GameObject.FindGameObjectWithTag("Player").GetComponent<Saved_variables>();
        var.Cargar();
    }

    // Update is called once per frame
    void Update()
    {
        if (var.progreso.score > var.progreso.hscore) {
            var.progreso.hscore = var.progreso.score;
        }
        Score.text = "Score "+var.progreso.score.ToString();
        HScore.text = "High score "+var.progreso.hscore.ToString();
    }
}
