using System;

namespace Effects
{
	public class ProtectionContainer : IReduplicationResolver
	{
		public Protection protection;
		public float value;

		public ProtectionContainer (Protection protection)
		{
			this.protection = protection;
			value = protection.baseValue;
		}

		public virtual void ResolveReduplication (Creature target, Performable performable)
		{
			Protection prot = performable as Protection;

			this.value += prot.baseValue;
			if ((value) > 100)
				this.value = 100;
		}
	}
}

