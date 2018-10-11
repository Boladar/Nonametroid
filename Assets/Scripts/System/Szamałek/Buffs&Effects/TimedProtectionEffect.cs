using UnityEngine;
using System.Collections;
using Effects.EffectActions;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Timed Protections/Effect")]
	public class TimedProtectionEffect : TimedProtection
	{
		public BasicEffect effect;

		public override bool CheckTarget (BasicEffect basicEffect)
		{
			if (basicEffect == effect)
				return true;
			else
				return false;
		}
	}
}
