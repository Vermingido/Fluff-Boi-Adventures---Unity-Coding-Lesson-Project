using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public int maxHP;
    public int curHP;
    public int atk;
    public ParticleSystem particles;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public int leatherScraps;
    public List<Loot> loot;
    public AudioClip lootSound;
    public Bar healthBar;
    public bool dead;
    public GameObject healthBarPrefab;
    public GameObject weapon;

    public void Awake()
    {
        if(healthBar == null)
        {
            GameObject newHealthBar = Instantiate(healthBarPrefab);
            newHealthBar.gameObject.GetComponent<ObjectFollow>().objectToFollow = this.transform;
            newHealthBar.transform.SetParent(GameObject.FindGameObjectWithTag("World Canvas").transform);
            healthBar = newHealthBar.GetComponentInChildren<Bar>();
        }
        if(healthBar != null)
        {
            healthBar.maxHP = maxHP;
            healthBar.currentHP = curHP;
        }
        if(GetComponent<BossMovement>() != null)
        {
            healthBar.transform.parent.gameObject.SetActive(false);
        }

    }
    public void addLeatherScrap()
    {
        leatherScraps += 1;
        if(audioSource != null)
            audioSource.PlayOneShot(lootSound);
    }
    public void TakeDamage()
    {
        curHP -= 1;
        if(healthBar != null)
            healthBar.currentHP = curHP;
        particles.Play();
        if(audioSource !=  null)
            audioSource.PlayOneShot(hitSound);
        if(curHP <= 0 && !dead)
        {
            Die();
        }
    }
    public void ActivateWeapon()
    {
        weapon.SetActive(true);
    }
    public void DeactivateWeapon()
    {
        weapon.SetActive(false);
    }
    public void Die()
    {
        if(loot.Count > 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<CapsuleCollider2D>().enabled = false;
             if(healthBar != null)
            {
                Destroy(healthBar.transform.parent.gameObject);
            }
            foreach (var item in loot)
            {
                Loot droppedLoot = Instantiate(item, transform.position, transform.rotation);
                droppedLoot.gameObject.SetActive(true);
                droppedLoot.SpawnAt(transform.position);
            }
            
            dead = true;
            GetComponent<EnemyMovement>().dead = true;
            Destroy(this.gameObject,5);
        }
        else
        {
            SceneManager.LoadScene("Game Over");
        }
        GetComponentInChildren<Animator>().SetTrigger("Die");
    }
}