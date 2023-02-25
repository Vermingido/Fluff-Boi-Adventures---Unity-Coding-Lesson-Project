using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFlight : MonoBehaviour
{
    Rigidbody2D rb;
    bool hit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hit)
        {
          float angle = Mathf.Atan2(rb.velocity.y,rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);  
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        hit = true;
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Character>().TakeDamage();
        }
        if(hit)
        {
            rb.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
            transform.SetParent(collision.transform);
        }
    }
}
