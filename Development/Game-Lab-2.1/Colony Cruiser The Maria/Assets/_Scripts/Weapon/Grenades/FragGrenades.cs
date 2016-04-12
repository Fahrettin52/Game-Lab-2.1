using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragGrenades : AbstractGrenades {

	public void OnEnable(){
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
				rb.AddExplosionForce (power, explosionPos, radius);
			}
		}
		//Instantiate hier de particle shit
		Destroy (gameObject);
	}

	public override void GrenadeEffect(GameObject objectHit){
		float fragDistance = Vector3.Distance (transform.localPosition, objectHit.GetComponent<Transform>().localPosition);
		float fragDamage = (radius - fragDistance) * grenadeDamage;
		switch (objectHit.tag) {
		case "Player":
			objectHit.GetComponent<Health> ().HealOrDamage ("damage", fragDamage);
			break;
		case "Limbs":
		case "Head":
		case "Body":
			print (objectHit.tag);
			break;
		};
	}
}
