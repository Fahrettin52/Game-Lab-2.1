﻿using UnityEngine;
using System.Collections;

public class Pistol : AbstractWeapon {

	public new void Start(){
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		player.GetComponent<WeaponManager> ().shootDelegate = Shooting;
		player.GetComponent<WeaponManager> ().aimDelegate = Aiming;
		startPos = transform.localPosition;
		startRot = transform.rotation;
	}

	public override void Shooting(){
		Debug.DrawRay (transform.position, transform.forward*5000, Color.blue, 4);
		if (Physics.Raycast (camero.transform.position, camero.transform.forward, out rayHit, rayDis)) {
			switch (rayHit.transform.tag) {
			case "Head":
				print ("Hit the Head");
				Destroy (rayHit.transform.gameObject);
				break;
			case "Limbs":
				print ("Hit Limb");
				Destroy (rayHit.transform.gameObject);
				break;
			case "Body":
				print ("Hit the Body");
				GameObject parento = rayHit.transform.parent.gameObject;
				Destroy (parento);
				break;
			default:
				print ("NOT AN ENEMY!");
				break;
			}
		}
		else {
			print ("MISSED");
		}
	}

	//	public void ShootDirection(){
	//		Random.Range (camero.transform.forward - 1, camero.transform.forward + 1);
	//	}

	public override void Reloading(){

	}

	public override bool Aiming(bool aim){
		if (!aim) {
			if (camero.GetComponent<Camera> ().fieldOfView < maxFieldOfView) {
				camero.GetComponent<Camera> ().fieldOfView += zoomSpeed * Time.deltaTime;
				//Vervang onderste code later met animation
				transform.rotation = startRot;
				transform.localPosition = startPos;
			}
		}
		else {
			if (camero.GetComponent<Camera> ().fieldOfView > minFieldOfView) {
				camero.GetComponent<Camera> ().fieldOfView -= zoomSpeed * Time.deltaTime;
				//Vervang onderste code later met animation
				transform.rotation = new Quaternion(0, 0, 0, 0);
				transform.localPosition = new Vector3(0, -0.16f, 1.064f);
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
