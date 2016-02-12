using UnityEngine;
using System.Collections;

public class AbstractWeapon : MonoBehaviour {
	private GameObject player;
	private GameObject camero;
	private bool aiming;
	public float rayDis;
	private RaycastHit rayHit;

	public float zoomSpeed;
	public float maxFieldOfView;
	public float minFieldOfView;

	public void Start(){
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		player.GetComponent<WeaponManager> ().shootDelegate = Shooting;
		player.GetComponent<WeaponManager> ().aimDelegate = Aiming;
	}

	public void Shooting(){
		Debug.DrawRay (transform.position, transform.forward*5000, Color.blue, 4);
		if (Physics.Raycast (transform.position, transform.forward, out rayHit, rayDis)) {
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
				Destroy (rayHit.transform.gameObject.GetComponentInParent<GameObject>());
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

	public void Reloading(){
	
	}

	public bool Aiming(bool aim){
		if (!aim) {
			if (camero.GetComponent<Camera> ().fieldOfView < maxFieldOfView) {
				camero.GetComponent<Camera> ().fieldOfView += zoomSpeed * Time.deltaTime;
			}
		}
		else {
			if (camero.GetComponent<Camera> ().fieldOfView > minFieldOfView) {
				camero.GetComponent<Camera> ().fieldOfView -= zoomSpeed * Time.deltaTime;
			}
		}
		return aim;
	}

	public void AmmoRemove(){
		
	}

	public void AmmoAdd(){

	}

	public void AmmoEffect(){
		
	}

	public void HitChecker(){
		
	}
}
