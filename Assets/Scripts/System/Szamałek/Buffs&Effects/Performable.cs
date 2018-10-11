using UnityEngine;
using System.Collections;

namespace Effects
{
	public abstract class Performable : ScriptableObject
	{
		public abstract void Perform (Creature target);
	}
}

