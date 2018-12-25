using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttack : MonoBehaviour {

    public float timeBetweenAttacks = 10.0f;
    public int attackDamage = 10;
    public int scoreValue = 10;
    public AudioClip attackClip;
    AudioSource enemyAudio;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetBool("PlayerDead", true);
        }
    }


    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            anim.SetTrigger("Attack");
            //anim.ResetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);
            enemyAudio.clip = attackClip;
            enemyAudio.Play();
        }
    }
}

