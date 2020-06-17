using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroll_enemy : enemy
{
    public float slide_speed; //Move speed
    RaycastHit hit; //Raycast for floor detection
    public float distance; // distance to catch floor
    public bool left; // if it's going to the left
    Vector3 direction = new Vector3(-1, -1, 0); // downwards direction of raycast

    // Update is called once per frame
     void Update()
    {
        if (!stop)
        {
            transform.Translate(Vector3.left * slide_speed * Time.deltaTime); // movement function

            if (Physics.Raycast(transform.position, direction, distance))
            {
                ////Uncomment following line for debug line
                // Debug.DrawRay(transform.position,direction*distance,Color.green);
            }
            else
            {
                // if moving to left and finds a hole, turns vector and movement direction the opposite way
                if (left == true)
                {
                    direction = new Vector3(-1, -1, 0);
                    left = !left;
                }
                else
                {
                    direction = new Vector3(1, -1, 0);
                    left = !left;
                }
                slide_speed = slide_speed * -1;
            }
        }

    }

}
