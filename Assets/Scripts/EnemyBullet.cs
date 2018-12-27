using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float speed;
    PlayerHealth playerHealth;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "player") {
            Debug.Log("shit");

            if (collision.gameObject.tag == "player") {
                playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(20);
            } Destroy(gameObject);
        }
    }

    private void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}