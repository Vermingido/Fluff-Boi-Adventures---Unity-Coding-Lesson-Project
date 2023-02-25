using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Vector2 xChange;
    public Vector2 yChange;
   public void SpawnAt(Vector3 position)
   {
        transform.position = new Vector3(position.x + Random.Range(xChange.x, xChange.y), position.y + Random.Range(yChange.y, yChange.y));
   }
}