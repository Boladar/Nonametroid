using UnityEngine;
using System.Collections;
using AI;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
	public override void Act(StateController controller)
	{
		Patrol (controller);
	}

	private void Patrol(StateController controller)
	{
		//Debug.Log ("patrol: " + controller.transform.name);
		if (CheckForGround (controller, controller.transform.position + new Vector3 (10 * controller.scale.x, 0)) &&
		   CheckForObstacle (controller, controller.transform.position + new Vector3 (10 * controller.scale.x, controller.transform.position.y + 1))) {
			
			controller.GetComponent<Rigidbody2D> ().velocity = new Vector2 (controller.creatureStats.movementSpeed * controller.scale.x,
				controller.GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			Flip (controller);
		}

	}

	private void Flip(StateController controller)
	{
		Vector2 scale = controller.scale;
		scale.x *= -1;
		controller.transform.localScale = scale;
		controller.scale = scale;
	}

	private bool CheckForGround(StateController controller,Vector2 position)
	{
		RaycastHit2D hit = Physics2D.Raycast (position,Vector2.down,10,controller.enemyStats.raycastCheckMask);
		Debug.DrawLine(position, new Vector2(position.x,position.y -10));
		if (hit.collider != null) {
			//Debug.Log ("hit" + hit.collider.name);
			return true;
		}
		return false;
	}

	private bool CheckForObstacle(StateController controller,Vector2 position)
	{
		RaycastHit2D hit = Physics2D.Raycast(position,Vector2.up,30,controller.enemyStats.raycastCheckMask);
		Debug.DrawLine(position,new Vector3(position.x,position.y + 30));
		if (hit.collider != null) {
			Debug.Log ("hit" + hit.collider.name);
			return false;
		}
		return true;
	}
}

