using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 5f;
    public List<Transform> wayPoints = new List<Transform>();
    public int currentWayPointIndex;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPointIndex].position,moveSpeed);
        if(Vector2.Distance(transform.position, wayPoints[currentWayPointIndex].position) < .01f)
        {
            currentWayPointIndex += 1;
            currentWayPointIndex %= wayPoints.Count;
        }
    }

}
