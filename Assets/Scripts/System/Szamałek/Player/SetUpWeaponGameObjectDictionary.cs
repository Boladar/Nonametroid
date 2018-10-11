using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpWeaponGameObjectDictionary : MonoBehaviour {

	private Dictionary<Weapon,GameObject> WeaponGameObjectDictionary;

	public List<Weapon> WeaponData = new List<Weapon>();
	public List<GameObject> WeaponGameObjects = new List<GameObject>();

	// Use this for initialization
	void Awake () {
		WeaponGameObjectDictionary = GameObject.Find ("Player").GetComponent<Controller> ().WeaponGameObjects;

		for (int i = 0; i < WeaponData.Count; i++) {
			WeaponGameObjectDictionary.Add (WeaponData [i], WeaponGameObjects [i]);
		}
	}


}
