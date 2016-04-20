using UnityEngine;
using System.Collections;

public abstract class AIEnemy : AbstractEnemy {

	public override abstract void PlayerDetection ();

	public override abstract void HealthChecker ();

	public override abstract void AttackPlayer ();

	public override abstract void StateSwitcher ();

	public abstract void LineOfSightDetection ();
}
