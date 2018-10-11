using UnityEngine;
using System.Collections;

namespace AI
{
	public abstract class Move: Action
	{
		protected bool IsFacingPlayer (StateController controller)
		{
			float playerCenterX = Controller.PLAYER.GetComponent<BoxCollider2D> ().bounds.center.x;
			float thisCenterX = controller.GetComponent<BoxCollider2D> ().bounds.center.x;
			if ((controller.scale.x > 0 && playerCenterX > thisCenterX) ||
			    (controller.scale.x < 0 && playerCenterX < thisCenterX))
				return true;
	
			return false;
		}
	}
}


