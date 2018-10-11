using UnityEngine;
using System.Collections;

public abstract class Collectable : ScriptableObject
{
	public string itemName;
	public Sprite sprite;
	public Sprite menuItemSprite;
	public string description;

	public virtual void OnCollect (Item item, Controller target)
	{
		AddItemToInventory (item, target);
		Debug.Log ("inventory.count : " + target.Inventory.Count);
		Debug.Log ("item added to inventory: " + this.itemName +", total number: " +
			target.Inventory[ target.GetCollectableIndex(this)].quantity);
	}

	public virtual void OnUse(Item item,Controller target)
	{
		Debug.Log ("ON_USE: " + this.itemName);
	}

	public void AddItemToInventory(Item item, Controller target)
	{
		int index = target.GetCollectableIndex (this);
		if (index != -1)
			target.Inventory [index].quantity += item.quantity;
		else
			target.Inventory.Add(new Item(this,item.quantity));

		item.quantity = 0;
	}
}

