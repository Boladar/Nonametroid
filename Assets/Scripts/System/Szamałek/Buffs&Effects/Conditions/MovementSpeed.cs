using System;
using Effects.EffectActions;
using UnityEngine;

namespace Effects.Conditions
{
	[CreateAssetMenu (menuName = "Effects/Conditions/Condition Actions/Movement Speed")]
	public class MovementSpeed : Stat
	{
		public override bool isConditionFulfilled (Creature target)
		{
			return Compare (target.movementSpeed, comparison);
		}
	}
}

