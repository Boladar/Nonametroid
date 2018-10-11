using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIFlipping : MonoBehaviour {

	float ParentScale;
	Vector3 scale = Vector3.one;

	void Update () {
		ParentScale = transform.parent.parent.localScale.x;
		if (ParentScale == 1f)
			this.transform.localScale = scale;
		else
			this.transform.localScale = -scale;
	}
}
