using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallax : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing = 1f;

	[SerializeField]
	private new Transform camera;
	private Vector3 previousCameraPosition;

	void Awake () {
		camera = GetComponent <Transform> ();	
	}

	void Start () {
		previousCameraPosition = camera.position;

		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z * -1;
		}
	}

	void Update () {
		for (int i = 0; i < backgrounds.Length; i++) {
			float Parallax = (previousCameraPosition.x - camera.position.x) * parallaxScales [i];

			float BackgroundTargetPosX = backgrounds [i].position.x + Parallax;
			Vector3 BackgroundTargetPos = new Vector3 (BackgroundTargetPosX, backgrounds [i].position.y, backgrounds [i].position.z);

			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, BackgroundTargetPos, smoothing * Time.deltaTime);
		}

		previousCameraPosition = camera.position;
	}
}
