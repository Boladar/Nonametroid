using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	[CreateAssetMenu (menuName = "PluggableAI/Decisions/Player In Range")]
	public class PlayerInRangeDecision : DirectionedLookDecision
	{
		public override bool Decide (StateController controller)
		{
			if (Look (controller))
				return CheckPosition (controller);
	
			return false;
		}

		protected bool CheckPosition (StateController controller)
		{
			BoxCollider2D playerCollider = Controller.PLAYER.GetComponent<BoxCollider2D> ();
			float playerCenterY = playerCollider.bounds.center.y;
			float playerMaxY = playerCollider.bounds.max.y;
			float playerMinY = playerCollider.bounds.min.y;
	
			BoxCollider2D thisBoxCollider = controller.GetComponent<BoxCollider2D> ();
			float thisMaxY = thisBoxCollider.bounds.max.y;
			float thisMinY = thisBoxCollider.bounds.min.y;
	
			if (playerCenterY < thisMaxY && playerCenterY > thisMinY)
				return true;
	
			return false;
		}
	}
}

