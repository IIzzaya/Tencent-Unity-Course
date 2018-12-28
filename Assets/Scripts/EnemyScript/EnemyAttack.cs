using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	[HideInInspector] public EnemyHealth enemyHealth;
	public bool playerInRange;
	private Animator animator;
	// public AudioClip attackClip;
	// AudioSource enemyAudio;

	private GameObject player;
	private PlayerHealth playerHealth;

	public float damage = 10f; // 怪物攻击力
	public int score = 10; // 怪物价值的分值
	public float cooldown = 1f; // 攻击间隔
	public bool isShootType = false; // 是否是远程攻击

	[Header("远程参数")]
	public Transform muzzle;
	public GameObject bulletPrefab;
	public float bulletSpeed = 4f;
	public float bulletLifeSpan = 3f;

	public void Attack(float yAngle) {
		animator.SetBool("isAttacking", true);
		if (isShootType) {
			var bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.Euler(0, yAngle, 0));
			var bulletInfo = bullet.GetComponent<EnemyBullet>();
			bulletInfo.speed = bulletSpeed;
			bulletInfo.damage = (int) damage;
			Destroy(bullet, bulletLifeSpan);
		} else {
			player.GetComponent<PlayerHealth>().TakeDamage((int) damage);
		}
	}

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		// enemyHealth = GetComponent<EnemyHealth>();
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			// Debug.Log("In");
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			// Debug.Log("Out");
			playerInRange = false;
		}
	}

	float timer;
	void Update() {
		timer += Time.deltaTime;

		if (playerInRange) {
			TryToAttack();
		} else {
			animator.SetBool("isAttacking", false);
		}
	}

	void TryToAttack() {
		if (timer >= cooldown) {
			timer = 0f;
			if (!playerHealth.isDead) {
				Attack(transform.eulerAngles.y);
			}
		}
	}

}