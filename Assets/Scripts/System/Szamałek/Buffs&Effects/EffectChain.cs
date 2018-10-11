using UnityEngine;
using System.Collections.Generic;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/EffectChain")]
	public class EffectChain : ScriptableObject
	{
		public Performable[] effects;
		public List<Condition> conditions;

		public void EndEffects (Creature target)
		{
			foreach (BasicEffect effect in effects) {
				effect.UnregisterEffect (target);
				effect.EndEffect (target);
			}	
		}

		public void EndConditions (Creature target)
		{
			foreach (Condition condition in conditions) {
				ConditionContainer container = GetConditionContinerInTarget (target, condition);
				if (container != null) {
					container.isActive = false;
				}
			}
		}

		public void PerformEffects (Creature target)
		{
			foreach (Performable performable in effects) {
				performable.Perform (target);
			}
		}

		public void RegisterConditions (Creature target)
		{
			foreach (Condition condition in conditions)
				condition.RegisterCondition (target);
		}

		public void StartCheckingConditionsForTarget (Creature target)
		{
			for (int i = 0; i < target.activeConditions.Count; i++) {
				ConditionContainer container = target.activeConditions [i];
				if (container.isActive == false) {
					container.isActive = true;
					EffectController.instance.StartConditionChecker (target, container);
				}
			}

			for (int i = 0; i < target.activeTimedConditions.Count; i++) {
				TimedConditionContainer container = target.activeTimedConditions [i];
				if (container.isActive == false) {
					container.isActive = true;
					EffectController.instance.StartTimedConditionChecker (target, container);
				}
			}
		}

		private ConditionContainer GetConditionContinerInTarget (Creature target, Condition condition)
		{
			foreach (ConditionContainer targetContainer in target.activeConditions) {
				if (targetContainer.condition == condition)
					return targetContainer;
			}
			return null;
		}
	}
}