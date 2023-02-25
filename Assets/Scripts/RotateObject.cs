using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
   public float rotationSpeed = 1f;

   void Update()
   {
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
   }
}
