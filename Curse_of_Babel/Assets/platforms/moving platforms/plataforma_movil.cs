using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma_movil : MonoBehaviour
{
    public float speed;
    public float move_limit; //Declara el límite de movimiento.
    public bool horizontal; //Declara si se va a mover horizontal o verticalmente.
    public bool invert; //Declara si primero se moverá a la izquiera o derecha.

    private float start_position;
    private float current_position;
    private float true_move_limit;
    private bool move_back;


    // Start is called before the first frame update
    void Start()
    { 
      start_position = DetermineStartPosition(horizontal); //Declara la posición inicial de movimiento dependiendo si el movimiento es horizontal o vertical
      true_move_limit = DetermineStartTrueLimit(invert); //Declara la posición final de movimiento dependiendo si el movimiento está invertido
    }

    // Update is called once per frame
    void Update()
    {
      current_position = CalculateCurrentPosition(horizontal); // Calcula y mueve la posición de la plataforma

      if (!invert)// Calcula cuándo la plataforma tendrá que dar la vuelta a su movimiento
      {
        if (current_position > true_move_limit && !move_back || current_position < start_position && move_back)
        {
            move_back = !move_back;
        }
      }
      else
      {
        if (current_position < true_move_limit && !move_back || current_position > start_position && move_back)
        {
            move_back = !move_back;
        }
      }

      if (horizontal)
      {
        MoveHorizontal(invert); 
      }
      else
      {
        MoveVertical(invert);
      }  
    }

    private float DetermineStartPosition(bool horizontal) //Determina la posición inicial de acuerdo con la dirección
    {
      if (horizontal)
      {
        return transform.position.x;
      }
      else
      {
        return transform.position.y;
      } 
    }

    private float CalculateCurrentPosition(bool horizontal) // Calcula y mueve la posición
    {
      if (horizontal)
      {
        return transform.position.x;
      }
      else
      {
        return transform.position.y;
      }
    }

    private float DetermineStartTrueLimit(bool invert) //Determina el límite de movimiento real
    {
      if(invert)
      {
        return start_position - move_limit;
      } 
      else
      {
        return start_position + move_limit;
      }
    }

    private void MoveHorizontal(bool invert) //Mueve la plataforma de manera horizontal
    {
      if (!invert){
        if (move_back)
        {
          Move(Vector3.left);
        }
        else
        {
          Move(Vector3.right);
        }   
      }
      else
      {
        if (move_back)
        {
          Move(Vector3.right);
        }
        else
        {
          Move(Vector3.left);
        }  
      }
    }


    private void MoveVertical(bool invert) //Mueve la plataforma de manera vertical
    {
      if (!invert){
        if (move_back)
        {
          Move(Vector3.down);
        }
        else
        {
          Move(Vector3.up);
        }   
      }
      else
      {
        if (move_back)
        {
          Move(Vector3.up);
        }
        else
        {
          Move(Vector3.down);
        }  
      }
    }

    private void Move(Vector3 direction) //Mueve la plataforma de acuerdo a la dirección
    {
      transform.position += direction * Time.deltaTime * speed;
    }

    public void OnCollisionEnter(Collision collision) //Hace que el jugador se haga hijo de la plataforma mientras está sobre ella
    {
      if (collision.gameObject.CompareTag("Player"))
      {
          collision.transform.parent = transform;
      }
    }
    public void OnCollisionExit(Collision collision) //Hace que el jugador se deje de ser hijo de la plataforma al dejar de tocarla
    {
      if (collision.gameObject.CompareTag("Player"))
      {
          collision.transform.parent = null;
          collision.transform.localScale = new Vector3(1.6f,1.6f,1.6f); //Hace que la escala del jugador se mantenga constante 
      }
    }
}
