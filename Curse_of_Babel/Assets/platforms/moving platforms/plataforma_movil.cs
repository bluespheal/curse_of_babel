using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma_movil : MonoBehaviour
{
    public float speed;
    public float move_limit;
    public bool horizontal;

    public float start_position;
    public float current_position;
    public float true_move_limit;
    public bool move_back;

    public bool invert;

    // Start is called before the first frame update
    void Start()
    { 
      start_position = DetermineStartPosition(horizontal);
      true_move_limit = DetermineStartTrueLimit(invert);
    }

    // Update is called once per frame
    void Update()
    {
      current_position = CalculateCurrentPosition(horizontal);

      if (!invert)
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

    private float DetermineStartPosition(bool horizontal)
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

    private float CalculateCurrentPosition(bool horizontal)
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

    private float DetermineStartTrueLimit(bool invert)
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

    private void MoveHorizontal(bool invert)
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


    private void MoveVertical(bool invert)
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

    private void Move(Vector3 direction)
    {
      transform.position += direction * Time.deltaTime * speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
            // collision.transform.localScale = new Vector3(1.6f,1f,1f);
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
            collision.transform.localScale = new Vector3(1.6f,1.6f,1.6f);
        }
    }
}
