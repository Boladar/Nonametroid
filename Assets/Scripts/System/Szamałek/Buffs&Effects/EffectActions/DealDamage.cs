using System;
using UnityEngine;

namespace Effects.EffectActions
{
	[CreateAssetMenu (menuName = "Effects/Effect Actions/Deal Damage")]
	public class DealDamage : EffectAction
	{
		public override float ApplyEffect (Creature target, float value)
		{
			target.DoDamage (value);
			return value;
		}

		public override void EndEffect (Creature target, float value)
		{
			
		}
	}
}

