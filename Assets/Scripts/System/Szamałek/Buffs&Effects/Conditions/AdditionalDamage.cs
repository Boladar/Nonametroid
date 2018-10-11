using System;
using Effects.EffectActions;
using UnityEngine;

namespace Effects.Conditions
{
	[CreateAssetMenu (menuName = "Effects/Conditions/Condition Actions/Additional Damage")]
	public class AdditionalDamage : Stat
	{
		public override bool isConditionFulfilled (Creature target)
		{
			return Compare (target.additionalDamage, comparison);
		}
	}
}
