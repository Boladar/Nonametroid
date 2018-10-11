using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName="Collectable/Weapon")]
public class Weapon : Collectable, InstantPicker
{
	public int ammoID;
	public int maxAmmoQuantity;
	public int weaponDamage;
	public float shootCooldown;
	public Sprite projectileSprite;

	public override void OnCollect (Item item,Controller target)
	{
		if (target.Weapons [1] != null) {
			if (CheckIfTargetAlreadyCarriesThisWeapon (target))
				target.GetWeaponGameObject (this).GetComponent<WeaponController> ().AddAmmo (item);
			else {
				target.ThrowWeapon (target.Weapons[1]);
				EquipWeapon (target,item);
			}
		} else {
			EquipWeapon(target,item);
		}
	}

	public override void OnUse (Item item,Controller target)
	{
		base.OnUse (item,target);
	}

	private void EquipWeapon(Controller target,Item item)
	{
		target.Weapons [1] = this;
		target.ChangeWeapon();
		target.GetWeaponGameObject (this).AddAmmo (item);
	}

	private bool CheckIfTargetAlreadyCarriesThisWeapon(Controller target)
	{
		if (this.itemName == target.Weapons [1].itemName)
			return true;
		return false;
	}

	void InstantPicker.Pick(Item item, Controller target)
	{
		if (target.Weapons [1] != null) {
			if (CheckIfTargetAlreadyCarriesThisWeapon (target))
				target.GetWeaponGameObject (this).GetComponent<WeaponController> ().AddAmmo (item);
		}
	}
}