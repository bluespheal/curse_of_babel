using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private Saved_variables saved_variables;
    public Slider BGM;
    public Slider SFX;
    // Start is called before the first frame update
    void Start()
    {
        saved_variables = GameObject.FindObjectOfType<Camera>().GetComponent<Saved_variables>();
        saved_variables.Cargar();
        BGM.value = saved_variables.progreso.BGM_Volume;
        SFX.value = saved_variables.progreso.SFX_Volume;
    }

    // Update is called once per frame
    void Update()
    {
        saved_variables.progreso.BGM_Volume = BGM.value;
        saved_variables.progreso.SFX_Volume = SFX.value;
    }

    private void OnApplicationQuit()
    {
        saved_variables.Guardar();
    }
}
