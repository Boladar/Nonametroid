using System;
using UnityEngine;

namespace Effects.EffectActions
{
	[CreateAssetMenu (menuName = "Effects/Effect Actions/Heal")]	
	public class Heal : EffectAction
	{
		public override float ApplyEffect (Creature target, float value)
		{
			target.hp += value;
			return value;
		}

		public override void EndEffect (Creature target, float value)
		{
			
		}
	}
}

