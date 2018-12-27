using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪体模块，枪的核心部分
/// </summary>
public class BodyModule : WeaponModule {

	[Header("BodyModule Properties")]
	public GameObject bulletPrefab;
	public float damageBase = 0f; // 伤害基数
	public float criticalRateMultiplier = 1f; // 暴击率乘数
	public float criticalDamageRateMultiplier = 1f; // 暴击伤害倍率乘数
	public float fireRateBase = 0f; // 射速基数
	public float fireRateMultiplier = 1f; // 射速乘数
	public float bulletSpeedBase = 0f; // 子弹飞行速度基数
	public float bulletSpeedMultiplier = 1f; // 子弹飞行速度乘数
	public float accuracyMultiplier = 1f; // 射击精准度乘数
	public float magazineSizeMultiplier = 1f; // 弹夹容量乘数
	public float reloadSpeedMultiplier = 1f; // 装弹速度乘数
	public float scopeSightRateMultiplier = 1f; // 视野倍率乘数
	public float bonuceTimesBase = 0f; // 子弹反弹基数
	public float bonuceTimesMultiplier = 1f; // 子弹反弹乘数
	public float burstBulletCountMultiplier = 1f; // 连发乘数

	[Header("* Shotgun Only 霰弹枪参数")]
	public int bulletCount = 1;
	public float diffuseAngle = 0;

	[Header("* Laser Only 激光枪参数")]
	public LaserRenderer laser;

	public override void GetRandomProperties() {
		
	}

	public override void UpdateProperty() {
		_damageBase = damageBase;
		_criticalRateMultiplier = criticalRateMultiplier;
		_criticalDamageRateMultiplier = criticalDamageRateMultiplier;
		_fireRateBase = fireRateBase;
		_fireRateMultiplier = fireRateMultiplier;
		_bulletSpeedBase = bulletSpeedBase;
		_bulletSpeedMultiplier = bulletSpeedMultiplier;
		_accuracyMultiplier = accuracyMultiplier;
		_magazineSizeMultiplier = magazineSizeMultiplier;
		_reloadSpeedMultiplier = reloadSpeedMultiplier;
		_scopeSightRateMultiplier = scopeSightRateMultiplier;
		_bonuceTimesBase = bonuceTimesBase;
		_bonuceTimesMultiplier = bonuceTimesMultiplier;
		_burstBulletCountMultiplier = burstBulletCountMultiplier;
	}

	public override void Start() {
		base.Start();
		modulePart = EWeaponModulePart.Body;
		UpdateProperty();
	}

}