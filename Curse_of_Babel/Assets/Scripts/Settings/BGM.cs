using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource bgm;
    private Saved_variables saved_variables;
    private static BGM BGMInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (BGMInstance == null)
        {
            BGMInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        saved_variables = GameObject.FindObjectOfType<Camera>().GetComponent<Saved_variables>();
        saved_variables.Cargar();
        bgm = GetComponent<AudioSource>();
        bgm.volume = saved_variables.progreso.BGM_Volume;
    }


    void OnLevelWasLoaded(int level)
    {
        saved_variables = GameObject.FindObjectOfType<Camera>().GetComponent<Saved_variables>();
        saved_variables.Cargar();
        bgm.volume = saved_variables.progreso.BGM_Volume;
    }


    void Update()
    {
        bgm.volume = saved_variables.progreso.BGM_Volume;
    }
}
