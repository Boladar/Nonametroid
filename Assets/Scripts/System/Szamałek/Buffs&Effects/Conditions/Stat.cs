using System;
using Effects.EffectActions;
using UnityEngine;

namespace Effects.Conditions
{
	public abstract class Stat : ConditionAction
	{
		public enum TypeOfComparison
		{
			equals,
			greater,
			less,
			lessOrEqual,
			moreOrEqual,
			notEqual
		}

		public float value;
		public TypeOfComparison comparison;

		protected bool Compare(float valueToCheck, TypeOfComparison type)
		{
			switch (type) {
			case TypeOfComparison.equals:
				if (valueToCheck == value)
					return true;
				break;
			case TypeOfComparison.greater:
				Debug.Log ("greater : value to check: " + valueToCheck +", value + " + value);
				if (valueToCheck > value)
					return true;
				break;
			case TypeOfComparison.less:
				if (valueToCheck < value)
					return true;
				break;
			case TypeOfComparison.lessOrEqual:
				if (valueToCheck <= value)
					return true;
				break;
			case TypeOfComparison.moreOrEqual:
				if (valueToCheck >= value)
					return true;
				break;
			case TypeOfComparison.notEqual:
				if (valueToCheck != value)
					return true;
				break;
			default:
				return false;
			}
			return false;
		}

	}
}