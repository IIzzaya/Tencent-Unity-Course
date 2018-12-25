using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪头模块
/// [控制]
/// 射出子弹的种类(module Part)：子弹/激光/导弹
/// [影响]
/// 
/// </summary>
public class BarrelModule : WeaponModule {

	[Header("BarrelModule Properties")]
	public float damageBase = 0f; // 伤害基数
	public float damageMultiplier = 1f; // 伤害乘数
	public float criticalRateMultiplier = 1f; // 暴击率乘数
	public float criticalRateBase = 0f; // 暴击率基数
	public float criticalDamageRateMultiplier = 1f; // 暴击伤害倍率乘数
	public float fireRateMultiplier = 1f; // 射速乘数
	public float bulletSpeedMultiplier = 1f; // 子弹飞行速度乘数
	public float accuracyMultiplier = 1f; // 射击精准度乘数
	public float bonuceTimesMultiplier = 1f; // 子弹反弹乘数
	public float burstBulletCountBase = 0f; // 连发基数
	public float burstBulletCountMultiplier = 1f; // 连发乘数

	// [Header("* Rifle Only 全自动步枪参数")]
	// [Header("* BurstRifle Only 半自动步枪参数")]
	// [Header("* Snipper Only 狙击枪参数")]
	// [Header("* Shotgun Only 霰弹枪参数")]
	// [Header("* Smg Only 冲锋枪参数")]
	// [Header("* Laser Only 激光枪参数")]
	// [Header("* Missile Only 火箭筒参数")]

	public bool isBurstType;

	public override void UpdateProperty() {
		_damageBase = damageBase;
		_damageMultiplier = damageMultiplier;
		_criticalRateBase = criticalRateBase;
		_criticalRateMultiplier = criticalRateMultiplier;
		_fireRateMultiplier = fireRateMultiplier;
		_bulletSpeedMultiplier = bulletSpeedMultiplier;
		_accuracyMultiplier = accuracyMultiplier;
		_bonuceTimesMultiplier = bonuceTimesMultiplier;
		_burstBulletCountBase = burstBulletCountBase;
		_burstBulletCountMultiplier = burstBulletCountMultiplier;
	}

	public override void Start() {
		base.Start();
		modulePart = EWeaponModulePart.Barrel;
	}

}