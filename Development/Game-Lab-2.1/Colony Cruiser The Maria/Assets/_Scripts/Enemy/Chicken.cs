using UnityEngine;
using System.Collections;

public class Chicken : LiveStockEnemy {
	public float flightLimit;
	public float chanceToFlee;
	public float disturbanceDistance;
	public float chanceToAttack;
	private float nextRoll;
	public float rollRate;
	public float regenRate;
	private float nextRegen;
	public float lowRiskHealth;
	public Transform[] fleePoint;
	private bool gotFleePoint;

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
				gotFleePoint = false;
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
		PlayerDetection ();
	}

	public void NextPatrolPoint(){
		if (curPatrolPoint < patrolPoints.Length - 1 && !reversePatrol) {
			curPatrolPoint++;
		}
		else {
			curPatrolPoint = 0;
			if(reversiblePatrol){
				reversePatrol = true;
			}
		}
		if (curPatrolPoint > 0 && reversePatrol) {
			curPatrolPoint--;
		}
		else {
			reversePatrol = false;
		}
		gotPatrolPoint = false;
	}

	public override void Patrolling(){
		if(!gotPatrolPoint){
			GetComponent<MyUnit> ().RecieveTarget (patrolPoints [curPatrolPoint]);
			transform.LookAt (patrolPoints[curPatrolPoint]);
			gotPatrolPoint = true;
		}
	}

	public override void Fleeing(){
		if(!gotFleePoint){
			int randomFleePoint = Random.Range (0, fleePoint.Length);
			GetComponent<MyUnit> ().RecieveTarget (fleePoint[randomFleePoint]);
			gotFleePoint = true;
		}
	}

	public override void Regeneration(){
		if (playerDis > disturbanceDistance) {
			if (Time.time > nextRegen && health < lowRiskHealth) {
				health++;
				nextRegen = Time.time + regenRate;
			} 
			else {
				myState = LiveStockState.Patrol;
			}
		}
		else {
			if (Time.time > nextRoll) {
				float attackRoll = Random.Range (0, 100);
				if(attackRoll < chanceToAttack){
					myState = LiveStockState.Attack;
				}
				nextRoll = Time.time + rollRate;
			} 
		}
	}
}
