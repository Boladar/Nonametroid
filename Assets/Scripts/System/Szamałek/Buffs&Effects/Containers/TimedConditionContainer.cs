using System;

namespace Effects
{
	public class TimedConditionContainer : ConditionContainer
	{
		public float timeToEnd;
		public TimedConditionContainer (Condition condition) : base (condition)
		{
			TimedCondition tc = condition as TimedCondition;
			this.condition = condition;
			this.timeToEnd = tc.duration;
		}
		
	}
}

