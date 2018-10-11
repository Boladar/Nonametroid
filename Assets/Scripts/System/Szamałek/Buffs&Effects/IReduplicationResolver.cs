using System;

namespace Effects
{
	public interface IReduplicationResolver
	{
		void ResolveReduplication(Creature target, Performable effect);
	}
}

