using System;
using UnityEngine;

namespace Effects.EffectActions
{
	[CreateAssetMenu (menuName = "Effects/Effect Actions/Additional Damage")]
	public class AdditionalDamage : EffectAction
	{
		public override float ApplyEffect (Creature target, float value)
		{
			target.additionalDamage += value;
			return value;
		}

		public override void EndEffect (Creature target, float value)
		{
			target.additionalDamage -= value;
		}
	}
}


