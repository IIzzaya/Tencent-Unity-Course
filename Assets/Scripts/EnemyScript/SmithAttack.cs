using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage;
    public int scoreValue = 10;
    public AudioClip attackClip;
    AudioSource enemyAudio;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    public GameObject enemyManager;
    int wave;

    void Awake() {
        EnemyHealth self = gameObject.GetComponent<EnemyHealth>();
        int id = self.id;
        wave = enemyManager.GetComponent<EnemyManager>().wave;
        player = GameObject.FindGameObjectWithTag("Player");
        attackDamage = Mathf.FloorToInt(4 * (10 - 8 * Mathf.Exp(-wave/10)));
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            playerInRange = false;
        }
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange /* && enemyHealth.currentHealth > 0*/ ) {
            Attack();
        }

    }

    


    void Attack()
    {
        timer = 0f;
        anim.ResetTrigger("Attack");

        if (playerHealth.currentHealth > 0) {
            anim.SetTrigger("Attack");
            //anim.ResetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);
            enemyAudio.clip = attackClip;
            enemyAudio.Play();
        }
    }
}
