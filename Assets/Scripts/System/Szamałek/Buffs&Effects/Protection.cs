using UnityEngine;
using System.Collections;

namespace Effects
{
	public abstract class Protection : Performable
	{
		public float baseValue;

		public override void Perform (Creature target)
		{
			if(!CheckForReduplication(target))
				RegisterEffect (target);			
		}

		public abstract bool CheckTarget (BasicEffect basicEffect);

		public virtual ProtectionContainer GetContainer(Creature target)
		{
			foreach (ProtectionContainer container in target.activeProtections) {
				if (container.protection == this)
					return container;
			}
			return null;
		}

		public virtual bool CheckForReduplication (Creature target)
		{
			foreach (ProtectionContainer container in target.activeProtections) {
				if (container.protection == this) {
					container.ResolveReduplication (target, this);
					return true;
				}
			}
			return false;
		}

		public virtual bool RegisterEffect (Creature target)
		{
			target.activeProtections.Add (new ProtectionContainer(this));
			return true;
		}

		public virtual void UnregisterEffect (Creature target)
		{
			for (int i = 0; i < target.activeProtections.Count; i++) {
				ProtectionContainer container = target.activeProtections [i];
				if (container.protection == this) {
					target.activeProtections.Remove (container);
				}
			}
			EffectController.instance.RecalculateProtections (target);
		}
	}
}

