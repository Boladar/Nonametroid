using System;
using UnityEngine;

namespace AI
{
	public abstract class EnemyAttack : ScriptableObject
	{
		public abstract void Attack (StateController controller);
	}
}

