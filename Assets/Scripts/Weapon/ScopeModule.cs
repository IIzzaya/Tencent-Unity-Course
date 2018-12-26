using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瞄准镜模块
/// </summary>
public class ScopeModule : WeaponModule {

	[Header("ScopeModule Properties")]
	public float damageMultiplier = 1f; // 伤害乘数
	public float criticalRateMultiplier = 1f; // 暴击率乘数
	public float criticalDamageRateBase = 0f; // 暴击伤害倍率基数
	public float criticalDamageRateMultiplier = 1f; // 暴击伤害倍率乘数
	public float accuracyMultiplier = 1f; // 射击精准度乘数
	public float scopeSightRateBase = 0f; // 视野倍率基数
	public float scopeSightRateMultiplier = 1f; // 视野倍率乘数

	public override void UpdateProperty() {
		_damageMultiplier = damageMultiplier;
		_criticalRateMultiplier = criticalRateMultiplier;
		_criticalDamageRateBase = criticalDamageRateBase;
		_criticalDamageRateMultiplier = criticalDamageRateMultiplier;
		_accuracyMultiplier = accuracyMultiplier;
		_scopeSightRateBase = scopeSightRateBase;
		_scopeSightRateMultiplier = scopeSightRateMultiplier;
	}

	public override void Start() {
		base.Start();
		modulePart = EWeaponModulePart.Scope;
		UpdateProperty();
	}

}