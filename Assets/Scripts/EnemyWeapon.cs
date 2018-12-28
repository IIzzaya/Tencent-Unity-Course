using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

	public Transform muzzle;
	public float cooldown;
	public GameObject bulletPrefab;
	public float bulletSpeed=4f;
	public float bulletLifeSpan=3f;

	public void Fire(float yAngle) {
		Debug.Log(yAngle);
		var bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.Euler(0, yAngle, 0));
		var bulletInfo = bullet.GetComponent<EnemyBullet>();
		bulletInfo.speed = bulletSpeed;
		Destroy(bullet, bulletLifeSpan);
	}
}