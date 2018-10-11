using UnityEngine;
using System.Collections;
using Effects.EffectActions;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Protections/Effect")]
	public class ProtectionEffect : Protection
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




