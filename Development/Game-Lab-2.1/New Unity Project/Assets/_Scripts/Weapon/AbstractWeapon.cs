using UnityEngine;
using System.Collections;

public abstract class AbstractWeapon : MonoBehaviour {
	public GameObject player;
	public GameObject camero;
	public bool aiming;
	public float rayDis;
	public RaycastHit rayHit;

	public float zoomSpeed;
	public float maxFieldOfView;
	public float minFieldOfView;
	public Vector3 startPos;
	public Quaternion startRot;

    public void Start(){
//		player = GameObject.FindWithTag ("Player");
//		camero = GameObject.Find ("Main Camera");
//		player.GetComponent<WeaponManager> ().shootDelegate = Shooting;
//		player.GetComponent<WeaponManager> ().aimDelegate = Aiming;
	}

	public abstract void Shooting ();

	public abstract void Reloading ();

	public abstract bool Aiming (bool aim);

	public abstract void AmmoRemove ();

	public abstract void AmmoAdd ();

	public abstract void AmmoEffect ();

	public abstract void HitChecker ();
}
