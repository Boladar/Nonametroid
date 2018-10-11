using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

public class Projectile : MonoBehaviour {
	Vector2 velocity;
	public float TimeToDestroy = 5f;
	float DestroyTime;
	public LayerMask layerMask;
	public string EnemyTag;
	public float ProjectileDamage;
	public EffectChain ProjectileEffect;

	// Use this for initialization
	void Start () {
		velocity = GetComponent<Rigidbody2D> ().velocity;
		DestroyTime = Time.time + TimeToDestroy;
	}
	
	// Update is called once per frame
	void Update () {
		//look for hit in the next frame
		RaycastHit2D hit = Physics2D.Raycast (transform.position, velocity.normalized, 1f ,layerMask);
		Debug.DrawLine(this.transform.position,this.transform.position + new Vector3(velocity.normalized.x, velocity.normalized.y));
		if (hit.collider != null) {
			//projectile hit its target
			if (hit.collider.gameObject.tag == EnemyTag) {
				EnemyAi enemy = hit.collider.gameObject.GetComponent<EnemyAi> ();

				DealDamage(enemy);
			}
			Destroy (this.gameObject);
		}

		if (Time.time >= DestroyTime)
			Destroy (this.gameObject);
	}

	void DealDamage(EnemyAi enemy)
	{
		enemy.HP -= ProjectileDamage;
	}

}
	