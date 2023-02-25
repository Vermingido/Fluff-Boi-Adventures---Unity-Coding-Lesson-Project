using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float attackRadius;
    public float sightRadius;
    public bool dead;
    public float pursuitDistance;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject player;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void facePlayer()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) <= sightRadius)
        {
            if(transform.position.x < player.transform.position.x)
            {
                speed = maxSpeed;
            }
            else if(transform.position.x > player.transform.position.x)
            {
                speed = -maxSpeed;
            }
        }
        if(Mathf.Abs(transform.position.x - player.transform.position.x) >= pursuitDistance)
        {
           speed = 0;
        }
    }
    
    public void attackPlayer()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x)<= attackRadius)
        {
            GetComponent<Animator>().SetTrigger("Attack");
            if(transform.position.x < player.transform.position.x)
            {
                speed = .01f;
            }
            else{
                speed = -.01f;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dead)
        {
            return;
        }
        facePlayer();
        attackPlayer();
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
    void OnCollisionEnter2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(other.transform,true);
    }

    void OnCollisionExit2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(null,true);
    }
}
