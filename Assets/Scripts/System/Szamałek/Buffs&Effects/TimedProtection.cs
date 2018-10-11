using UnityEngine;
using System.Collections;

namespace Effects
{
	public abstract class TimedProtection : Protection
	{
		public float duration;
		public float timeToStart;

		public override void Perform (Creature target)
		{
			if (!CheckForReduplication (target)) {
				if (RegisterEffect (target)) {
					EffectController.instance.StartTimedProtection (target, GetContainer (target) as TimedProtectionContainer);
				}
			}
		}

		public override bool CheckForReduplication (Creature target)
		{
			foreach (TimedProtectionContainer container in target.activeTimedProtections) {
				if (container.protection == this) {
					container.ResolveReduplication (target, this);
					return true;
				}
			}
			return false;
		}

		public override ProtectionContainer GetContainer (Creature target)
		{
			foreach (TimedProtectionContainer container in target.activeTimedProtections) {
				if (container.protection == this)
					return container;
			}
			return null;
		}

		public override bool RegisterEffect (Creature target)
		{
			target.activeTimedProtections.Add (new TimedProtectionContainer (this));
			return true;
		}

		public override void UnregisterEffect (Creature target)
		{
			for (int i = 0; i < target.activeTimedProtections.Count; i++) {
				TimedProtectionContainer container = target.activeTimedProtections [i];
				if (container.protection == this) {
					target.activeTimedProtections.Remove (container);
				}
			}
			Debug.Log ("unregister protection: " + this.name);
			EffectController.instance.RecalculateProtections (target);
		}
	}
}
