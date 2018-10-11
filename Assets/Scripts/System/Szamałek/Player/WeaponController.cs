using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public Transform WeaponEnd;
	public GameObject WeaponPreafab;
	public GameObject ProjectilePrefab;

	public string EnemyTag;

	private float WeaponTimeStamp = 0f;

	public Weapon WeaponData;

	public int AmmoQuantity = 0;
	public int MaxAmmoQuantity;

	// Use this for initialization
	void Start () {
		MaxAmmoQuantity = WeaponData.maxAmmoQuantity;
	}

	void Update()
	{
		if (Time.timeScale == 0)
			return;

		GetComponent<Animator>().SetFloat ("weaponDirection", Input.GetAxisRaw ("Vertical"));
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetAxisRaw ("Shoot") > 0 && WeaponTimeStamp <= Time.time && (WeaponData.ammoID == 0 || AmmoQuantity > 0)) {
			Shoot ();
			WeaponTimeStamp = Time.time +  WeaponData.shootCooldown;

			if(WeaponData.ammoID != 0)
				AmmoQuantity--;
		}
	}
		
	public void AddAmmo(Item weapon)
	{
		int quantity = weapon.quantity;
		int maxammount = MaxAmmoQuantity - AmmoQuantity;
		if (weapon.quantity + AmmoQuantity > MaxAmmoQuantity) {
			AmmoQuantity += maxammount;
			weapon.quantity -= maxammount;
		} else {
			AmmoQuantity += weapon.quantity;
			weapon.quantity -= quantity;
		}
	}

	public int ThrowAmmo()
	{
		int am = AmmoQuantity;
		AmmoQuantity = 0;
		return am;
	}

	void Shoot()
	{
		GameObject projectile = Instantiate (ProjectilePrefab, WeaponEnd.position, WeaponEnd.rotation);
		projectile.GetComponent<SpriteRenderer> ().sprite = WeaponData.projectileSprite;
		float scaleX = this.transform.parent.parent.transform.localScale.x;
		projectile.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		projectile.GetComponent<Rigidbody2D> ().AddRelativeForce (Vector2.right * 8000 * scaleX);
		projectile.GetComponent<Projectile> ().ProjectileDamage = WeaponData.weaponDamage;
		projectile.GetComponent<Projectile> ().EnemyTag = EnemyTag;
	}
}
