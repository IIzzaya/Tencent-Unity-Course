using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    static int getid = 0;
    public int id;

    Animator anim;
    // AudioSource enemyAudio;
    // public AudioClip deathClip;
    // ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    public bool isDead = false;
    bool isSinking;

    public delegate void DeathDelegate();
    public event DeathDelegate DeathEvent;

    void Start() {
        id = GetId();
        int wave = EnemyManager.global.wave;
        startingHealth = Mathf.FloorToInt(50 * (10 - 8 * Mathf.Exp(-wave / 10)));
        anim = GetComponent<Animator>();
        // enemyAudio = GetComponent<AudioSource>();
        // hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        isDead = false;
        currentHealth = startingHealth;

    }

    void Update() {
        // anim.ResetTrigger("GetHit");
        if (isSinking) {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount) {
        if (isDead) return;

        // enemyAudio.Play();

        currentHealth -= amount;
        // anim.SetTrigger("GetHit");

        // hitParticles.transform.position = hitPoint;
        // hitParticles.Play();

        if (currentHealth <= 0) {
            Death();
        }
    }

    void Death() {
        isDead = true;
        EnemyManager.EnemyDelete(gameObject);

        if (DeathEvent != null) {
            DeathEvent.Invoke();
        }

        capsuleCollider.isTrigger = true;
        StartSinking();

        // anim.SetTrigger("Dead");
        // enemyAudio.clip = deathClip;
        // enemyAudio.Play();
    }

    public void StartSinking() {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }

    int GetId() {
        getid++;
        return getid;
    }
}