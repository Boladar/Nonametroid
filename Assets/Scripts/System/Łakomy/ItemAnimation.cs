using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour {

	void Update () 
	{        
		transform.Rotate (Vector3.up * Time.deltaTime * 120, Space.World);
	}

}
