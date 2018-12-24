using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeaponType {
	Rifle,
	Laser,
	Missile
}

public class Weapon : MonoBehaviour {

	public Transform muzzle;
	public GameObject bulletPrefab;

	[Header("Weapon Proerties 武器参数")]
	[SerializeField]
	public EWeaponType type;
	public float coolDown;
	private float coolDownTimer = 0;
	public float bulletSpeed;
	public float bulletLifeSpan;
	public int bounceTimes;

	public void Fire(float yAngle) {
		switch (type) {

			case EWeaponType.Rifle:
				if (Time.time - coolDownTimer > coolDown) {
					var bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.Euler(0, yAngle, 0));
					var bulletInfo = bullet.GetComponent<Bullet>();
					bulletInfo.speed = bulletSpeed;
					bulletInfo.bounceLeftTimes = bounceTimes;
					Destroy(bullet, bulletLifeSpan);
					coolDownTimer = Time.time;
				}
				break;

			case EWeaponType.Laser:

				break;

			case EWeaponType.Missile:

				break;
		}

	}
}