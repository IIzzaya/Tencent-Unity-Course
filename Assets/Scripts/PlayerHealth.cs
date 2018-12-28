using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.2f);

    Animator anim;
    AudioSource playerAudio;
    PlayerController playerController;
    //PlayerShooting playerShooting;
    public bool isDead = false;
    bool damaged;

    void Awake() {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        // playerShooting = GetComponentInChildren <PlayerShooting>();
        currentHealth = startingHealth;
        isDead = false;
    }

    void Update() {
        if (damaged) {
            damageImage.color = flashColor;
        } else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount) {
        damaged = true;
        currentHealth -= amount;

        healthSlider.value = (float) currentHealth / startingHealth;

        // playerAudio.Play();

        if (currentHealth <= 0 && !isDead) {
            Death();
        }
    }

    void Death() {
        isDead = true;

        // playerShooting.DisableEffects ();
        // anim.SetTrigger("Die");

        // playerAudio.clip = deathClip;
        // playerAudio.Play();

        playerController.enabled = false;
        HUDController.GameOver();
        //playerShooting.enabled = false;
    }

    public void RestartLevel() {
        SceneManager.LoadScene(0);
    }

}