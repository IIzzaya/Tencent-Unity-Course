using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeaponModuleType {
	Rifle, // 自动步枪
	Sniper, // 狙击枪
	Shotgun, // 霰弹枪
	Smg, // 冲锋枪
	Laser, // 激光枪
	Missile, // 火箭筒
}

public enum EWeaponModulePart {
	Barrel,
	Body,
	Grip,
	Scope,
	Stock
}

/// <summary>
/// 武器模块基类
/// </summary>
[System.Serializable]
public class WeaponModule : FloatingItem {

	protected Weapon weapon;

	[Header("WeaponModule Properties")]
	public EWeaponModuleType moduleType;
	public EWeaponModulePart modulePart;

	[HideInInspector] public float _damageBase = 0f; // 伤害基数
	[HideInInspector] public float _damageMultiplier = 1f; // 伤害乘数
	[HideInInspector] public float _criticalRateBase = 0f; // 暴击率基数
	[HideInInspector] public float _criticalRateMultiplier = 1f; // 暴击率乘数
	[HideInInspector] public float _criticalDamageRateBase = 0f; // 暴击伤害倍率基数
	[HideInInspector] public float _criticalDamageRateMultiplier = 1f; // 暴击伤害倍率乘数
	[HideInInspector] public float _fireRateBase = 0f; // 射速基数
	[HideInInspector] public float _fireRateMultiplier = 1f; // 射速乘数
	[HideInInspector] public float _bulletSpeedBase = 0f; // 子弹飞行速度基数
	[HideInInspector] public float _bulletSpeedMultiplier = 1f; // 子弹飞行速度乘数
	[HideInInspector] public float _accuracyBase = 0f; // 射击精准度基数
	[HideInInspector] public float _accuracyMultiplier = 1f; // 射击精准度乘数
	[HideInInspector] public float _magazineSizeBase = 0f; // 弹夹容量基数
	[HideInInspector] public float _magazineSizeMultiplier = 1f; // 弹夹容量乘数
	[HideInInspector] public float _reloadSpeedBase = 0f; // 装弹速度基数
	[HideInInspector] public float _reloadSpeedMultiplier = 1f; // 装弹速度乘数
	[HideInInspector] public float _scopeSightRateBase = 0f; // 视野倍率基数
	[HideInInspector] public float _scopeSightRateMultiplier = 1f; // 视野倍率乘数
	[HideInInspector] public float _bonuceTimesBase = 0f; // 子弹反弹基数
	[HideInInspector] public float _bonuceTimesMultiplier = 1f; // 子弹反弹乘数
	[HideInInspector] public float _burstBulletCountBase = 0f; // 连发基数
	[HideInInspector] public float _burstBulletCountMultiplier = 1f; // 连发乘数

	public override void Equip() {
		base.Equip();
		rb.useGravity = false;
		model.gameObject.SetActive(false);
	}

	public override void DropDown() {
		base.DropDown();
		rb.useGravity = true;
		model.gameObject.SetActive(true);
	}

	public virtual void UpdateProperty() { }

	public override void Start() {
		base.Start();

		gameObject.tag = "WeaponModule";
		isEquippable = true;
		UpdateProperty();
	}

}