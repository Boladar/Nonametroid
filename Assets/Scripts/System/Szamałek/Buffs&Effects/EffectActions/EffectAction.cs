using UnityEngine;
using System.Collections;

namespace Effects.EffectActions
{
	public abstract class EffectAction : ScriptableObject
	{
		public abstract float ApplyEffect (Creature target, float value);

		public abstract void EndEffect (Creature target, float value);
	}
}

