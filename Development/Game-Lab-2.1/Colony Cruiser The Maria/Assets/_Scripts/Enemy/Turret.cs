using UnityEngine;
using System.Collections;

public class Turret : AIEnemy {
	public enum AIState
	{
		Patrol,
		Attack
	}
	public enum RotationPhase
	{
		Phase1,
		Phase2,
		Phase3,
		Phase4
	}
	public AIState myState;
	public RotationPhase myRotPhase;
	public GameObject player;
	private Transform playerTransform;
	public float health;
	public float critMultiplier;
	private float playerDis;
	public float distanceOfSight;
	private float startRotationPoint;
	public float rotationSpeed;
	public float rotationLimit;
	private float maxRotationLimit;
	private float minRotationLimit;
	private float trueMinRotLimit;
	private float trueMaxRotLimit;
	public float bulletDamage;
	public float shootDirValueX;
	public float shootDirValueY;
	public float overheatCountDown;
	private float overheatCountDownReset;
	public float playerOutOfSightTimer;
	private float playerOutOfSightTimerReset;
	public float cooldownTimer;
	public RaycastHit rayHit;
	public RaycastHit bulletHit;
	public bool isCoolingOff;
	public bool rotateNegative;

	public void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		overheatCountDownReset = overheatCountDown;
		playerOutOfSightTimerReset = playerOutOfSightTimer;
		startRotationPoint = transform.eulerAngles.y;
		maxRotationLimit = startRotationPoint + rotationLimit;
		minRotationLimit = startRotationPoint - rotationLimit;
		if (maxRotationLimit > 360) {
			trueMaxRotLimit = maxRotationLimit - 360;
		}
		else {
			trueMaxRotLimit = maxRotationLimit;
		}
		if (minRotationLimit < 0) {
			trueMinRotLimit = 360 + minRotationLimit;
		}
		else {
			trueMinRotLimit = minRotationLimit;
		}
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
			Patrolling ();
			break;
		case AIState.Attack:
			AttackPlayer ();
			break;
		}
	}

	public void Patrolling(){
		float myAngleY = transform.rotation.eulerAngles.y;
		switch(myRotPhase){
		case RotationPhase.Phase1:
			rotateNegative = false;
			if (myAngleY > trueMaxRotLimit) {
				myRotPhase = RotationPhase.Phase2;
			}
			break;
		case RotationPhase.Phase2:
			if (myAngleY < trueMinRotLimit) {
				rotateNegative = true;
			}
			else{
				myRotPhase = RotationPhase.Phase3;
			}
			break;
		case RotationPhase.Phase3:
			if (myAngleY > trueMinRotLimit) {
				rotateNegative = true;
			}
			else {
				myRotPhase = RotationPhase.Phase4;
			}
			break;
		case RotationPhase.Phase4:
			if (myAngleY > trueMaxRotLimit) {
				rotateNegative = false;
			}
			else {
				myRotPhase = RotationPhase.Phase1;
			}
			break;
		}
		if (!rotateNegative) {
			transform.Rotate (Vector3.up, Time.deltaTime * rotationSpeed);
		}
		else {
			transform.Rotate (-Vector3.up, Time.deltaTime * rotationSpeed);
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
