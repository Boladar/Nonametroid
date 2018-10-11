using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : Creature {

	//statoc
	public static GameObject PLAYER;
	public static Controller CONTROLLER;

	//items
	public Dictionary<Weapon,GameObject> WeaponGameObjects = new Dictionary<Weapon, GameObject>();
	public List<Item> Inventory = new List<Item>();
	public Weapon[] Weapons = new Weapon[2];
	public Wearable CurrentlyWeared;
	public Weapon DefaultWeaponData;
	private int CurrentWeaponIndex;
	public GameObject CurrentWeaponGameObject;

	//bools
	bool isFacingRight = true;
	bool isGrounded = true;
	public bool CanPickUpWeapon = false;
	public bool CanPickUpMoreAmmo = false;

	//layermasks
	public LayerMask GroundLayer;
	public LayerMask ignoreMask;
	public LayerMask WeaponMask;

	//prefabs
	public GameObject ProjectilePrefab;
	public GameObject ItemPrefab;

	//various
	float move;
	float WeaponChangeTimeStamp = 0;
	Animator PlayerAnimator;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	int lmask;	

	void Awake()
	{
		PLAYER = this.gameObject;
		CONTROLLER = this;
	}

	protected override void Start ()
	{
		base.Start ();
		int lmask = ~ignoreMask.value;
		PlayerAnimator = GetComponent<Animator>();
		Weapons [0] = DefaultWeaponData;
		CurrentWeaponGameObject = WeaponGameObjects [DefaultWeaponData];
	}

	// Update is called once per frame
	void FixedUpdate () {

		move = Input.GetAxisRaw ("Horizontal");

		AnimatePlayer ();
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, GroundLayer);

		if(isGrounded && Input.GetAxisRaw("Jump")> 0)
		{
			isGrounded = !isGrounded;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce)); 
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (movementSpeed * move, GetComponent<Rigidbody2D> ().velocity.y);

		if (move < 0 && isFacingRight || move > 0 && !isFacingRight)
			Flip ();
		
		//change weapon
		if (Input.GetAxis ("ChangeWeapon") != 0 && WeaponChangeTimeStamp <= Time.time && Weapons[1] != null) {
			ChangeWeapon ();
		}

		Collider2D result;
		result = Physics2D.OverlapCircle(this.transform.position, 5f, WeaponMask);
		Debug.DrawLine(this.transform.position, new Vector3(this.transform.position.x + 10f,this.transform.position.y));
		if (result != null && Input.GetButtonDown ("Action"))
			PickItem (result.gameObject.GetComponent<CollectableGameObject> ().item);

		if (result != null) {
			InstantPicker picker = result.GetComponent<CollectableGameObject>().item.collectable as InstantPicker;
			if (picker != null)
				picker.Pick (result.GetComponent<CollectableGameObject> ().item, this);
		}
	}
		
	void Update()
	{
		//check for collisions on player movement for next frame
		Vector2 vel = GetComponent<Rigidbody2D>().velocity;
		RaycastHit2D hit = Physics2D.Raycast (this.transform.position, vel.normalized, 1f, lmask);

		Debug.DrawLine(this.transform.position,this.transform.position + new Vector3(vel.normalized.x * 1f, vel.normalized.y * 1f));

		if (hit.collider != null) 
		{
			Debug.Log (hit.collider.name);
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero; 
			move = 0;
		}
	}
		
	#region item

	public void RemoveItemFromInventory(Item item)
	{
		Inventory.Remove (item);
	}

	public void EquipWearable(Wearable wear)
	{
		CurrentlyWeared = wear;
	}

	public void UseItem(Item item)
	{
		Debug.Log ("USE: " + item.collectable.itemName);
		item.collectable.OnUse(item,this);
	}

	public void PickItem(Item item)
	{
		item.OnCollect (this);
	}

	public int GetCollectableIndex(Collectable collectable)
	{
		for (int i = 0; i < Inventory.Count; i++) {
			if (Inventory [i].collectable.itemName == collectable.itemName) {
				return i;
			}
				
		}
		return -1;
	}


	#endregion

	#region Weapon

	public void ThrowWeapon(Weapon weapon){
		GameObject gm =  Instantiate (ItemPrefab, this.transform.position, this.transform.rotation);
		gm.GetComponent<CollectableGameObject> ().SetCollectableData (new Item (weapon, GetWeaponGameObject (weapon).ThrowAmmo ()));
		gm.GetComponent<CollectableGameObject> ().SetSprite ();
		Debug.Log ("gm" + gm.name);
	}

	public void ChangeWeapon()
	{
		WeaponGameObjects [Weapons [CurrentWeaponIndex]].SetActive(false);
		CurrentWeaponIndex = (CurrentWeaponIndex + 1) % 2;
		WeaponChangeTimeStamp = Time.time + 0.5f;
		WeaponGameObjects [Weapons [CurrentWeaponIndex]].SetActive(true);
		CurrentWeaponGameObject = WeaponGameObjects [Weapons [CurrentWeaponIndex]];
	}

	public WeaponController GetWeaponGameObject(Weapon weapon)
	{
		return WeaponGameObjects [weapon].GetComponent<WeaponController> ();
	}
	#endregion

	void Flip()
	{
		isFacingRight = !isFacingRight;
		scale = this.transform.localScale;
		scale.x *= -1;
		this.transform.localScale = scale;
	}
		
	void AnimatePlayer()
	{
		//running animation
		if (move == 0 && isGrounded )
			PlayerAnimator.SetBool ("isRunning", false);
		else if (move != 0 && isGrounded)
			PlayerAnimator.SetBool("isRunning",true);
		//jumping animation
		if (!isGrounded)
			PlayerAnimator.SetBool ("isJumping", true);
		else
			PlayerAnimator.SetBool("isJumping",false);

		//crouching
		if (Input.GetAxisRaw ("Crouch") != 0) {
			PlayerAnimator.SetBool ("isCrouching", true);
			move = 0;
		}else
			PlayerAnimator.SetBool("isCrouching", false);

	}

	protected override void Die ()
	{
		throw new System.NotImplementedException ();
	}

}
