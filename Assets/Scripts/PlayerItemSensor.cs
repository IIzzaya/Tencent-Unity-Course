using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSensor : MonoBehaviour {

	public PlayerController player;

	private void OnTriggerEnter(Collider other) {

		if (other.tag == "Weapon") {
			var weapon = other.GetComponent<Weapon>();
			if (!weapon.isEquipped) {
				Debug.Log("Press E to equip " + weapon.name);
				player.weaponToEquip = weapon;
			}
		}

	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Weapon") {
			var weapon = other.GetComponent<Weapon>();
			if (weapon == player.weaponToEquip) {
				player.weaponToEquip = null;
			}
		}
	}

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}
}