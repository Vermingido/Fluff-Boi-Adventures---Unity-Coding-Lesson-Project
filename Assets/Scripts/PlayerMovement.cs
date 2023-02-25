using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveInput;
    public float speed;
    public float jumpSpeed;
    public Rigidbody2D rb;
    public bool pressedJump;
    public Transform checkpoint;
    public Animator animator;
    public bool grounded;
    public float landingRadius;
    public LayerMask groundLayer;
    public bool facingLeft;
    public Bow bow;
    private List<NPC> NPCsInRange = new List<NPC>();
    public PlayerStats stats;
    public Character character;
    public TextMeshProUGUI leatherScrapsText;
    public TextMeshProUGUI arrowText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        if(bow != null)
        {
            arrowText.text = bow.remainingArrows.ToString();
        }
        string currentScene = FindObjectOfType<SceneChanger>().current;
        if(currentScene.Equals("Save 2"))
        {
            transform.position = stats.forestPosition;
        }
        if(currentScene.Equals("First Cave Scene"))
        {
            transform.position = stats.cavePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal") * speed;
        Collider2D collider = Physics2D.OverlapCircle(transform.position,landingRadius,groundLayer);
        grounded = collider != null;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            pressedJump = true;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(NPCsInRange.Count > 0)
            {
                NPCsInRange[0].OpenDialogue();
            }
        }
        rb.velocity = new Vector2(moveInput, rb.velocity.y);
        if(pressedJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed);
            pressedJump = false;
        }
        if(facingLeft && rb.velocity.x > 0.1 || (!facingLeft && rb.velocity.x < -0.1))
        {
            facingLeft = !facingLeft;
            if(bow != null)
        {
            bow.flipped = !bow.flipped;
        }
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        animator.SetFloat("MoveX", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Grounded", grounded);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Activate");
            checkpoint = other.transform;
        }
        if (other.gameObject.CompareTag("Hazard"))
        {
            transform.position = checkpoint.transform.position;
        }
        if(other.gameObject.CompareTag("Portal"))
        {
            other.GetComponent<SceneChanger>().changeScene();
            stats.currentHP += 1;

        }
        if(other.gameObject.CompareTag("Barrier"))
        {
            other.gameObject.GetComponent<Barrier>().Activate();
        }

        if(other.gameObject.CompareTag("Start Battle"))
        {
            FindObjectOfType<BossMovement>().Startup();

        }
        if(other.gameObject.CompareTag("Leather Scrap"))
        {
            character.addLeatherScrap();
            leatherScrapsText.text = character.leatherScraps.ToString();
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag ("Enemy Weapon"))
        {
        GetComponent<Character>().TakeDamage();
        }
        if(other.gameObject.CompareTag ("Talk Area"))
        {
            NPCsInRange.Add(other.GetComponentInParent<NPC>());
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag ("Talk Area"))
            {
                NPCsInRange.Remove(other.GetComponentInParent<NPC>());
            }
    }

void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(other.transform,true);
        if(other.gameObject.CompareTag ("Enemy"))
        {
            other.gameObject.GetComponent<Character>().TakeDamage();
            GetComponent<Character>().TakeDamage();
        } 
    }

    void OnCollisionExit2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(null,true);
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerMovement movement &&
               base.Equals(obj) &&
               EqualityComparer<TextMeshProUGUI>.Default.Equals(leatherScrapsText, movement.leatherScrapsText);
    }
}
