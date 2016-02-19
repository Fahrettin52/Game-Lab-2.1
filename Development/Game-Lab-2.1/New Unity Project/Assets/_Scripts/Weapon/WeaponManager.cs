using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {
	public delegate void WeaponDelegate();
	public delegate bool AimDelegate (bool aims);
	public WeaponDelegate shootDelegate;
	public AimDelegate aimDelegate;

	public List<GameObject> weaponList = new List<GameObject>();
	public float curWeapon;

	void Start(){
		WeaponSwitch ();
	}

	public void Update(){
		if (shootDelegate != null || aimDelegate != null) {
			shootDelegate ();
			if (Input.GetButton ("Fire2")) {
				aimDelegate (true);
			} else {
				aimDelegate (false);
			}
		}
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			if (curWeapon < weaponList.Count-1) {
				curWeapon++;
				WeaponSwitch ();
			}
			else{
				curWeapon = 0;
				WeaponSwitch ();
			}
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0){
			if (curWeapon > 0) {
				curWeapon--;
				WeaponSwitch ();
			}
			else {
				curWeapon = weaponList.Count-1;
				WeaponSwitch ();
			}
		}
	}

	public void WeaponSwitch(){
		for(int i = 0; i < weaponList.Count; i++){
			if (i == curWeapon) {
				weaponList [i].SetActive (true);
			}
			else {
				weaponList [i].SetActive (false);
			}
		}
	}

	public void WeaponObtained(){
		
	}

	public void AmmoSwitch(){
	
	}

	public void GrenadeSwitch(){
		
	}
}
