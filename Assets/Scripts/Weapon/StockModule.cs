using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪托模块
/// </summary>
public class StockModule : WeaponModule {

	[Header("StockModule Properties")]
	public float damageMultiplier = 1f; // 伤害乘数
	public float fireRateBase = 0f; // 射速基数
	public float fireRateMultiplier = 1f; // 射速乘数
	public float accuracyBase = 0f; // 射击精准度基数
	public float accuracyMultiplier = 1f; // 射击精准度乘数
	public float magazineSizeMultiplier = 1f; // 弹夹容量乘数
	public float reloadSpeedMultiplier = 1f; // 装弹速度乘数
	public float burstBulletCountMultiplier = 1f; // 连发乘数

	public override void UpdateProperty() {
		_damageMultiplier = damageMultiplier;
		_fireRateBase = fireRateBase;
		_fireRateMultiplier = fireRateMultiplier;
		_accuracyBase = accuracyBase;
		_accuracyMultiplier = accuracyMultiplier;
		_magazineSizeMultiplier = magazineSizeMultiplier;
		_reloadSpeedMultiplier = reloadSpeedMultiplier;
		_burstBulletCountMultiplier = burstBulletCountMultiplier;
	}

	public override void Start() {
		base.Start();
		modulePart = EWeaponModulePart.Stock;
	}

}