using UnityEngine;
using System.Collections;

public class CollectableGameObject : MonoBehaviour
{
	public Item item;

	[SerializeField]
	private Collectable itemData = null;
	[SerializeField]
	private int quantity = 0;

	void Awake()
	{
		item = new Item (itemData, quantity);
	}

	// Use this for initialization
	void Start ()
	{
		if (item != null)
			SetSprite ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (item.quantity == 0) {
			Destroy (this.gameObject);
		}
	}

	public void SetCollectableData(Item item)
	{
		this.item = item;
	}

	public void SetSprite()
	{
		GetComponent<SpriteRenderer>().sprite = item.collectable.sprite;
		Vector2 SpriteSize = this.gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		gameObject.GetComponent<CapsuleCollider2D> ().size = SpriteSize;
	}
}

