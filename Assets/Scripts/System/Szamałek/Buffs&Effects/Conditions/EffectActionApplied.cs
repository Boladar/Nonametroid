using System;
using Effects.EffectActions;
using UnityEngine;

namespace Effects.Conditions
{
	[CreateAssetMenu (menuName = "Effects/Conditions/Condition Actions/EffectAction Applied")]
	public class EffectActionApplied : ConditionAction
	{
		public EffectAction actionToCheck;

		public override bool isConditionFulfilled (Creature target)
		{
			foreach (BasicEffectContainer container in target.activeBasicEffects) {
				if (container.basicEffect.effectAction == actionToCheck)
					return true;
			}

			foreach (TimedEffectContainer container in target.activeTimedEffects) {
				if (container.timedEffect.effectAction == actionToCheck)
					return  true;
			}

			return false;
		}
	}
}

