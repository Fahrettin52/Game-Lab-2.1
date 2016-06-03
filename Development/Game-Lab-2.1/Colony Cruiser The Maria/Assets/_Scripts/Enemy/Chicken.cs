using UnityEngine;
using System.Collections;

public class Chicken : LiveStockEnemy {
	public float flightLimit;
	public float chanceToFlee;

	public override void PlayerDetection (){
		playerDis = Vector3.Distance(playerTransform.position, transform.position);
		if(playerDis < distanceOfSight && myState == LiveStockState.Patrol){
			if (Physics.Raycast (transform.position, transform.forward, out rayHit, distanceOfSight)) {
				if (rayHit.transform.tag == player.tag) {
					myState = LiveStockState.Attack;
				} 
			}
			else {
				return;
			}
		}
	}

	public override void HealthChecker (){
		if(health < flightLimit){
			float flightCheck = Random.Range (0, 100);
			if(flightCheck < chanceToFlee){
				myState = LiveStockState.Flee;
			}
		}
		if(health < 1){
			Destroy (gameObject);
		}
	}

	public override float RecieveDamage (float recievedDamage){
		health -= recievedDamage;
		HealthChecker ();
		return health;
	}

	public override float RecieveCriticalDamage(float recievedDamage){
		health -= recievedDamage * critMultiplier;
		HealthChecker ();
		return health;
	}

	public override void AttackPlayer (){

	}

	public override void StateChecker (){
		switch (myState) {
		case LiveStockState.Patrol:
			PlayerDetection ();
			Patrolling ();
			break;
		case LiveStockState.Attack:
			AttackPlayer ();
			break;
		case LiveStockState.Flee:
			Fleeing();
			break;
		case LiveStockState.Regenerate:
			Regeneration ();
			break;
		}
	}

	public override void Patrolling (){

	}

	public override void Fleeing(){

	}

	public override void Regeneration(){

	}
}
