using System;
using Effects.EffectActions;
using UnityEngine;

namespace Effects.Conditions
{
	[CreateAssetMenu (menuName = "Effects/Conditions/Condition Actions/JumpForce")]
	public class JumpForce : Stat
	{
		public override bool isConditionFulfilled (Creature target)
		{
			return Compare (target.jumpForce, comparison);
		}
	}
}
