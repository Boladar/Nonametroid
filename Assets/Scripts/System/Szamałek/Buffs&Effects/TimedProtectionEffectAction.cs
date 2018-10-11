using UnityEngine;
using System.Collections;
using Effects.EffectActions;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Timed Protections/EffectAction")]
	public class TimedProtectionEffectAction : TimedProtection
	{
		public EffectAction effectAction;

		public override bool CheckTarget (BasicEffect basicEffect)
		{
			if (basicEffect.effectAction == effectAction)
				return true;
			else
				return false;
		}
	}
}


