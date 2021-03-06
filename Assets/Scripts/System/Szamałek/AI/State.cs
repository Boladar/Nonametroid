﻿using UnityEngine;
using System.Collections;

namespace AI
{
	[CreateAssetMenu (menuName = "PluggableAI/State")]
	public class State : ScriptableObject
	{
		public Action[] actions;
		public StateTransition[] transitions;
		public Color sceneGizmoColor = Color.grey;
	
		public float TransitionCheckDelay;

		public void UpdateState (StateController controller)
		{
			DoActions (controller);
			CheckTransitions (controller);
		}

		public void DoActions (StateController controller)
		{
			for (int i = 0; i < actions.Length; i++) {
				actions [i].Act (controller);
			}
		}

		public void CheckTransitions (StateController controller)
		{
			for (int i = 0; i < transitions.Length; i++) {
				bool decisionSucceded = transitions [i].decision.Decide (controller);
	
				if (decisionSucceded)
					controller.TransitionToState (transitions [i].trueState);
				else
					controller.TransitionToState (transitions [i].falseState);
			}
		}
	
	}
}

