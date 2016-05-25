using UnityEngine;
using System.Collections;

public abstract class AbstractEnemy : MonoBehaviour {

	public abstract void PlayerDetection();

	public abstract void HealthChecker ();

	public abstract void AttackPlayer ();

	public abstract void StateChecker ();
}
