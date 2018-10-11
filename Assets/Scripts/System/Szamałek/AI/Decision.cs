using UnityEngine;
using System.Collections;

namespace AI
{
	public abstract class Decision : ScriptableObject
	{
		public abstract bool Decide (StateController controller);
	}
}

