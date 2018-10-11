using UnityEngine;
using System.Collections;

namespace Effects.EffectActions 
{
	[CreateAssetMenu (menuName = "Effects/Effect Actions/Hasten")]
	public class Hasten : EffectAction
	{
		public override float ApplyEffect (Creature target, float value)
		{
			float multiplayer;
			float movementSpeedBefore = target.movementSpeed;

			if (value == 0)
				multiplayer = 1;
			else
		   		multiplayer = (100 + value) / 100;

			float movementSpeedAfter = target.movementSpeed *= multiplayer;
			return movementSpeedAfter - movementSpeedBefore;
		}

		public override void EndEffect (Creature target, float value)
		{
			target.movementSpeed -= value;
		}
	}
}