using UnityEngine;
using System.Collections.Generic;

namespace AI
{
	[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
	public class AttackAction : Action
	{
		public List<EnemyAttack> attacks;

		public override void Act (StateController controller)
		{
			if (controller.attackSequence == null)
				controller.attackSequence = new AttackSequence (this.attacks);

			if (controller.attackSequence.attacks != attacks)
				controller.attackSequence = new AttackSequence (this.attacks);

			if (controller.attackSequence.i >= controller.attackSequence.attacks.Count)
				controller.attackSequence.i = 0;

				attacks [controller.attackSequence.i].Attack (controller);	
		}
	}
}