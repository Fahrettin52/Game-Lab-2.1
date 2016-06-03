using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {
	public float health;
	public float critMultiplier;
	protected float playerDis;
	public float distanceOfSight;
	public GameObject player;
	protected Transform playerTransform;
	public RaycastHit rayHit;

	public abstract void PlayerDetection();

	public abstract void HealthChecker ();

	public abstract float RecieveDamage (float recievedDamage);

	public abstract float RecieveCriticalDamage (float recievedDamage);

	public abstract void AttackPlayer ();

	public abstract void StateChecker ();
}
