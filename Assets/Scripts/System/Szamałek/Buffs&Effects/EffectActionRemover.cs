using UnityEngine;
using System.Collections;
using Effects.EffectActions;

namespace Effects
{
	[CreateAssetMenu (menuName = "Effects/Removers/EffectAction Remover")]
	public class EffectActionRemover : Performable
	{
		
		public EffectAction actionToRemove;

		public override void Perform (Creature target)
		{
			RemoveEffectAction (target);
		}

		private void RemoveEffectAction (Creature target)
		{
			for (int i = 0; i < target.activeBasicEffects.Count; i++) {
				BasicEffectContainer container = target.activeBasicEffects [i];
				if (container.basicEffect.effectAction == actionToRemove)
					container.basicEffect.EndEffect (target);
			}
			for (int j = 0; j < target.activeTimedEffects.Count; j++) {
				TimedEffectContainer container = target.activeTimedEffects [j];
				if (container.timedEffect.effectAction == actionToRemove)
					container.timedEffect.EndEffect (target);
			}
		}
	}
}