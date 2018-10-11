using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	[CreateAssetMenu (menuName = "PluggableAI/Decisions/Directioned Look")]
	public class DirectionedLookDecision : Decision
	{
		public override bool Decide (StateController controller)
		{
			bool targetVisible = Look (controller);
			return targetVisible;
		}

		protected bool Look (StateController controller)
		{
			Vector3 PlayerMiddle = Controller.PLAYER.transform.position + new Vector3 (0, Controller.PLAYER.GetComponent<BoxCollider2D> ().bounds.size.y / 2);
			Vector2 Heading = PlayerMiddle - controller.transform.position;
	
			//check if player is within range and enemy is looking at the player
			if (Heading.sqrMagnitude < controller.enemyStats.visionRange * controller.enemyStats.visionRange) {
				//if player is in range check if enemy can see the player
				float DistanceToPlayer = Heading.magnitude;
	
				Vector2 DirectionToPlayer = Heading / DistanceToPlayer;
	
				RaycastHit2D hit = Physics2D.Raycast (controller.transform.position, new Vector2 (controller.scale.x, DirectionToPlayer.y),
					                   controller.enemyStats.visionRange, controller.enemyStats.raycastCheckMask);
	
				if (hit.collider != null) {
					if (hit.collider.name == "Player") {
						Debug.DrawLine (controller.transform.position, PlayerMiddle);
						return true;
					}
				}
			}
			return false;
		}
	}
}
