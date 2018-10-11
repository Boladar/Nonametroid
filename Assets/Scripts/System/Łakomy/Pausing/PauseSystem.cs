using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour {

	public bool canGameStop;
	bool getMessage;

	public void Update () {
		if (Input.GetButtonDown ("Pause") || getMessage == true) {
			if (!GameManager.INSTANCE.isOnPause) {
				GameManager.INSTANCE.StopGame (true);
				this.SendMessage ("MenuVisible");
				getMessage = false;
				StopGame ();
			} else {
				GameManager.INSTANCE.StopGame (false);
				this.SendMessage ("MenuVisible");
				getMessage = false;
				ResumeGame ();
			}

			Debug.Log ("Is game paused: " + GameManager.INSTANCE.isOnPause);
		}
	}

	void StopGame () {
		if (canGameStop) {
			Time.timeScale = .0f;
		}
	}

	void ResumeGame () {
		if (canGameStop) {
			Time.timeScale = 1.0f;
		}
	}

	public void GetMessage () {
		getMessage = true;
	}
}
