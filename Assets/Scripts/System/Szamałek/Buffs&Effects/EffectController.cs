using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public class EffectController : MonoBehaviour
	{
	
		public static EffectController instance;

		void Awake ()
		{
			instance = this;
		}

		public void RemoveEffectChain (Creature target, EffectChain effectChain)
		{
			effectChain.EndEffects (target);
			effectChain.EndConditions (target);
		}

		public void RequestEffectChain (Creature target, EffectChain effectChain)
		{
			effectChain.PerformEffects (target);
			effectChain.RegisterConditions (target);
			effectChain.StartCheckingConditionsForTarget (target);
		}

		public void StartEffectRepeater (Creature target, TimedEffectContainer container, float value)
		{
			StartCoroutine (EffectRepeater (target, container, value));
		}

		public void StartDoTimedEffect (Creature target, TimedEffectContainer container, float value)
		{
			StartCoroutine (DoTimedEffect (target, container, value));
		}

		public void StartConditionChecker (Creature target, ConditionContainer conditionContainer)
		{
			StartCoroutine (ConditionChecker (target, conditionContainer));
		}

		public void StartTimedConditionChecker(Creature target, TimedConditionContainer conditionContainer)
		{
			StartCoroutine (TimedConditionChecker (target, conditionContainer));
		}

		public void StartTimedProtection(Creature target, TimedProtectionContainer protectionContainer)
		{
			StartCoroutine(ApplyTimedProtection(target,protectionContainer));
		}

		private IEnumerator ConditionChecker (Creature target, ConditionContainer container)
		{
			while (container.isActive) {
				CheckCondition (target, container);
				yield return null;
			}

			yield break;
		}

		private IEnumerator TimedConditionChecker(Creature target, TimedConditionContainer container)
		{
			TimedCondition condition = container.condition as TimedCondition;
			float startTime = Time.time + condition.timeToStart;
			float endTime = startTime + condition.duration;

			while (Time.time < startTime)
				yield return null;

			while (Time.time <= endTime && container.isActive) {
				endTime = startTime + container.timeToEnd;
				CheckCondition (target, container);
				yield return null;
			}

			condition.UnregisterCondition (target);
		}

		private void CheckCondition(Creature target,ConditionContainer conditionContainer)
		{
			Condition condition = conditionContainer.condition;
			bool conditionFulfilled = condition.conditionAction.isConditionFulfilled(target);

			if (conditionFulfilled && condition.trueEffect != null) {
				condition.trueEffect.Perform (target);
					conditionContainer.isActive = false;
					condition.UnregisterCondition (target);
					
			} else if (!conditionFulfilled && condition.falseEffect != null) {
				condition.falseEffect.Perform (target);
					conditionContainer.isActive = false;
					condition.UnregisterCondition (target);
			}
		}

		public float CalculateProtection (BasicEffect basicEffect, float value)
		{
			if (value == 100)
				return 0;

			float percentage = (100 - value) / 100;

			Debug.Log ("protection : " + value + ", against: " + basicEffect.name);

			return basicEffect.baseValue * percentage;
		}

		public float ApplyProtections (Creature target, BasicEffect basicEffect)
		{
			float finalProtection = 0;
			foreach (ProtectionContainer container in target.activeProtections) {
				if (container.protection.CheckTarget (basicEffect))
					finalProtection += container.value;
			}

			foreach (TimedProtectionContainer container in target.activeTimedProtections) {
				if (container.protection.CheckTarget (basicEffect))
					finalProtection += container.value;
			}

			if (finalProtection == 0)
				return basicEffect.baseValue;
			else
				return CalculateProtection (basicEffect, finalProtection);
		}

		public void RecalculateProtections(Creature target)
		{
			Debug.Log ("welcome to recalculate protectinos");
			foreach (BasicEffectContainer container in target.activeBasicEffects) {
				container.basicEffect.EndEffectAction (target);
				container.basicEffect.ApplyEffectAction (target, ApplyProtections (target, container.basicEffect));
			}

			foreach (TimedEffectContainer container in target.activeTimedEffects) {
				container.timedEffect.EndEffectAction (target);
				container.timedEffect.ApplyEffectAction(target,ApplyProtections(target,container.timedEffect));
			}
		}

		private IEnumerator EffectRepeater (Creature target, TimedEffectContainer container, float value)
		{
			TimedEffect timedEffect = container.timedEffect;
			float startTime = Time.time + timedEffect.timeToStart;
			float endTime = Time.time + timedEffect.timeToStart + timedEffect.duration;
	
			while (Time.time < startTime)
				yield return null;
			
			while (Time.time <= endTime) {
				endTime = startTime + container.timeToEnd;
				StartCoroutine (DoTimedEffect (target, container, value));
				yield return new WaitForSeconds (timedEffect.repeatTime);
			}
		}

		private IEnumerator DoTimedEffect (Creature target, TimedEffectContainer container, float value)
		{
			TimedEffect timedEffect = container.timedEffect;
	
			float startTime = Time.time + timedEffect.timeToStart;
			float endTime = Time.time + timedEffect.timeToStart + timedEffect.duration;
	
			Debug.Log ("start time: " + startTime + ", name: " + timedEffect.name);
			Debug.Log ("czas: " + Time.time);
	
			while (Time.time < startTime)
				yield return null;
	
			timedEffect.ApplyEffectAction (target, value);
	
			while (Time.time <= endTime) {
				endTime = startTime + container.timeToEnd;
				yield return null;
			}
			Debug.Log ("end time: " + endTime + "| effect final duration: " + container.timeToEnd + ", name: " + timedEffect.name); 
			timedEffect.EndEffect (target);
		}

		private IEnumerator ApplyTimedProtection(Creature target, TimedProtectionContainer container)
		{
			TimedProtection protection = container.protection as TimedProtection;
			float startTime = Time.time + protection.timeToStart;
			float endTime = Time.time + protection.timeToStart + protection.duration;

			while (Time.time < startTime)
				yield return null;
			
			protection.Perform (target);

			while (Time.time <= endTime) {
				endTime = startTime + container.timeToEnd;
				yield return null;
			}
			protection.UnregisterEffect (target);
		}
	}
}
 