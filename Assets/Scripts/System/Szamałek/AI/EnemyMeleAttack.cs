using System;
using UnityEngine;
using Effects;

namespace AI
{
	[CreateAssetMenu (menuName = "PluggableAI/Attacks/Mele")]
	public class EnemyMeleAttack : EnemyAttack
	{
		public float cooldown;
		public float damage;

		public EffectChain attackEffect;

		public override void Attack (StateController controller)
		{
			Vector2 center = controller.GetComponent <BoxCollider2D> ().bounds.center;
			Debug.DrawLine (center, new Vector2 (center.x + (20 * controller.scale.x), center.y));

			if (controller.meleCooldown == 0) {
				RaycastHit2D hit = Physics2D.Raycast (center, Vector2.right * controller.scale.x, 20, controller.enemyStats.PlayerMask);
				if (hit.collider != null) {

					if(attackEffect != null)
						EffectController.instance.RequestEffectChain (hit.collider.GetComponent <Controller>(),attackEffect);

					hit.collider.transform.GetComponent <Controller>().DoDamage (damage);

					controller.meleCooldown = cooldown;
					controller.attackSequence.i++;
				}
			}
		}
	}
}

