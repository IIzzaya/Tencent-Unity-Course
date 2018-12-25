﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可拾取的物品类
/// </summary>
[System.Serializable]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class FloatingItem : MonoBehaviour {

	[Header("FloatingItem Properties")]
	public bool isEquippable = false;
	public bool isEquipped = false;
	public Transform model;
	private Collider itemCollider;
	private CapsuleCollider capsuleCollider;
	protected Rigidbody rb;

	public virtual void Equip() {
		isEquipped = true;

		if (itemCollider == null)
			itemCollider = GetComponent<Collider>();
		itemCollider.enabled = false;
	}

	public virtual void DropDown() {
		isEquipped = false;
		itemCollider.enabled = true;
	}

	private float floatingTimer = 0f;
	private float floatingFrequency = 3f; // 物体待拾取时的浮动速度
	private float floatingAmplitude = 0.3f; // 物体待拾取时的浮动幅度
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

	public virtual void Start() {
		capsuleCollider = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();

		capsuleCollider.radius = 1;
		rb.constraints = RigidbodyConstraints.FreezeRotation;
		gameObject.layer = LayerMask.NameToLayer("Item");

		gameObject.tag = "Resource";

		if (!isEquipped) {
			itemCollider = GetComponent<Collider>();
			DropDown();
		}
	}

	public void Update() {
		if (!isEquipped) {
			WaitToPickAnimation();
		}
	}
}