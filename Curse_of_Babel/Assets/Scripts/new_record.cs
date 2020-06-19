using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class new_record : MonoBehaviour
{
  public bool is_new_record;
  public Image img;
  public Saved_variables var;


    // Start is called before the first frame update
    void Start()
    {
      if (var.progreso.score == var.progreso.hscore)
      {
        is_new_record = true;
      }
      else
      {
        is_new_record = false;
      }

      if(!is_new_record)
        {
          img.enabled = false;
        }
    }
}
