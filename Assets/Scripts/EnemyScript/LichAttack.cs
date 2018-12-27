using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttack : MonoBehaviour {

    public float timeBetweenAttacks = 1f;
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

    public Transform weapon;
    private Weapon weaponInfo;
    int i = 0;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        weaponInfo = weapon.GetComponent<Weapon>();
        weapon.position = weapon.position;
        weapon.rotation = weapon.rotation;

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag  == "LitchAttack")
        {
            playerInRange = true;
            //Debug.Log("In");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LitchAttack")
        {
            playerInRange = false;
            //Debug.Log("Out");
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        anim.ResetTrigger("Attack1");
       


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
            
                anim.SetTrigger("Attack1");
            
               
            
            weaponInfo.Fire(transform.rotation.eulerAngles.y);
            
        }
    }
}

