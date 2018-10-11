using UnityEngine;
using System.Collections;
namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Removers/Condition Remover")]
	public class ConditionRemover : Performable
	{
		public Condition conditionToRemove;

		public override void Perform (Creature target)
		{
			RemoveCondition (target);
		}

		private void RemoveCondition (Creature target)
		{
			for (int i = 0; i < target.activeConditions.Count; i++) {
				ConditionContainer container = target.activeConditions [i];
				if (container.condition == conditionToRemove) {
					container.isActive = false;
					container.condition.UnregisterCondition (target);
				}
					
			}
		}
	}
}

