using System;
using Effects.EffectActions;
using UnityEngine;

namespace Effects.Conditions
{
	[CreateAssetMenu (menuName = "Effects/Conditions/Condition Actions/HP")]
	public class HP : Stat
	{
		public override bool isConditionFulfilled (Creature target)
		{
			return Compare (target.hp, comparison);
		}
	}
}
