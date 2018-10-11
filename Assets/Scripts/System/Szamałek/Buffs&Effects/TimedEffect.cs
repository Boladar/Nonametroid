using UnityEngine;
using System.Collections;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/TimedEffect")]
	public class TimedEffect : BasicEffect
	{
		public float duration;
		public float timeToStart;
		public float repeatTime;

		protected override void PerformEffect (Creature target, float finalValue)
		{
			if (repeatTime > 0)
				EffectController.instance.StartEffectRepeater (target,
					GetEffectContainer (target) as TimedEffectContainer, finalValue);
			else
				EffectController.instance.StartDoTimedEffect (target,
					GetEffectContainer (target) as TimedEffectContainer, finalValue);
		}

		protected override Container GetEffectContainer (Creature target)
		{
			foreach (TimedEffectContainer container in target.activeTimedEffects) {
				if (container.timedEffect == this)
					return container;
			}
			return null;
		}

		public override bool RegisterEffect (Creature target)
		{
			if (!CheckForReduplication (target)) {
				target.activeTimedEffects.Add (new TimedEffectContainer (this, EffectController.instance.ApplyProtections (target, this)));
				return true;
			}
			return false;
		}

		public override bool CheckForReduplication (Creature target)
		{
			TimedEffectContainer container = GetEffectContainer (target) as TimedEffectContainer;
			if (container != null) {
				container.ResolveReduplication (target, this);
				return true;
			}
			return false;
		}

		public override void LogChangeInTargetValue (Creature target, float value)
		{
			TimedEffectContainer container = GetEffectContainer (target) as TimedEffectContainer;
			container.ChangeinTargetValue = value;
		}

		public override void UnregisterEffect (Creature target)
		{
			foreach (TimedEffectContainer container in target.activeTimedEffects) {
				if (container.timedEffect == this) {
					container.timeToEnd = 0;
					target.activeTimedEffects.Remove (container);
					break;
				}
			}
		}

		public override void EndEffectAction (Creature target)
		{
			Debug.Log ("end effect: " + this.name);
			TimedEffectContainer container = GetEffectContainer (target) as TimedEffectContainer;
			effectAction.EndEffect (target, container.ChangeinTargetValue);
		}
	}
}