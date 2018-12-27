using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingPanelController : MonoBehaviour {
	public Text nameText;
	public Text detailText;
	public Animator animator;
	public float shiftSpeed = 5f;

	/// <summary>
	/// 计算字符串在指定text控件中的长度
	/// </summary>
	/// <param name="message"></param>
	/// <returns></returns>
	int CalculateLengthOfText(string message, Text tex) {
		int totalLength = 0;
		Font myFont = tex.font; //chatText is my Text component
		myFont.RequestCharactersInTexture(message, tex.fontSize, tex.fontStyle);
		CharacterInfo characterInfo = new CharacterInfo();

		char[] arr = message.ToCharArray();
		Debug.Log(myFont.fontSize);
		Debug.Log("CharacterInfo: " + characterInfo.advance);

		foreach (char c in arr) {
			myFont.GetCharacterInfo(c, out characterInfo, tex.fontSize);

			totalLength += characterInfo.advance;
		}

		return totalLength;
	}

	public void Open() {
		animator.SetBool("isOpen", true);
	}

	public void Close() {
		animator.SetBool("isOpen", false);
	}

	private void Start() {

		Debug.Log(nameText.text);
		var len = CalculateLengthOfText(nameText.text, nameText);
		Debug.Log(len);
	}
}