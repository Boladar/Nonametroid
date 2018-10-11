using System;

namespace Effects
{
	public class ConditionContainer
	{
		public Condition condition;
		public bool isActive;

		public ConditionContainer (Condition condition)
		{
			this.condition = condition;
			this.isActive = false;
		}
	}
}

