using UnityEngine;
using System.Collections.Generic;
using Effects;

public abstract class Creature : MonoBehaviour
{
	public float hp;
	public float jumpForce;
	public float movementSpeed;
	public float additionalDamage;
	public List<BasicEffectContainer> activeBasicEffects = new List<BasicEffectContainer>();
	public List<TimedEffectContainer> activeTimedEffects = new List<TimedEffectContainer> ();
	public List<ProtectionContainer> activeProtections = new List<ProtectionContainer> ();
	public List<TimedProtectionContainer> activeTimedProtections = new List<TimedProtectionContainer> ();
	public List<ConditionContainer> activeConditions = new List<ConditionContainer> ();
	public List<TimedConditionContainer> activeTimedConditions = new List<TimedConditionContainer> ();
	public Stats creatureStats;
	public LayerMask GroundMask;
	public LayerMask playerMask;
	[HideInInspector] public Vector2 scale;

	private BoxCollider2D boxCollider;

	protected virtual void Start()
	{
		if (creatureStats == null)
			throw new MissingStatsException (this.gameObject.name);

		hp = creatureStats.hp;
		jumpForce = creatureStats.jumpForce;
		movementSpeed = creatureStats.movementSpeed;
		additionalDamage = creatureStats.additionalDamage;

		boxCollider = GetComponent<BoxCollider2D> ();
		scale.x = 1;
	}

	protected abstract void Die ();

	public void DoDamage(float value)
	{
		hp -= value;

		Debug.Log ("taken damage: " + this.name +", value: " + value);
		if (hp <= 0)
			Die();
	}

	public void Flip()
	{
		Vector2 scale = this.transform.localScale;
		scale.x *= -1;
		this.transform.localScale = scale;
		this.scale *= -1;
	}

	protected float GetCheckX()
	{
		boxCollider = GetComponent<BoxCollider2D> ();
		float maxX = boxCollider.bounds.max.x;
		float minX = boxCollider.bounds.min.x;

		float CheckX;
		if (transform.localScale.x == 1)
			CheckX = maxX + 1;
		else
			CheckX = minX - 1;

		return CheckX;
	}

	public bool CheckForGroundInFront()
	{
		float minY = boxCollider.bounds.min.y;
		float checkX = GetCheckX ();

		RaycastHit2D hit = Physics2D.Raycast (new Vector2(checkX,minY),Vector2.down,10,GroundMask);
		Debug.DrawLine(new Vector2(checkX,minY), new Vector2(checkX,minY -10));
		if (hit.collider != null) {
			//Debug.Log ("hit" + hit.collider.name);
			return true;
		}
		return false;
	}

	public bool IsGrounded()
	{
		float minX = boxCollider.bounds.min.x;
		float maxX = boxCollider.bounds.max.x;

		float minY = boxCollider.bounds.min.y;
		RaycastHit2D hitMinX = Physics2D.Raycast (new Vector2 (minX, minY), Vector2.down, 10, GroundMask);
		if (hitMinX.collider != null)
			return true;

		RaycastHit2D hitMaxX = Physics2D.Raycast (new Vector2 (maxX, minY), Vector2.down, 10, GroundMask);
		if (hitMaxX.collider != null)
			return true;

		return false;
	}

	public bool CheckForObstacleInFront(LayerMask checkMask)
	{
		float minY = boxCollider.bounds.min.y;
		float sizeY = boxCollider.size.y;

		float checkX = GetCheckX ();

		RaycastHit2D hit = Physics2D.Raycast(new Vector2(checkX,minY),Vector2.up,sizeY,checkMask);
		Debug.DrawLine(new Vector2(checkX,minY),new Vector3(checkX,minY + sizeY));
		if (hit.collider != null) {
			return false;
		}
		return true;
	}

}
