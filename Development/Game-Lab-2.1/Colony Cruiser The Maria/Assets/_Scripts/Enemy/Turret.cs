using UnityEngine;
using System.Collections;

public class Turret : AIEnemy {
	public enum AIState
	{
		Patrol,
		Attack
	}
	public AIState myState;
	public GameObject player;
	private Transform playerTransform;
	public float playerDis;
	public float distanceOfSight;
	public float bulletDamage;
	public float shootDirValueX;
	public float shootDirValueY;
	public float overheatCountDown;
	private float overheatCountDownReset;
	public float cooldownTimer;
	public float playerOutOfSightTimer;
	public float playerOutOfSightTimerReset;
	public RaycastHit rayHit;
	public RaycastHit bulletHit;
	public bool isCoolingOff;

	public void Start(){
		player = GameObject.FindGameObjectWithTag ("Playerr");
		overheatCountDownReset = overheatCountDown;
		playerOutOfSightTimerReset = playerOutOfSightTimer;
	}

	public void Update(){
		if(player != null){
			playerTransform = player.transform;
			PlayerDetection ();
			StateChecker ();
		}
	}

	public override void PlayerDetection (){
		playerDis = Vector3.Distance(playerTransform.position, transform.position);
		if(playerDis < distanceOfSight){
			Physics.Raycast (transform.position, transform.forward, out rayHit, distanceOfSight);
			if(rayHit.transform.tag == player.tag){
				myState = AIState.Attack;
			}
		}
	}

	public override void HealthChecker (){
	
	}

	public override void AttackPlayer (){
		transform.LookAt (playerTransform);
		if (rayHit.transform.tag != player.tag) {
			playerOutOfSightTimer -= Time.deltaTime;
			if (playerOutOfSightTimer < 1) {
				CountdownResets ();
				myState = AIState.Patrol;
				StateChecker ();
			}
		} 
		else {
			playerOutOfSightTimer = playerOutOfSightTimerReset;
		}
		if (!isCoolingOff) {
			Vector3 shootDir = transform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
			Physics.Raycast (transform.position, shootDir, out bulletHit, distanceOfSight);
			overheatCountDown -= Time.deltaTime;
			if (bulletHit.transform.tag == player.tag) {
				player.GetComponent<Health> ().HealOrDamage ("damage", bulletDamage);
			} else {
				//dit moet een else if worden I guess, hierin moet een plaatje van de bullet komen in een muur
				//Als het een livestock enemy is moet ie de livestock enemy damagen
				//Als het een AI enemy is moet het stoppen met vuren
			}
			if (overheatCountDown < 1) {
				isCoolingOff = true;
				StartCoroutine (CoolingOff ());
			}
		}
	}

	public override void StateChecker (){
		switch (myState) {
		case AIState.Patrol:
			//moet dit nog verder uitbreiden, maar ga eerst verder met het aanvallen van speler
			transform.Rotate (Vector3.right, Time.deltaTime);
			break;
		case AIState.Attack:
			AttackPlayer ();
			break;
		}
	}

	public override void LineOfSightDetection (){
	
	}
		
	public IEnumerator CoolingOff (){
		yield return new WaitForSeconds (cooldownTimer);
		CountdownResets ();
		isCoolingOff = false;
	}

	public void CountdownResets(){
		playerOutOfSightTimer = playerOutOfSightTimerReset;
		overheatCountDown = overheatCountDownReset;
	}
}
