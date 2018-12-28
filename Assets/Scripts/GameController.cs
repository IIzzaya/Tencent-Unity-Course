using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController global;

	public static void Restart() {
		SceneManager.LoadScene("Game");
	}

	private void Awake() {
		if (GameController.global == null) {
			GameController.global = this;
		} else if (GameController.global != this) {
			Destroy(GameController.global);
			GameController.global = this;
		}

		DontDestroyOnLoad(gameObject);
	}
}