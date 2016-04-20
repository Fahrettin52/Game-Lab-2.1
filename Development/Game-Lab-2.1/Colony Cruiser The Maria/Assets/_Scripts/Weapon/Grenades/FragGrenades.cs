using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragGrenades : AbstractGrenades {
	private Vector3 explosionPos;
	private float timesHit;
	public float damageReductionRate;

	public void OnEnable(){
		myPoolManager = GameObject.Find (poolManagerString);
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.AddForce (transform.forward * throwingPower);
		StartCoroutine ("TimerToExplode");
	}

	public override IEnumerator TimerToExplode(){
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

	public override void GrenadeEffect(GameObject objectHit){
		float fragDistance = Vector3.Distance (transform.position, objectHit.GetComponent<Transform>().position);
		float damageReduction = timesHit * damageReductionRate;
		float fragDamage = (radius - fragDistance) * grenadeDamage - damageReduction;
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
