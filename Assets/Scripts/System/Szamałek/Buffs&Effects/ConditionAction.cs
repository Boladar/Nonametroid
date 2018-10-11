using System;
using UnityEngine;

namespace Effects
{
	public abstract class ConditionAction : ScriptableObject
	{
		public abstract bool isConditionFulfilled (Creature target);
	}
}

