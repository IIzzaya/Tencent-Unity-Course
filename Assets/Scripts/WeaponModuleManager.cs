using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModuleManager : MonoBehaviour {

	public static WeaponModuleManager global;

	[Header("武器默认预设模块")]
	public GameObject barrelPrefab;
	public GameObject bodyPrefab;
	public GameObject gripPrefab;
	public GameObject scopePrefab;
	public GameObject stockPrefab;

	[Header("各种稀有度材质，由低到高")]
	public Material[] rarityMaterials;

	private void Awake() {
		if (WeaponModuleManager.global == null) {
			WeaponModuleManager.global = this;
		}
		Debug.Log("Weapon Module Manager Loaded");
	}

	public static GameObject InstantiateRandomModule(Vector3 position) {
		int part = Random.Range(0, 5);
		GameObject obj;
		switch (part) {
			case 0: // Barrel
				obj = Instantiate(global.barrelPrefab, position, Quaternion.identity);
				break;
			case 1: // Body
				obj = Instantiate(global.bodyPrefab, position, Quaternion.identity);
				break;
			case 2: // Grip
				obj = Instantiate(global.gripPrefab, position, Quaternion.identity);
				break;
			case 3: // Scope
				obj = Instantiate(global.scopePrefab, position, Quaternion.identity);
				break;
			case 4: // Stock
				obj = Instantiate(global.stockPrefab, position, Quaternion.identity);
				break;
			default:
				return null;
		}
		obj.GetComponent<WeaponModule>().GetRandomProperties();
		return obj;
	}

}