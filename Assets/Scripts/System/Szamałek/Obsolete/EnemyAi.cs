using UnityEngine;
using System.Collections;

public abstract class EnemyAi : MonoBehaviour
{
	public float HP = 10;

	public float PushCheckRadius;
	public float VisionRange;
	public Vector3 Scale;

	public LayerMask RaycastCheckMask;
	public LayerMask PlayerLayer;

	public Vector2 PushForce;

	public bool IsFacingRight = true;

	private Vector3 PlayerMiddle;
	private Vector2 Heading;
	private float DistanceToPlayer;
	private Vector2 DirectionToPlayer;

	// Update is called once per frame
	public virtual void Update ()
	{
		if (!LookForPlayer()) {
			StandardBehaviour ();
		}
		else {
			Attack();
		}
		AnimateEnemy ();

		if (HP <= 0) {
			Die ();
		}
	}

	public abstract void StandardBehaviour ();
	public abstract void Attack ();

	public  virtual bool LookForPlayer()
	{
		PlayerMiddle = Controller.PLAYER.transform.position + new Vector3 (0, Controller.PLAYER.GetComponent<BoxCollider2D> ().bounds.size.y / 2);
		Heading =  PlayerMiddle - this.transform.position;

		//check if player is within range and enemy is looking at the player
		if (Heading.sqrMagnitude < VisionRange * VisionRange ) {
			//if player is in range check if enemy can see the player
			DistanceToPlayer = Heading.magnitude;

			DirectionToPlayer = Heading / DistanceToPlayer;

			RaycastHit2D hit = Physics2D.Raycast (this.transform.position, new Vector2(Scale.x,DirectionToPlayer.y), VisionRange, PlayerLayer);

			if (hit.collider != null && hit.collider.name == "Player") {
				Debug.DrawLine(this.transform.position, PlayerMiddle);
				return false;
			}
		}
		return false;
	}

	public virtual void Push(Collider2D other)
	{
		Debug.Log ("kontakt");
		other.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(1,1) * 9000);
	}

	public  virtual void AnimateEnemy()
	{
		if (GetComponent<Rigidbody2D> ().velocity.x != 0)
			GetComponent<Animator> ().SetBool ("isRunning", true);
		else
			GetComponent<Animator> ().SetBool ("isRunning",false);
	}

	public virtual void Die()
	{
		Destroy (this.gameObject);
	}

	public void Flip()
	{
		IsFacingRight = !IsFacingRight;
		Scale = this.transform.localScale;
		Scale.x *= -1;
		this.transform.localScale = Scale;
	}
}

