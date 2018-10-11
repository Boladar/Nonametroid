using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Stats/EnemyStats")]
public class EnemyStats : Stats
{
	public float visionRange;
	public LayerMask raycastCheckMask;
	public LayerMask PlayerMask;
}