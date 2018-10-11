using UnityEngine;
using System.Collections;

public class Item
{
	public Collectable collectable;
	public int quantity;

	public Item (Collectable collectable, int quantity)
	{
		this.collectable = collectable;
		this.quantity = quantity;
	}

	public void OnCollect(Controller target)
	{
		collectable.OnCollect (this,target);
	}
}

