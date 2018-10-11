using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FontFix : MonoBehaviour {

	public Font[] Fonts;

	void Start () {
		for (int i = 0; i < Fonts.Length; i++) {
			Fonts [i].material.mainTexture.filterMode = FilterMode.Point;
		}
	}
}
