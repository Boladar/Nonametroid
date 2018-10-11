using UnityEngine;
using System.Collections;

namespace AI
{
	[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
	public class ChaseAction : Move
	{
		public override void Act (StateController controller)
		{
			if (controller.IsGrounded () && controller.CheckForObstacleInFront (controller.GroundMask)) {
	
				if (IsFacingPlayer (controller)) {
					if (controller.CheckForObstacleInFront (controller.playerMask)) {
						controller.GetComponent<Rigidbody2D> ().velocity = new Vector2 (controller.creatureStats.movementSpeed * controller.scale.x,
							controller.GetComponent<Rigidbody2D> ().velocity.y);
					} else
						controller.GetComponent <Rigidbody2D> ().velocity = 
							new Vector2 (0, controller.GetComponent <Rigidbody2D> ().velocity.y); 	
				} else
					controller.Flip ();
			} else if (controller.IsGrounded ())
				controller.Flip ();		
		}
	
	}
}


