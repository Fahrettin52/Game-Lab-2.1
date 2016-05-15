using UnityEngine;
using System.Collections;

public class SmokeGrenade : VirtualGrenades {

	public void OnEnable(){
		myPoolManager = GameObject.Find (poolManagerString);
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.AddForce (transform.forward * throwingPower);
		StartCoroutine ("TimerToExplode");
	}

	public override IEnumerator TimerToExplode(){
		yield return new WaitForSeconds (myTimer);	
		Vector3 explosionPos = transform.position;
		myPoolManager.GetComponent<ExplosionPool>().PickFromPool(explosionPos);
		Destroy (gameObject);
	}
}