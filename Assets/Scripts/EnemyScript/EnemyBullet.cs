using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float speed;
    public int damage;
    PlayerHealth playerHealth;

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Player") {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }

    }

    private void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}