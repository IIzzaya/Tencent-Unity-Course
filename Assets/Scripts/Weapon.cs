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
	public Transform model;
	public Transform equipRect;
	public GameObject bulletPrefab;
	public bool isEquipped = false;
	private Collider itemCollider;

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

	public void Equip() {
		isEquipped = true;
		model.position = equipRect.position;
		model.rotation = equipRect.rotation;
		model.localScale = equipRect.localScale;
		
		if (itemCollider == null)
			itemCollider = GetComponent<Collider>();
		itemCollider.enabled = false;
	}

	public void DropDown() {
		isEquipped = false;
		itemCollider.enabled = true;
	}

	private float floatingTimer = 0f;
	private float floatingFrequency = 3f;
	private float floatingAmplitude = 0.5f;
	public void WaitToPickAnimation() {

		var rotation = model.eulerAngles;
		rotation.y += 30f * Time.deltaTime;
		model.rotation = Quaternion.Euler(rotation);

		floatingTimer += Time.deltaTime;
		if (floatingTimer > 2 * floatingFrequency) { floatingTimer = 0f; }
		var position = model.position;
		position.y = transform.position.y + Mathf.Cos(Mathf.PI * (floatingTimer - floatingFrequency) / floatingFrequency) * floatingAmplitude;
		model.position = position;
	}

	private void Start() {
		if (!isEquipped) {
			itemCollider = GetComponent<Collider>();
			DropDown();
		}
	}

	private void Update() {
		if (!isEquipped) {
			WaitToPickAnimation();
		}
	}
}