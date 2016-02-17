﻿using UnityEngine;
using System.Collections;

public class Shotgun : AbstractWeapon {
	public float shotCount;

	public override void OnEnable(){
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		FillDelegate ();
	}

	public override void FillDelegate(){
		player.GetComponent<WeaponManager> ().shootDelegate = null;
		player.GetComponent<WeaponManager> ().aimDelegate = null;
		player.GetComponent<WeaponManager> ().shootDelegate = Shooting;
		player.GetComponent<WeaponManager> ().aimDelegate = Aiming;
	}

	public override void Shooting(){
		for (int i = 0; i < shotCount; i++) {
			Vector3 playerPos = player.transform.position;
			shootDir = camero.transform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
			Debug.DrawRay (camero.transform.position, shootDir * 5000, Color.blue, 3);
			if (Physics.Raycast (camero.transform.position, shootDir, out rayHit, rayDis)) {
				DistanceChecker (playerPos);		
			} 
			else {
				print ("MISSED");
			}
		}
	}

	public override void DistanceChecker(Vector3 savedPos){
		float impactDistance = Vector3.Distance(savedPos, rayHit.transform.position);
		float timeToImpact = impactDistance / projectileSpeed;
		float impactDamage = new float ();
		if (impactDistance < effectiveRange) {
			impactDamage = projectileDamage - (effectiveRange - impactDistance);
		}
		else {
			impactDamage = 0;
		}
		StartCoroutine(ImpactDelay(timeToImpact, impactDamage));
	}

	public override IEnumerator ImpactDelay(float impactTime, float damage){
		switch (rayHit.transform.tag) {
		case "Head":
			print ("Hit the Head, damage x3");
			Destroy (rayHit.transform.gameObject);
			break;
		case "Limbs":
			print ("Hit Limb, damage x1");
			Destroy (rayHit.transform.gameObject);
			break;
		case "Body":
			print ("Hit the Body, damage x2");
			//Destroyen van de parent voorbeeld:
			GameObject parento = rayHit.transform.parent.gameObject;
			Destroy (parento);
			break;
		default:
			print ("NOT AN ENEMY!");
			break;
		}
		yield return new WaitForSeconds(impactTime);
	}

	public override void Reloading(){

	}

	public override bool Aiming(bool aim){
		if (!aim) {
			if (camero.GetComponent<Camera> ().fieldOfView < maxFieldOfView) {
				camero.GetComponent<Camera> ().fieldOfView += zoomSpeed * Time.deltaTime;
				//Vervang onderste code later met animation
			}
		}
		else {
			if (camero.GetComponent<Camera> ().fieldOfView > minFieldOfView) {
				camero.GetComponent<Camera> ().fieldOfView -= zoomSpeed * Time.deltaTime;
				//Vervang onderste code later met animation
			}
		}
		return aim;
	}

	public override void AmmoRemove(){

	}

	public override void AmmoAdd(){

	}

	public override void AmmoEffect(){

	}

	public override void HitChecker(){

	}
}
