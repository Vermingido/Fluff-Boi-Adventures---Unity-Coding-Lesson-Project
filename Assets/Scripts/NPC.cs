using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float maxSpeed;
    public float minStandTime;
    public float maxStandTime;
    public float minMovementTime;
    public float maxMovementTime;
    public GameObject shop;
    public GameObject dialogue;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float speed;
    private float timeToWait;

    void ChooseMovement()
    {
        int choice = Random.Range(0,3);
        // Standing Still
        if(choice == 0)
        {
            timeToWait = Random.Range(minStandTime, maxStandTime);
            speed = 0;
        }
         // Go Left
        if(choice == 1)
        {
            timeToWait = Random.Range(minMovementTime, maxMovementTime);
            speed = -maxSpeed;
        }
         // Go Right
        if(choice == 2)
        {
            timeToWait = Random.Range(minMovementTime, maxMovementTime);
            speed = maxSpeed;
        }
    }

    public void OpenDialogue()
    {
        dialogue.SetActive(true);
        dialogue.GetComponent<Dialogue>().SetShop(shop);
    }

    public void OpenShop()
    {
        shop.SetActive(true);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        timeToWait -= Time.deltaTime;
        if(timeToWait <= 0)
        {
          ChooseMovement();  
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if(Mathf.Abs(rb.velocity.x) < .1)
        {
            GetComponent<Animator>().SetTrigger("Idle");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Run");
        }
        if(rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
