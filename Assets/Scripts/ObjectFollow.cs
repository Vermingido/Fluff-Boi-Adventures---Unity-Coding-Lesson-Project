using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;

    private void Update()
    {
        if(objectToFollow != null)
            transform.position = objectToFollow.position + offset;
    }
}
