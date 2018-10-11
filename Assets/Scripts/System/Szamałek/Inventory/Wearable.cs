using UnityEngine;
using System.Collections;
using Effects;

[CreateAssetMenu (menuName="Collectable/Wearable")]
public class Wearable : Collectable
{	
	public EffectChain WearableEffect;

	public override void OnUse (Item item,Controller target)
	{
		OnEquip (item,target);
	}

	public void OnEquip(Item item,Controller target)
	{
		target.RemoveItemFromInventory (item);
		EffectController.instance.RequestEffectChain (target, WearableEffect);
	}

	public void OnUnequip(Item item,Controller target)
	{
		
	}
}

