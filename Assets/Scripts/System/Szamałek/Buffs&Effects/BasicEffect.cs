using UnityEngine;
using System.Collections;
using Effects.EffectActions;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Basic Effect")]
	public class BasicEffect : Performable
	{
		public EffectAction effectAction;
		public float baseValue;

		public override void Perform (Creature target)
		{
			if (RegisterEffect (target))
				PerformEffect (target, EffectController.instance.ApplyProtections (target, this));
		}

		protected virtual void PerformEffect (Creature target, float finalValue)
		{
			ApplyEffectAction (target, finalValue);
		}

		public virtual void EndEffect (Creature target)
		{
			EndEffectAction (target);				
			UnregisterEffect (target);
		}

		private float GetFinalValue (Creature target)
		{
			BasicEffectContainer container = (GetEffectContainer (target)) as BasicEffectContainer;
			return container.valueAfterProtections;
		}

		protected virtual Container GetEffectContainer (Creature target)
		{
			foreach (BasicEffectContainer container in target.activeBasicEffects) {
				if (container.basicEffect == this)
					return container;
			}
			return null;
		}

		public virtual void LogChangeInTargetValue(Creature target, float value)
		{
			BasicEffectContainer container = (GetEffectContainer (target)) as BasicEffectContainer;
			container.ChangeinTargetValue = value;
		}

		public virtual bool RegisterEffect (Creature target)
		{
			if (!CheckForReduplication (target)) {
				target.activeBasicEffects.Add (new BasicEffectContainer (this, EffectController.instance.ApplyProtections (target, this)));
				return true;
			}
			return false;
		}

		public virtual bool CheckForReduplication (Creature target)
		{
			foreach (BasicEffectContainer container in target.activeBasicEffects) {
				if (container.basicEffect == this) {
					container.ResolveReduplication (target, this);
					return true;
				}
			}
			return false;
		}

		public virtual void UnregisterEffect (Creature target)
		{
			foreach (BasicEffectContainer container in target.activeBasicEffects) {
				if (container.basicEffect == this) {
					target.activeBasicEffects.Remove (container);
					break;
				}
			}
		}

		public void ApplyEffectAction (Creature target, float finaleValue)
		{
			LogChangeInTargetValue(target, effectAction.ApplyEffect (target, finaleValue));
		}

		public virtual void EndEffectAction (Creature target)
		{
			Debug.Log ("end effect: " + this.name);
			BasicEffectContainer container = GetEffectContainer (target) as BasicEffectContainer;
			effectAction.EndEffect (target, container.ChangeinTargetValue);
			container.ChangeinTargetValue = 0;
			container.valueAfterProtections = 0;
		}
	}
}

