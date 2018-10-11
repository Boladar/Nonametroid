using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCameraRounding : MonoBehaviour {

	public Transform objToFollow;
	public Transform camera;

	public float horizontalShift = 74f;
	public float verticalShift = 54f;

	private float currentHorizontalShift = 0;
	private float currentVerticalShift = 0;

	private float endTimeHorizontal;
	private float endTimeVertical;

	void Update () {
		
		Vector3 NextPosition;
		NextPosition = new Vector3 (objToFollow.position.x + currentHorizontalShift, objToFollow.position.y + 30f + currentVerticalShift, -800);
	
		//Przesunięcie pierwsze, podczas zaczęcia ruchu

		if (Input.GetAxisRaw ("Horizontal") == 1) {
			currentHorizontalShift = Mathf.Lerp (currentHorizontalShift, horizontalShift, 3f * Time.deltaTime);
			endTimeHorizontal = Time.time;
		} else if (Input.GetAxisRaw ("Horizontal") == -1) {
			currentHorizontalShift = Mathf.Lerp (currentHorizontalShift, -horizontalShift, 3f * Time.deltaTime);
			endTimeHorizontal = Time.time;
		}

		if (Input.GetAxisRaw ("Vertical") == 1) {
			currentVerticalShift = Mathf.Lerp (currentVerticalShift, verticalShift, 4f * Time.deltaTime);
			endTimeVertical = Time.time;
		} else if (Input.GetAxisRaw ("Vertical") == -1) {
			currentVerticalShift = Mathf.Lerp (currentVerticalShift, -verticalShift, 4f * Time.deltaTime);
			endTimeVertical = Time.time;
		}

		//Przesunięcie drugie, powrót po czasie

		if (Time.time >= endTimeHorizontal + 0.75f) {
			currentHorizontalShift = Mathf.Lerp (currentHorizontalShift, 0, 2f * Time.deltaTime);
		}

		if (Time.time >= endTimeVertical + 0.75f) {
			currentVerticalShift = Mathf.Lerp (currentVerticalShift, 0, 3f * Time.deltaTime);
		}

		NextPosition = new Vector3 (Mathf.Floor (NextPosition.x), Mathf.Floor (NextPosition.y), NextPosition.z);

		camera.position = NextPosition;


	}


}
