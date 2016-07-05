using UnityEngine;
using System.Collections;

public class Roomba : AIEnemy {
	public float explosionRange;
	private Vector3 explosionPos;
	public float damage;
	public float myTimer;

	public void Awake(){
		if(player != null){
			playerTransform = player.transform;
			StateChecker ();
		}
	}

	public void Update(){
		StateChecker ();
	}

	public override void PlayerDetection(){
		playerDis = Vector3.Distance(playerTransform.position, transform.position);
		if (playerDis < distanceOfSight) {
			myState = AIState.Attack;
		}
		else {
			myState = AIState.Patrol;
		}
	}

	public override void HealthChecker(){
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
		if (!standStillToAttack) {
			GetComponent<MyUnit> ().RecieveTarget (playerTransform);
			transform.LookAt (new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
		}
		if(playerDis < explosionRange){
			StartCoroutine (WaitToExplode());
			standStillToAttack = true;
		}
	}

	public IEnumerator WaitToExplode(){
		yield return new WaitForSeconds(myTimer);
		if (playerDis < explosionRange) {
			player.GetComponent<Health> ().HealOrDamage ("damage", damage);
			Destroy (gameObject);
		}
		else {
			standStillToAttack = false;
		}
	}

	public override void StateChecker (){
		switch (myState) {
		case AIState.Patrol:
			Patrolling ();
			break;
		case AIState.Attack:
			AttackPlayer ();
			break;
		}
		PlayerDetection ();
		transform.rotation = Quaternion.Euler (270, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
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

	public void Patrolling(){
		if(!gotPatrolPoint){
			GetComponent<MyUnit> ().RecieveTarget (patrolPoints [curPatrolPoint]);
			gotPatrolPoint = true;
		}
		transform.LookAt (new Vector3(patrolPoints[curPatrolPoint].position.x, transform.position.y, patrolPoints[curPatrolPoint].position.z));
	}

	public override void LineOfSightDetection(){
		
	}
}
