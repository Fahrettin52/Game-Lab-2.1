using UnityEngine;
using System.Collections;

public class SmokeBarrel : AbstractBarrel {
	public GameObject myPoolManager;

	public override void Explode(){
		Vector3 explosionPos = transform.position;
		myPoolManager.GetComponent<ExplosionPool>().PickFromPool(explosionPos);
	}

	public override void ExplosionEffect(){
		
	}
}
