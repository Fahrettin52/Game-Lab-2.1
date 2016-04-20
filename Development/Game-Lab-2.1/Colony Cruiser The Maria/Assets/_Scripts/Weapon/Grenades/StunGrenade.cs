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
				float rayDis = Vector3.Distance (transform.position, rb.transform.position);
				Vector3 rayDir = (rb.transform.position - transform.position);
				if (Physics.Raycast (transform.position, rayDir, out rayHit, rayDis)) {
					string rayTag = rayHit.transform.tag;
					if (rayHit.transform.gameObject == rb.gameObject) {
						print ("Object hit");
						GrenadeEffect (rb.gameObject);
					}
					else for (int i = 0; i < blockade.Length; i++) {
						if (rayTag == blockade [i]) {
							break;
						}
					}
				}
			}
		}
		Destroy (gameObject);
	}

	public override void GrenadeEffect(GameObject objectHit){
		switch (objectHit.tag) {
		case "Player":
			Transform player = objectHit.transform;
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
