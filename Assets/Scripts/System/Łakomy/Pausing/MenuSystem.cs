using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject[] allMenuChoices;
	public GameObject inventorySubMenu;
	[SerializeField]
	RectTransform cursor;
	[SerializeField]
	int currentMenuChoice = 0;
	int currentSubMenuInUsing = 0;

	void MenuVisible () {
		pauseMenu.SetActive (GameManager.INSTANCE.isMenuVisible);
	}

	void Update () {
		for (int i = 0; i < allMenuChoices.Length; i++) {
			if (i == currentMenuChoice)
				allMenuChoices [currentMenuChoice].GetComponent<Text> ().color = GameManager.INSTANCE.highlightTextColor;
			else 
				allMenuChoices [i].GetComponent<Text> ().color = Color.white;
		}

		Vector3 currentMenuChoicePosition = allMenuChoices [currentMenuChoice].GetComponent<RectTransform> ().position;

		currentMenuChoicePosition = new Vector3 (currentMenuChoicePosition.x- 3, currentMenuChoicePosition.y - 3, 0);
		cursor.position = Vector3.Lerp (cursor.position, currentMenuChoicePosition, 0.6f);

		if (Input.GetButtonDown ("Vertical"))
			currentMenuChoice = currentMenuChoice - Mathf.RoundToInt(Input.GetAxisRaw ("Vertical"));

		if (currentMenuChoice < 0)
			currentMenuChoice = allMenuChoices.Length - 1;
		else if (currentMenuChoice > allMenuChoices.Length - 1)
			currentMenuChoice = 0;

		MenuChoices ();
	}
		
	void MenuChoices () {
		//Debug.Log (currentMenuChoice);
		if (currentMenuChoice == 0) {
			if (Input.GetButtonDown ("Submit")) {
				this.SendMessage ("GetMessage");
			}
		}
		if (currentMenuChoice == 1) {
			inventorySubMenu.SetActive (true);
		} else {
			inventorySubMenu.SetActive (false);
		}
	}
}
