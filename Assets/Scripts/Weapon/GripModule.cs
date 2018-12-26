using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 握柄模块
/// </summary>
public class GripModule : WeaponModule {

	[Header("GripModule Properties")]
	public float damageMultiplier = 1f; // 伤害乘数
	public float fireRateMultiplier = 1f; // 射速乘数
	public float accuracyMultiplier = 1f; // 射击精准度乘数
	public float magazineSizeBase = 0f; // 弹夹容量基数
	public float magazineSizeMultiplier = 1f; // 弹夹容量乘数
	public float reloadSpeedBase = 0f; // 装弹速度基数
	public float reloadSpeedMultiplier = 1f; // 装弹速度乘数
	public float bonuceTimesMultiplier = 1f; // 子弹反弹乘数
	public float burstBulletCountMultiplier = 1f; // 连发乘数

	public override void UpdateProperty() {
		_damageMultiplier = damageMultiplier;
		_fireRateMultiplier = fireRateMultiplier;
		_accuracyMultiplier = accuracyMultiplier;
		_magazineSizeBase = magazineSizeBase;
		_magazineSizeMultiplier = magazineSizeMultiplier;
		_reloadSpeedBase = reloadSpeedBase;
		_reloadSpeedMultiplier = reloadSpeedMultiplier;
		_bonuceTimesMultiplier = bonuceTimesMultiplier;
		_burstBulletCountMultiplier = burstBulletCountMultiplier;
	}

	public override void Start() {
		base.Start();
		modulePart = EWeaponModulePart.Grip;
		UpdateProperty();
	}

}