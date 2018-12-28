using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public static HUDController global;

	public Text magazineText;
	public FloatingPanelController floatingPanel;
	public GameObject gameOverPanel;
	private bool isFloatingPanelActive = false;

	public static void GameOver() {
		global.gameOverPanel.SetActive(true);
	}

	public void OnRestartButtonClick() {
		GameController.Restart();
	}

	public void UpdateMagazineText(int loaded, int size, int count) {
		magazineText.text = loaded + "/" + size * count;
	}

	public static void DisableFloatingPanel() {
		global.isFloatingPanelActive = false;
		global.floatingPanel.Close();
	}

	private Transform floatingAnchor;

	public static void EnableFloatingPanelAt(Transform target) {
		global.floatingAnchor = target;
		global.isFloatingPanelActive = true;
		global.floatingPanel.Open();
	}

	private void Update() {
		if (isFloatingPanelActive) {
			floatingPanel.transform.position = floatingAnchor.position + Vector3.up * 0.5f;
			floatingPanel.transform.LookAt(Camera.main.transform);
			floatingPanel.transform.Rotate(0, 180, 0);
		}
	}

	private void Awake() {
		if (HUDController.global == null) {
			HUDController.global = this;
		}

		if (floatingPanel == null)
			floatingPanel = GameObject.Find("Floating Panel").GetComponent<FloatingPanelController>();
		isFloatingPanelActive = false;
		gameOverPanel.SetActive(false);

		UpdateMagazineText(0, 0, 0);
	}
}