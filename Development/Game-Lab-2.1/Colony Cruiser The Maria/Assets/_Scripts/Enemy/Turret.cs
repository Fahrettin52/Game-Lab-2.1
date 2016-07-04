using UnityEngine;
using System.Collections;

public class Turret : AIEnemy {
	public enum RotationPhase
	{
		Phase1,
		Phase2,
		Phase3,
		Phase4
	}
	public RotationPhase myRotPhase;
	private float nextFire;
	public float fireRate;
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
	public RaycastHit bulletHit;
	public bool isCoolingOff;
	public bool rotateNegative;
	public Transform rayCastHolder;
	public GameObject soundManager;
	public GameObject shotLight;
	public GameObject shotLight2;

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
			StateChecker ();
		}
	}

	public override void PlayerDetection (){
		playerDis = Vector3.Distance(playerTransform.position, transform.position);
		if(playerDis < distanceOfSight && myState == AIState.Patrol){
			print ("in range");
			if (Physics.Raycast (rayCastHolder.position, rayCastHolder.forward, out rayHit, distanceOfSight)) {
				Debug.DrawRay (rayCastHolder.position, rayCastHolder.forward * distanceOfSight, Color.white, 2.5f);
				if (rayHit.transform.tag == player.tag) {
					print ("detected");
					myState = AIState.Attack;
				} 
			}
			else {
				return;
			}
		}
	}

	public override void HealthChecker(){
		if(health < 1){
			Destroy (gameObject);
		}
	}

	public override float RecieveDamage (float recievedDamage){
		if (recievedDamage > 0) {
			health -= recievedDamage;
			HealthChecker ();
		}
		return health;
	}

	public override float RecieveCriticalDamage(float recievedDamage){
		if (recievedDamage > 0) {
			health -= recievedDamage * critMultiplier;
			HealthChecker ();
		}
		return health;
	}

	public override void AttackPlayer (){
		if (player.GetComponent<Movement> ().myMovement != Movement.MovementType.Dead) {
			print ("looking at player");
			transform.LookAt (playerTransform);
			if (Physics.Raycast (rayCastHolder.position, rayCastHolder.forward, out rayHit, distanceOfSight)) {
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
			}
			if (!isCoolingOff) {
				Vector3 shootDir = transform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
				if (Physics.Raycast (transform.position, shootDir, out bulletHit, distanceOfSight)) {
					overheatCountDown -= Time.deltaTime;
					if (Time.time > nextFire) {
						StartCoroutine (PlayTheLight());
						soundManager.GetComponent<SoundManager> ().Turretshot ();
						print (bulletHit.transform.name);
						Debug.DrawRay (rayCastHolder.position, rayCastHolder.forward * distanceOfSight, Color.red, 2.5f);
						if (bulletHit.transform.tag == player.tag) {
							print ("Damaging player");
							player.GetComponent<Health> ().HealOrDamage ("damage", bulletDamage);
						} 
						else {
							//dit moet een else if worden I guess, hierin moet een plaatje van de bullet komen in een muur
							//Als het een livestock enemy is moet ie de livestock enemy damagen
							//Als het een AI enemy is moet het stoppen met vuren
						}
						nextFire = Time.time + fireRate;
					}
				}
				if (overheatCountDown < 1) {
					isCoolingOff = true;
					StartCoroutine (CoolingOff ());
				}
			}
		}
		else {
			myState = AIState.Patrol;
		}
	}
	public override void StateChecker (){
		switch (myState) {
		case AIState.Patrol:
			PlayerDetection ();
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

	public IEnumerator PlayTheLight(){
		shotLight.SetActive (true);
		shotLight2.SetActive (true);
		yield return new WaitForSeconds (0.05f);
		shotLight.SetActive (false);
		shotLight2.SetActive (false);
	}
}
