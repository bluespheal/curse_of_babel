using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    public float moveSpeed = 5;
    public float turnSpeed = 5;
    public WaypointInfo[] wayPoints;
    private WaypointInfo currentWayPoint;
    private int currentWayPointIndex;

    private Transform myTransform;
    // Use this for initialization
    void Awake()
    {
        myTransform = transform; //assign the reference of Transform
        if (wayPoints.Length > 0)
        {
            currentWayPoint = wayPoints[0];//set initial waypoint
            currentWayPointIndex = 0;
        }
        else
        {
            Debug.LogError("No waypoint assigned");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Turning the object to the target
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, Quaternion.LookRotation(currentWayPoint.wayPoint - myTransform.position), Time.deltaTime * turnSpeed); // Smooth turning
                                                                                                                                                                            //Moving the object forwards
        myTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (currentWayPoint.IsWaypointReached(myTransform.position))
        {
            NextWaypoint();
        }

    }

    /// <summary>
    /// Assign Next waypoint in the list.
    /// </summary>
    private void NextWaypoint()
    {
        currentWayPointIndex++; // try to increase the index
        if (currentWayPointIndex > wayPoints.Length - 1)
        {
            currentWayPointIndex = 0; // if index is larger than list of waypoints, reset it to zero
        }

        currentWayPoint = wayPoints[currentWayPointIndex]; // assign current waypoint from the list
    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            if (i == 0)
            {
                Gizmos.color = new Color(0, 0.4f, 0);
            }
            else
            {
                Gizmos.color = new Color(0.6f, 1, 0.6f);
            }
            Gizmos.DrawCube(wayPoints[i].wayPoint, new Vector3(0.3f, 1, 0.3f));

            if (wayPoints.Length > 1)
            {
                Gizmos.color = Color.blue;
                if (i == 0)
                {
                    Gizmos.DrawLine(wayPoints[0].wayPoint, wayPoints[1].wayPoint);

                }
                else if (i == wayPoints.Length - 1)
                {
                    Gizmos.DrawLine(wayPoints[i - 1].wayPoint, wayPoints[i].wayPoint);
                    Gizmos.color = Color.grey;
                    Gizmos.DrawLine(wayPoints[wayPoints.Length - 1].wayPoint, wayPoints[0].wayPoint);
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(wayPoints[i - 1].wayPoint, wayPoints[i].wayPoint);
                }
            }
        }
    }
}

