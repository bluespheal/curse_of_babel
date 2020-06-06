using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saved_variables : MonoBehaviour
{
    public SAVEDATACLASS progreso; //es visible desde Inspector

    private void Start()
    {
        Cargar();
    }

    public void Guardar()
    {
        //Generamos el formato Json
        string Archivo = JsonUtility.ToJson(progreso);
        //Lo guardamos con PlayerPrefs
        PlayerPrefs.SetString("keySave", Archivo);
    }
    public void Cargar()
    {
        string Archivo = PlayerPrefs.GetString("keySave");
        //Solo seguridad que si exista algo
        if (!string.IsNullOrEmpty(Archivo))
        {
            progreso = JsonUtility.FromJson<SAVEDATACLASS>(Archivo);
        }
    }
}

[System.Serializable]
public class SAVEDATACLASS
{
    public int nivelActual;
    public int score;
    public int hscore;
    public float BGM_Volume;
    public float SFX_Volume;
    public bool Tutorial;
}
