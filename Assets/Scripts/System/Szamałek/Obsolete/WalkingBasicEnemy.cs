using UnityEngine;
using System.Collections;

public class WalkingBasicEnemy : EnemyAi
{
	public float MovementSpeed;

	private bool CheckForGround(Vector2 position)
	{
		RaycastHit2D hit = Physics2D.Raycast (position,Vector2.down,10,RaycastCheckMask);
		Debug.DrawLine(position, new Vector2(position.x,position.y -10));
		if (hit.collider != null) {
			//Debug.Log ("hit" + hit.collider.name);
			return true;
		}
		return false;
	}

	private bool CheckForObstacle(Vector2 position)
	{
		RaycastHit2D hit = Physics2D.Raycast(position,Vector2.up,30,RaycastCheckMask);
		Debug.DrawLine(position,new Vector3(position.x,position.y + 30));
		if (hit.collider != null) {
			return false;
		}
		return true;
	}


	private void Walk()
	{
		if(CheckForGround(transform.position + new Vector3(10 * Scale.x,0)) && CheckForObstacle(transform.position + new Vector3(10 * Scale.x, transform.position.y + 1)))
		{
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (MovementSpeed * Scale.x, GetComponent<Rigidbody2D> ().velocity.y);
		}
		else
			Flip ();
	}

	override public void StandardBehaviour()
	{
		Walk ();
	}

	override public void Attack()
	{
		
	}

}

