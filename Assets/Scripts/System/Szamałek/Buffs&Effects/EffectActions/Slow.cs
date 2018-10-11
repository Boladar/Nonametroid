using UnityEngine;
using System.Collections;

namespace Effects.EffectActions
{
	[CreateAssetMenu (menuName = "Effects/Effect Actions/Slow")]
	public class Slow : EffectAction
	{
		public override float ApplyEffect (Creature target, float value)
		{
			float multiplayer;
			float movementSpeedBefore = target.movementSpeed;

			if ((100 - value) == 0)
				multiplayer = 0;
			else
				multiplayer = (100 - value) / 100;
			
			float movementSpeedAfter = target.movementSpeed *= multiplayer;
			return movementSpeedBefore - movementSpeedAfter;
		}

		public override void EndEffect (Creature target, float value)
		{
			target.movementSpeed += value;
		}
	}
}

