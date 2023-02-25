using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private Vector2 bowPosition;
    private Vector2 mousePosition;
    private Vector2 direction;
    public GameObject arrow;
    public float launchSpeed;
    public Transform firingPosition;
    public float reloadTime;
    public int remainingArrows;
    public float timeSinceShot;
    public bool flipped;
    public AudioClip shoot;
    public AudioSource audioSource;
    void Update()
    {
        bowPosition = transform.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        if(flipped)
        {
            direction *= -1;
        }
        transform.right = direction;
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        timeSinceShot += Time.deltaTime;
    }

    private void Shoot()
    {
        if(timeSinceShot > reloadTime && remainingArrows > 0)
        {
            GameObject newArrow = Instantiate(arrow, firingPosition.position, firingPosition.rotation);
            if(flipped)
            {
                newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchSpeed * -1;
            }
            else
            {
                newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchSpeed;
            }
            timeSinceShot = 0;
            remainingArrows -= 1;
            FindObjectOfType<PlayerMovement>().arrowText.text = remainingArrows.ToString();
            audioSource.PlayOneShot (shoot);
        }
    }
}