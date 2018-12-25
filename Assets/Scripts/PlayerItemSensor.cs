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
		if (other.tag == "WeaponModule") {
			var module = other.GetComponent<WeaponModule>();
			if (!module.isEquipped) {
				Debug.Log("Press E to equip " + module.name);
				player.moduleToEquip = module;
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

		if (other.tag == "WeaponModule") {
			var module = other.GetComponent<WeaponModule>();
			if (module == player.moduleToEquip) {
				player.moduleToEquip = null;
			}
		}
	}

}