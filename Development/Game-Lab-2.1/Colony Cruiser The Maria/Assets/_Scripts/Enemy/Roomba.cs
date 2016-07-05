using UnityEngine;
using System.Collections;

public class Roomba : AIEnemy {
	public float explosionRange;
	private Vector3 explosionPos;
	private float timesHit;
	public float damageReductionRate;
	public GameObject myPoolManager;
	public string[] blockade;
	public float myTimer;
	public float grenadeDamage;
	public float radius;
	public float power;

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
		}
		if(playerDis < explosionRange){
			StartCoroutine (TimerToExplode());
			standStillToAttack = true;
		}
	}

	public IEnumerator TimerToExplode(){
		yield return new WaitForSeconds (myTimer);	
		explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		myPoolManager.GetComponent<ExplosionPool>().PickFromPool(explosionPos);
		foreach (Collider col in colliders) {
			Rigidbody rb = col.GetComponent<Rigidbody>();
			if (rb != null) {
				float rayDis = Vector3.Distance (transform.position, rb.transform.position);
				Vector3 rayDir = (rb.transform.position - transform.position);
				if(Physics.Raycast (transform.position, rayDir, out rayHit, rayDis)){
					string rayTag = rayHit.transform.tag;
					bool stop = false;
					if(rayHit.transform.gameObject == rb.gameObject){
						GrenadeEffect (rb.gameObject);
						rb.AddExplosionForce (power, explosionPos, radius);
					}
					else for(int i = 0; i < blockade.Length; i++){
							if(rayTag == blockade[i]){
								stop = true;
								break;
							}
						}
					if (!stop) {
						EntityBetween (rayHit.transform.position, rb);
					}
				}
			}
		}
		Destroy (gameObject);
	}

	public void EntityBetween(Vector3 newOrigin, Rigidbody oldTarget){
		float rayDis = Vector3.Distance (newOrigin, oldTarget.transform.position);
		Vector3 rayDir = (oldTarget.transform.position - newOrigin);
		if(Physics.Raycast (newOrigin, rayDir, out rayHit, rayDis)){
			timesHit++;
			if(rayHit.transform.gameObject != oldTarget.gameObject){
				if(rayHit.transform.tag != "Wall" && rayHit.transform.tag != "Cover" && rayHit.transform.tag != "Ground"){
					EntityBetween(rayHit.transform.position, oldTarget);
				}
			}
			else if(rayHit.transform.gameObject == oldTarget.gameObject) {
				GrenadeEffect (oldTarget.gameObject);
				oldTarget.AddExplosionForce (power, explosionPos, radius);
			}
		}
	}

	public void GrenadeEffect(GameObject objectHit){
		float fragDistance = Vector3.Distance (transform.position, objectHit.GetComponent<Transform>().position);
		float damageReduction = timesHit * damageReductionRate;
		float fragDamage = (radius - fragDistance) * grenadeDamage - damageReduction;
		switch (objectHit.tag) {
		case "Player":
			objectHit.GetComponent<Health> ().HealOrDamage ("damage", fragDamage);
			break;
		case "Body":
			print (objectHit.tag);
			break;
		};
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
