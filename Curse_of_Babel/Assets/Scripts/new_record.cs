using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class new_record : MonoBehaviour
{
  public bool is_new_record; 
  public Image img; //imagen de nuevo record
  public Saved_variables var;


    // Start is called before the first frame update
    void Start()
    {
      if (var.progreso.score == var.progreso.hscore) //Revisa si el nuevo record es igual al record actual
      {
        is_new_record = true;
      }
      else
      {
        is_new_record = false;
      }

      //Activa o desactiva la imagen de nuevo record si se logró romper el rércord anterior
      if(!is_new_record)
        {
          img.enabled = false;
        }
    }
}
