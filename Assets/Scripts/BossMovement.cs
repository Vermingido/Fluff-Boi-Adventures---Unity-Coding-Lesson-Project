using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float attackRadius;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject player;
    public bool activeBattle;
    public float specialAttackChance;
    public bool attacking;
    public BoxCollider2D weapon;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void facePlayer()
    {
        if(attacking)
        {
            return;
        }
        if(transform.position.x < player.transform.position.x)
        {
            speed = maxSpeed;
        }
        else if(transform.position.x > player.transform.position.x)
        {
            speed = -maxSpeed;
        }
    }
    public void attackPlayer()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x)<= attackRadius && !attacking)
        {
            attacking = true;
            if(Random.Range(0f, 1f) <= specialAttackChance)
            {
                GetComponent<Animator>().SetTrigger("Attack_1");
            }
            else {
                GetComponent<Animator>().SetTrigger("Attack_2");
            }
            if(transform.position.x < player.transform.position.x)
            {
                speed = .01f;
            }
            else{
                speed = -.01f;
            }
        }
    }

    public void StartAttack()
    {
        weapon.gameObject.SetActive(true);
    }

    public void FinishAttack()
    {
        attacking = false;
        weapon.gameObject.SetActive(false);
    }
    public void Startup()
    {
        if(!activeBattle)
        {
            GetComponent<Animator>().SetTrigger("Startup");
            GetComponent<AudioSource>().Play(); 
        }
    }
    public void StartBattle()
    {
        activeBattle = true;
        GetComponent<Character>().healthBar.transform.parent.gameObject.SetActive(true);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(activeBattle)
        {
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
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
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
