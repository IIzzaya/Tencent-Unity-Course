using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪由五大模块构成
/// </summary>
[System.Serializable]
public class Weapon : FloatingItem {

	[Header("Weapon Proerties 武器参数")]
	public Transform muzzle;
	public Transform equipRect;
	public float bulletLifeSpan = 3;

	public BarrelModule barrel;
	public BodyModule body;
	public GripModule grip;
	public ScopeModule scope;
	public StockModule stock;
	private List<WeaponModule> modules {
		get {
			var list = new List<WeaponModule>();
			if (barrel) list.Add(barrel);
			if (body) list.Add(body);
			if (grip) list.Add(grip);
			if (scope) list.Add(scope);
			if (stock) list.Add(stock);
			return list;
		}
	}

	[Header("Calculated Proerties 计算得参数")]
	public float damage;
	public float criticalRate;
	public float criticalDamageRate;
	public float fireRate;
	public float bulletSpeed;
	public float accuracy;
	public float magazineSize;
	public float reloadSpeed;
	public float scopeSightRate;
	public float bounceTimes;
	public float burstBulletCount;

	private float coolDown {
		get { return 1f / fireRate; }
	}
	private float coolDownTimer = 0;

	// 控制射出子弹的种类(module Part)：子弹/激光/导弹
	public void Fire(float yAngle) {
		if (body == null) {
			Debug.Log("Without body module cant fire");
			return;
		}

		if (Time.time - coolDownTimer > coolDown) {

			GameObject bullet;
			Bullet bulletInfo;

			switch (body.moduleType) {
				case EWeaponModuleType.Rifle:
				case EWeaponModuleType.Sniper:
				case EWeaponModuleType.Smg:

					if (barrel != null && barrel.isBurstType) {

					} else {
						bullet = Instantiate(body.bulletPrefab, muzzle.position, Quaternion.Euler(0, yAngle, 0));
						bulletInfo = bullet.GetComponent<Bullet>();
						bulletInfo.speed = bulletSpeed;
						bulletInfo.damage = (int) damage;
						bulletInfo.bounceLeftTimes = (int) bounceTimes;
						Destroy(bullet, bulletLifeSpan);
					}

					break;

				case EWeaponModuleType.Shotgun:
					var angleStep = body.diffuseAngle / (body.bulletCount + 1);
					var angleStart = -body.diffuseAngle / 2;

					for (int i = 0; i < body.bulletCount; i++) {
						bullet = Instantiate(body.bulletPrefab, muzzle.position, Quaternion.Euler(0, angleStart + (i + 1) * angleStep + yAngle, 0));
						bulletInfo = bullet.GetComponent<Bullet>();
						bulletInfo.speed = bulletSpeed;
						bulletInfo.damage = (int) damage;
						bulletInfo.bounceLeftTimes = (int) bounceTimes;
						Destroy(bullet, bulletLifeSpan);
					}
					break;

				case EWeaponModuleType.Laser:
					RaycastHit hit;
					var forward = muzzle.forward;
					forward.y = 0;
					if (Physics.Raycast(muzzle.position, forward, out hit, 100, LayerMask.GetMask("Default"))) {
						// Debug.DrawRay(muzzle.position, forward * hit.distance, Color.yellow);
						body.laser.gameObject.SetActive(true);
						body.laser.GetEndPoint().position = hit.point;
						body.laser.SetMidPoints();
						// Debug.Log("Did Hit");
					} else {
						// Debug.DrawRay(muzzle.position, forward * 100, Color.white);
						// Debug.Log("Did not Hit");
						body.laser.gameObject.SetActive(true);
						body.laser.GetEndPoint().position = muzzle.position + forward * 100;
						body.laser.SetMidPoints();
					}
					break;
				case EWeaponModuleType.Missile:
					break;
			}
			coolDownTimer = Time.time;
		}
	}

	public void Unfire() {
		if (body == null) { return; }
		switch (body.moduleType) {
			case EWeaponModuleType.Laser:
				body.laser.gameObject.SetActive(false);
				break;
		}
	}

	public void DropModule(WeaponModule module) {
		if (module == null) return;
		if (module.moduleType == EWeaponModuleType.Laser && module.modulePart == EWeaponModulePart.Body) {
			var laser = ((BodyModule) module).laser;
			laser.gameObject.SetActive(false);
			laser.transform.SetParent(module.transform);
			laser.transform.position = module.transform.position;
		}

		module.DropDown(transform.position);
	}

	public void EquipModule(WeaponModule module) {

		module.Equip();

		switch (module.modulePart) {
			case EWeaponModulePart.Barrel:
				DropModule(barrel);
				barrel = (BarrelModule) module;
				break;
			case EWeaponModulePart.Body:
				DropModule(body);
				body = (BodyModule) module;
				if (body.moduleType == EWeaponModuleType.Laser) {
					body.laser.transform.SetParent(muzzle);
					body.laser.transform.position = muzzle.position;
					body.laser.transform.rotation = muzzle.rotation;
					body.laser.gameObject.SetActive(true);
				}
				break;
			case EWeaponModulePart.Grip:
				DropModule(grip);
				grip = (GripModule) module;
				break;
			case EWeaponModulePart.Scope:
				DropModule(scope);
				scope = (ScopeModule) module;
				break;
			case EWeaponModulePart.Stock:
				DropModule(stock);
				stock = (StockModule) module;
				break;
		}

		UpdateModulesProperty();
	}

	public void EquipPresetModule(WeaponModule module) {

		module.Equip();

		switch (module.modulePart) {
			case EWeaponModulePart.Barrel:
				barrel = (BarrelModule) module;
				break;
			case EWeaponModulePart.Body:
				body = (BodyModule) module;
				if (body.moduleType == EWeaponModuleType.Laser) {
					body.laser.transform.SetParent(muzzle);
					body.laser.transform.position = muzzle.position;
					body.laser.transform.rotation = muzzle.rotation;
					body.laser.gameObject.SetActive(false);
				}
				break;
			case EWeaponModulePart.Grip:
				grip = (GripModule) module;
				break;
			case EWeaponModulePart.Scope:
				scope = (ScopeModule) module;
				break;
			case EWeaponModulePart.Stock:
				stock = (StockModule) module;
				break;
		}

		module.UpdateProperty();
		UpdateModulesProperty();
	}

	public void UpdateModulesProperty() {
		damage = 0;
		criticalRate = 0;
		criticalDamageRate = 0;
		fireRate = 0;
		bulletSpeed = 0;
		accuracy = 0;
		magazineSize = 0;
		reloadSpeed = 0;
		scopeSightRate = 0;
		bounceTimes = 0;
		burstBulletCount = 0;

		foreach (var item in modules) {
			damage += item._damageBase;
			criticalRate += item._criticalRateBase;
			criticalDamageRate += item._criticalDamageRateBase;
			fireRate += item._fireRateBase;
			bulletSpeed += item._bulletSpeedBase;
			accuracy += item._accuracyBase;
			magazineSize += item._magazineSizeBase;
			reloadSpeed += item._reloadSpeedBase;
			scopeSightRate += item._scopeSightRateBase;
			bounceTimes += item._bonuceTimesBase;
			burstBulletCount += item._burstBulletCountBase;
		}

		foreach (var item in modules) {
			damage *= item._damageMultiplier;
			criticalRate *= item._criticalRateMultiplier;
			criticalDamageRate *= item._criticalDamageRateMultiplier;
			fireRate *= item._fireRateMultiplier;
			bulletSpeed *= item._bulletSpeedMultiplier;
			accuracy *= item._accuracyMultiplier;
			magazineSize *= item._magazineSizeMultiplier;
			reloadSpeed *= item._reloadSpeedMultiplier;
			scopeSightRate *= item._scopeSightRateMultiplier;
			bounceTimes *= item._bonuceTimesMultiplier;
			burstBulletCount *= item._burstBulletCountMultiplier;
		}
	}

	public override void Equip() {
		base.Equip();
		model.position = equipRect.position;
		model.rotation = equipRect.rotation;
	}

	public override void Start() {
		base.Start();

		gameObject.tag = "Weapon";
		isEquippable = true;

		if (barrel != null) EquipPresetModule(barrel);
		if (body != null) EquipPresetModule(body);
		if (scope != null) EquipPresetModule(scope);
		if (stock != null) EquipPresetModule(stock);
		if (grip != null) EquipPresetModule(grip);

		UpdateModulesProperty();
	}

}