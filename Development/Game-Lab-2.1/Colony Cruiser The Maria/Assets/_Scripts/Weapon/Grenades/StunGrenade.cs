using UnityEngine;
using System.Collections;

public class StunGrenade : AbstractGrenades {
	public float stunningTime;
	private ParticleSystem myParticles;

	public void OnEnable(){
		myParticles = GetComponent<ParticleSystem> ();
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.AddForce (transform.forward * throwingPower);
		StartCoroutine ("TimerToExplode");
	}

	public override IEnumerator TimerToExplode(){
		yield return new WaitForSeconds (myTimer);	
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null) {
				GrenadeEffect (rb.gameObject);
			}
		}
		//Instantiate hier de particle shit

		Destroy (gameObject);
	}

	public override void GrenadeEffect(GameObject objectHit){
		switch (objectHit.tag) {
		case "Player":
			//Stun hier die shit uit de speler en enemies.
			objectHit.GetComponent<Movement> ().stunTime = stunningTime;
			objectHit.GetComponent<Movement> ().myMovement = Movement.MovementType.Stunned;
			break;
		case "Limbs":
		case "Head":
		case "Body":
			print (objectHit.tag);
			break;
		};
	}
}
