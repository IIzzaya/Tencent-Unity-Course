using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingPanelController : MonoBehaviour {
	public Text nameText;
	public Text detailText;
	public Animator animator;

	public void Open() {
		animator.SetBool("isOpen", true);
	}

	public void Close() {
		animator.SetBool("isOpen", false);
	}

	private void Start() { }
}