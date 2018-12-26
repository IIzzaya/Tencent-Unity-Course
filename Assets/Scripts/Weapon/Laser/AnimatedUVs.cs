using System.Collections;
using UnityEngine;

public class AnimatedUVs : MonoBehaviour {

	public int materialIndex = 0;
	public float speed = 1f;
	[HideInInspector] public float speedMultiplier = 1f;
	public Vector2 uvAnimationDirection = new Vector2(1.0f, 0.0f);
	public string textureName = "_MainTex";

	Vector2 uvOffset = Vector2.zero;

	void LateUpdate() {
		uvOffset += (uvAnimationDirection.normalized * speed * speedMultiplier * Time.deltaTime);
		if (GetComponent<Renderer>().enabled) {
			GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, uvOffset);
		}
	}
}