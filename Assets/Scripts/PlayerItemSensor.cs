using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSensor : MonoBehaviour {

	public PlayerController player;

	private float minDistance = float.MaxValue;
	private GameObject minDistanceObj;

	private void OnTriggerStay(Collider other) {
		if (player.isShooting)
			HUDController.DisableFloatingPanel();

		if (other.tag == "Weapon") {
			var weapon = other.GetComponent<Weapon>();
			if (!weapon.isEquipped) {
				Debug.DrawLine(transform.position, weapon.transform.position);
				var dis = Vector3.Distance(transform.position, weapon.transform.position);
				if (dis < minDistance) {
					minDistance = dis;
					minDistanceObj = weapon.gameObject;
					if (!player.isShooting)
						HUDController.EnableFloatingPanelAt(weapon.model.transform);
					player.weaponToEquip = weapon;
				}
			}

		}
		if (other.tag == "WeaponModule") {
			var module = other.GetComponent<WeaponModule>();
			if (!module.isEquipped) {
				Debug.DrawLine(transform.position, module.transform.position);
				var dis = Vector3.Distance(transform.position, module.transform.position);
				if (dis < minDistance) {
					minDistance = dis;
					minDistanceObj = module.gameObject;
					if (!player.isShooting)
						HUDController.EnableFloatingPanelAt(module.model.transform);
					player.moduleToEquip = module;
				}
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Weapon") {
			var weapon = other.GetComponent<Weapon>();
			if (weapon == player.weaponToEquip) {
				HUDController.DisableFloatingPanel();
				player.weaponToEquip = null;
			}
		}

		if (other.tag == "WeaponModule") {
			var module = other.GetComponent<WeaponModule>();
			if (module == player.moduleToEquip) {
				HUDController.DisableFloatingPanel();
				player.moduleToEquip = null;
			}
		}
	}

	private void Update() {
		if (minDistanceObj != null) {
			minDistance = Vector3.Distance(transform.position, minDistanceObj.transform.position);
		}
	}

	/*
	private void OnTriggerEnter(Collider other) {

		if (other.tag == "Weapon") {
			var weapon = other.GetComponent<Weapon>();
			if (!weapon.isEquipped) {
				Debug.Log("Press E to equip " + weapon.name);
				HUDController.EnableFloatingPanelAt(weapon.model.transform);
				player.weaponToEquip = weapon;
			}
		}
		if (other.tag == "WeaponModule") {
			var module = other.GetComponent<WeaponModule>();
			if (!module.isEquipped) {
				Debug.Log("Press E to equip " + module.name);
				HUDController.EnableFloatingPanelAt(module.model.transform);
				player.moduleToEquip = module;
			}
		}

	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Weapon") {
			var weapon = other.GetComponent<Weapon>();
			if (weapon == player.weaponToEquip) {
				HUDController.DisableFloatingPanel();
				player.weaponToEquip = null;
			}
		}

		if (other.tag == "WeaponModule") {
			var module = other.GetComponent<WeaponModule>();
			if (module == player.moduleToEquip) {
				HUDController.DisableFloatingPanel();
				player.moduleToEquip = null;
			}
		}
	}
	*/
}