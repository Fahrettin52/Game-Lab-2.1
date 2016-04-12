using UnityEngine;
using System.Collections;

public abstract class LiveStockEnemy : AbstractEnemy {

	public override abstract void PlayerDetection();

	public override abstract void HealthChecker ();

	public override abstract void AttackPlayer ();

	public override abstract void StateSwitcher ();

	public abstract void Patrolling ();

	public abstract void Fleeing();

	public abstract void Regeneration();
}
