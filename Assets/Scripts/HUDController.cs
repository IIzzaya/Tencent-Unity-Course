using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public static HUDController global;

	public Text magazineText;

	public void UpdateMagazineText(int loaded, int size, int count) {
		magazineText.text = loaded + "/" + size * count;
	}

	private void Awake() {
		if (HUDController.global == null) {
			HUDController.global = this;
		}

		UpdateMagazineText(0, 0, 0);
	}
}