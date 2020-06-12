using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wander_behavior : MonoBehaviour
{
    public float speed;
    public float move_limit;
    public bool horizontal;

    float start_position;
    float current_position;
    float true_move_limit;
    public bool move_left;

    // Start is called before the first frame update
    void Start()
    { 
      if (horizontal)
      {
          start_position = transform.position.x;
          true_move_limit = start_position + move_limit;
      }
      else
      {
          start_position = transform.position.y;
          true_move_limit = start_position + move_limit;
      }
      
    }

    // Update is called once per frame
    void Update()
    {
      if (horizontal)
      {
          current_position = transform.position.x;
          true_move_limit = start_position + move_limit;
      }
      else
      {
          current_position = transform.position.y;
          true_move_limit = start_position + move_limit;
      }
      if (horizontal)
      {
        if (move_left)
        {
          transform.position += Vector3.left * Time.deltaTime * speed;
        }
        if (!move_left)
        {
          transform.position += Vector3.right * Time.deltaTime * speed;
        }
        if (current_position > true_move_limit && !move_left)
        {
            move_left = !move_left;
        }
        if (current_position < start_position && move_left)
        {
            move_left = !move_left;
        }
      }
      else
      {
        if (move_left)
        {
          transform.position += Vector3.down * Time.deltaTime * speed;
        }
        if (!move_left)
        {
          transform.position += Vector3.up * Time.deltaTime * speed;
        }
        if (current_position > true_move_limit && !move_left)
        {
            move_left = !move_left;
        }
        if (current_position < start_position && move_left)
        {
            move_left = !move_left;
        }
      }
        
    }
}
