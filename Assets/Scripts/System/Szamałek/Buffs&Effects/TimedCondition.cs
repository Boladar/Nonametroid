using UnityEngine;
using System.Collections;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Conditions/Timed Condition")]
	public class TimedCondition: Condition
	{
		public float duration;
		public float timeToStart;

		public override void RegisterCondition (Creature target)
		{
			if (!CheckForReduplication (target)) {
				target.activeTimedConditions.Add (new TimedConditionContainer (this));
			}
		}

		public override void UnregisterCondition (Creature target)
		{
			Debug.Log ("unregister condition");
			for (int i = 0; i < target.activeTimedConditions.Count; i++) {
				TimedConditionContainer container = target.activeTimedConditions [i];
				if (container.condition == this) {
					target.activeTimedConditions.Remove (container);
					break;
				}
			}
			Debug.Log ("target timed conditions count: " + target.activeTimedConditions.Count);
		}

		protected override bool CheckForReduplication (Creature target)
		{
			for (int i = 0; i < target.activeTimedConditions.Count; i++) {
				TimedConditionContainer container = target.activeTimedConditions [i];
				if (container.condition == this)
					return true;
			}	
			return false;
		}
	}
}
