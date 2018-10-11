using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	[System.Serializable]
	public class StateTransition
	{
		public Decision decision;
		public State trueState;
		public State falseState;
	}
}
