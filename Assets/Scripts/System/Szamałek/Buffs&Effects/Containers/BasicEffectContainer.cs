using System;

namespace Effects
{
	public class BasicEffectContainer : Container,IReduplicationResolver
	{
		public BasicEffect basicEffect;
		public float valueAfterProtections;
		public float ChangeinTargetValue;

		public BasicEffectContainer (BasicEffect  basicEffect, float value)
		{
			this.basicEffect = basicEffect;
			this.valueAfterProtections = value;
		}

		public virtual void ResolveReduplication (Creature target,Performable performable)
		{
			BasicEffect effect = performable as BasicEffect;
			basicEffect.EndEffectAction (target);
			this.valueAfterProtections += EffectController.instance.ApplyProtections (target, effect);
			basicEffect.ApplyEffectAction (target, valueAfterProtections);
		}
	}
}

