using UnityEngine;
using System.Collections;

public class StunGrenade : AbstractGrenades {
	public float stunningTime;
	public float stunAngle;

	public void OnEnable(){
		myPoolManager = GameObject.Find (poolManagerString);
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.AddForce (transform.forward * throwingPower);
		StartCoroutine ("TimerToExplode");
	}

	public override IEnumerator TimerToExplode(){
		yield return new WaitForSeconds (myTimer);	
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		myPoolManager.GetComponent<ExplosionPool>().PickFromPool(explosionPos);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null) {
				GrenadeEffect (rb.gameObject);
			}
		}
		Destroy (gameObject);
	}

	public override void GrenadeEffect(GameObject objectHit){
		switch (objectHit.tag) {
		case "Player":
			Transform player = objectHit.transform;
			//De comment hieronder wordt gebruikt om te helpen bij het instellen van de stunAngle
			//print("stunAngle is " + Vector3.Angle (player.forward, transform.position - player.position));
			if (Vector3.Angle (player.forward, transform.position - player.position) < stunAngle) {
				objectHit.GetComponent<Movement> ().stunTime = stunningTime;
				objectHit.GetComponent<Movement> ().myMovement = Movement.MovementType.Stunned;
			}
			break;
		case "Limbs":
		case "Head":
		case "Body":
			print (objectHit.tag);
			break;
		};
	}
}
