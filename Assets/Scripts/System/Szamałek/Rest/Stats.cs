using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Stats/Stats")]
public class Stats : ScriptableObject
{
	public float hp;
	public float jumpForce;
	public float movementSpeed;
	public float additionalDamage;
}

