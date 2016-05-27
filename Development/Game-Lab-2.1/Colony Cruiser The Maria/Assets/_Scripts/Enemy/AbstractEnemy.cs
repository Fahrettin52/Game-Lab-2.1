using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {

	public abstract void PlayerDetection();

	public abstract void HealthChecker ();

	public abstract float RecieveDamage (float recievedDamage);

	public abstract float RecieveCriticalDamage (float recievedDamage);

	public abstract void AttackPlayer ();

	public abstract void StateChecker ();
}
