using UnityEngine;
using System.Collections;
using Effects.EffectActions;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Protections/EffectAction")]
	public class ProtectionEffectAction : Protection
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



