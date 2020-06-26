using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wander_behavior : enemy
{
  public float speed;
  public float move_limit; //Declara el límite de movimiento.
  public bool horizontal; //Declara si se va a mover horizontal o verticalmente.
  public bool invert; //Declara si primero se moverá a la izquiera o derecha.

  private float start_position;
  private float current_position;
  private float true_move_limit;
  private bool move_back;


  void Start()
  { 
    start_position = DetermineStartPosition(horizontal);  //Declara la posición inicial de movimiento dependiendo si el movimiento es horizontal o vertical
    true_move_limit = DetermineStartTrueLimit(invert); //Declara la posición final de movimiento dependiendo si el movimiento está invertido
  }

  void Update()
  {
    if (!stop){ //mientras el enemigo siga vivo, se moverá
      current_position = CalculateCurrentPosition(horizontal); // Calcula y mueve la posición del enemigo

      if (!invert)// Calcula cuándo el enemigo tendrá que dar la vuelta a su movimiento
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

  private void MoveHorizontal(bool invert) //Mueve al enemigo de manera horizontal
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


  private void MoveVertical(bool invert) //Mueve al enemigo de manera vertical
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

  private void Move(Vector3 direction) //Mueve al enemigo a la dirección determinada.
  {
    transform.position += direction * Time.deltaTime * speed;
  }

}
