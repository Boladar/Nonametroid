using System;
using System.Collections.Generic;

namespace AI
{
	public class AttackSequence
	{
		public List<EnemyAttack> attacks; 
		public int i;

		public AttackSequence (List<EnemyAttack> attacks)
		{
			this.attacks = attacks;
			i = 0;
		}
	}
}

