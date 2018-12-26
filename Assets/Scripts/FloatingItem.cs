using System.Collections;
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
	[HideInInspector] public Rigidbody rb;

	public virtual void Equip() {
		if (rb == null) rb = GetComponent<Rigidbody>();
		isEquipped = true;
		rb.useGravity = false;
		if (itemCollider == null)
			itemCollider = GetComponent<Collider>();
		itemCollider.enabled = false;
	}

	public virtual void DropDown(Vector3 position) {
		transform.SetParent(null);
		transform.position = position;
		isEquipped = false;
		rb.useGravity = true;
		if (itemCollider == null)
			itemCollider = GetComponent<Collider>();
		itemCollider.enabled = true;
		rb.velocity = Vector3.up * 5f;
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
			DropDown(transform.position);
		}
	}

	public void Update() {
		if (!isEquipped) {
			WaitToPickAnimation();
		}
	}
}