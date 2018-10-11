using UnityEngine;
using System.Collections;
using Effects;

[CreateAssetMenu (menuName="Collectable/Consumable")]
public class Consumable : Collectable
{
	public EffectChain ConsumableEffect;

	public override void OnUse (Item item,Controller target)
	{
		EffectController.instance.RequestEffectChain (target, ConsumableEffect);
	}
}

