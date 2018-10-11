using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

public class GameManager : MonoBehaviour {

	public static GameManager INSTANCE;
	public bool isOnPause;
	public bool isMenuVisible;
	public Color highlightTextColor;
	public Creature target;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
		INSTANCE = this;
	}

	public void StopGame (bool isActive) {
		isOnPause = isActive;
		isMenuVisible = isActive;
	}
}
