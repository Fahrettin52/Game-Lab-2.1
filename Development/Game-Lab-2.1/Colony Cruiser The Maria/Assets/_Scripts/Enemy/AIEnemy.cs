using UnityEngine;
using System.Collections;

public abstract class AIEnemy : AbstractEnemy {
	public enum AIState
	{
		Patrol,
		Attack
	}
	public AIState myState;

	public override abstract void PlayerDetection ();

	public override abstract void HealthChecker ();

	public override abstract float RecieveDamage (float recievedDamage);

	public override abstract float RecieveCriticalDamage (float recievedDamage);

	public override abstract void AttackPlayer ();

	public override abstract void StateChecker ();

	public abstract void LineOfSightDetection ();
}
