using UnityEngine;
using System.Collections;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Conditions/Condition")]
	public class Condition : Performable
	{
		public bool isPernament;
		public ConditionAction conditionAction;
		public Performable trueEffect;
		public Performable falseEffect;

		public override void Perform (Creature target)
		{
			RegisterCondition (target);
		}

		public virtual void RegisterCondition (Creature target)
		{
			if(!CheckForReduplication(target))
				target.activeConditions.Add (new ConditionContainer (this));
		}

		public virtual void UnregisterCondition (Creature target)
		{
			for (int i = 0; i < target.activeConditions.Count; i++) {
				ConditionContainer container = target.activeConditions [i];
				if (container.condition == this) {
					target.activeConditions.Remove (container);
					break;
				}
			}
		}

		protected virtual bool CheckForReduplication(Creature target)
		{
			for (int i = 0; i < target.activeConditions.Count; i++) {
				ConditionContainer container = target.activeConditions [i];
				if (container.condition == this)
					return true;
			}	
			return false;
		}
	}
}