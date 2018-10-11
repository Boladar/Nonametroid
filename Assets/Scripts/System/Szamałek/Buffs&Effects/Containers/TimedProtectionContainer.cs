using System;

namespace Effects
{
	public class TimedProtectionContainer : ProtectionContainer
	{	
		public float timeToEnd;

		public TimedProtectionContainer (Protection protection) : base (protection)
		{
			this.protection = protection;
			this.timeToEnd = (protection as TimedProtection).duration;
		}

		public override void ResolveReduplication (Creature target, Performable performable)
		{
			TimedProtection tp = performable as TimedProtection;
			this.timeToEnd += tp.duration;
		}
	}
}


