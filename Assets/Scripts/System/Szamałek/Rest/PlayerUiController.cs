using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUiController : MonoBehaviour {

	public Image HealthBar;
	public Image AmmoBar;
	private WeaponController PlayerWeapon;

	// Update is called once per frame
	void Update () {
		WeaponController PlayerWeapon = Controller.PLAYER.GetComponent<Controller> ().CurrentWeaponGameObject.GetComponent<WeaponController> ();

		HealthBar.fillAmount = Controller.PLAYER.GetComponent<Controller> ().hp / 100;

		if (PlayerWeapon.MaxAmmoQuantity != 0) {
			AmmoBar.fillAmount = ((float)PlayerWeapon.AmmoQuantity / (float)PlayerWeapon.MaxAmmoQuantity);
		}
		else
			AmmoBar.fillAmount = 1f;
	}
}
