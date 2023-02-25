using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Leather Scrap"))
        {
            GetComponent<Character>().loot.Add(other.GetComponent<Loot>());
            other.gameObject.SetActive(false);
            Debug.Log("Deactivated!");
        }
    }
}
