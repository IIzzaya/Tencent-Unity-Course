using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttack : MonoBehaviour {

    public float timeBetweenAttacks = 10.0f;
    public int attackDamage = 10;
    public int scoreValue = 10;
    //public AudioClip attackClip;
    AudioSource enemyAudio;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    Vector3 dir;
    Vector3 pos;
    RaycastHit hit;
    


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
       
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag  == "LitchAttack")
        {
            playerInRange = true;
            Debug.Log("In");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LitchAttack")
        {
            playerInRange = false;
            Debug.Log("Out");
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
 

        if (timer >= timeBetweenAttacks &&  playerInRange == true)
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
            //enemyAudio.clip = attackClip;
            //enemyAudio.Play();
            pos = transform.position;
            if (Physics.SphereCast(pos, 6.0f, transform.forward, out hit, 5))
            {
                Debug.Log("shit");
                if (hit.collider.gameObject.tag == "player")
                {
                    //playerHealth.TakeDamage(attackDamage);
                    
                }
            }
            
            //anim.ResetTrigger("Attack");
            //playerHealth.TakeDamage(attackDamage);
            
        }
    }
}

