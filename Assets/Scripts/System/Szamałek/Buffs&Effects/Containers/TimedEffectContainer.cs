using System;

namespace Effects
{
	public class TimedEffectContainer : BasicEffectContainer
	{
		public TimedEffect timedEffect;
		public float timeToEnd;

		public TimedEffectContainer (BasicEffect basicEffect, float value) : base (basicEffect, value)
		{
			TimedEffect timedEffect = basicEffect as TimedEffect;
			this.timedEffect = timedEffect;
			this.timeToEnd = timedEffect.duration;
			this.valueAfterProtections = value;
		}

		public override void ResolveReduplication (Creature target, Performable effect)
		{
			TimedEffect tE = effect as TimedEffect;
			this.timeToEnd += tE.duration;
		}
	}
}

