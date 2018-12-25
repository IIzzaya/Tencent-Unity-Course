using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;
	public int bounceLeftTimes;
	[HideInInspector] public Rigidbody rb;

	private Vector3 direction;

	private void OnCollisionEnter(Collision other) {
		var contactInfo = other.contacts[0];

		// Debug.DrawLine(contactInfo.point, contactInfo.point + contactInfo.normal);
		var normal = contactInfo.normal;
		if (normal.y > 0.9) Destroy(gameObject);
		normal.y = 0;

		if (bounceLeftTimes > 0) {
			rb.velocity = Vector3.Reflect(direction, normal) * speed;
			direction = rb.velocity.normalized;
			bounceLeftTimes--;
		} else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
		direction = transform.forward;
	}
}