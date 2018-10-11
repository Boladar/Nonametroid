using UnityEngine;
using System.Collections.Generic;

namespace AI
{
	public class StateController : Creature
	{
		//ai system
		public State currentState;
		public State remainState;
	
		public float timeFromStart;
	
		//enemy variables
		[HideInInspector]public EnemyStats enemyStats;
	
		public  AttackSequence attackSequence;
		public float meleCooldown;
	
		// Use this for initialization
		protected override void Start ()
		{
			base.Start ();
			scale = this.transform.localScale;
			enemyStats = creatureStats as EnemyStats;
			meleCooldown = 0;
	
			if (enemyStats == null)
				throw new MissingStatsException (this.gameObject.name);
		}
		
		// Update is called once per frame
		void Update ()
		{
			meleCooldown -= Time.deltaTime;
	
			if (meleCooldown < 0)
				meleCooldown = 0;
	
			timeFromStart += Time.deltaTime;
	
			if (currentState.TransitionCheckDelay == 0) {
				currentState.UpdateState (this);
			} else {
				currentState.DoActions (this);
				if (timeFromStart >= currentState.TransitionCheckDelay)
					currentState.CheckTransitions (this);
			}
				
	
			Animate ();
		}

		void OnDrawGizmos ()
		{
			if (currentState != null) {
				Gizmos.color = currentState.sceneGizmoColor;
				Gizmos.DrawWireSphere (this.transform.position, 2f);
			}
		}

		public void TransitionToState (State nextState)
		{
			if (nextState != remainState) {
				currentState = nextState;
			}
		}

		public  virtual void Animate ()
		{
			if (GetComponent<Rigidbody2D> ().velocity.x != 0)
				GetComponent<Animator> ().SetBool ("isRunning", true);
			else
				GetComponent<Animator> ().SetBool ("isRunning", false);
		}

		protected override void Die ()
		{
			throw new System.NotImplementedException ();
		}
	}
}

