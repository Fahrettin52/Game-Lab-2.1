using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {
	public delegate void WeaponDelegate();
	public delegate bool AimDelegate (bool aims);
	public WeaponDelegate shootDelegate;
	public WeaponDelegate ammoSwitchDelegate;
	public AimDelegate aimDelegate;
    private bool aiming;

	public List<GameObject> weaponList = new List<GameObject>();
	public float curWeapon;

	public GameObject[] grenades;
	public int curGrenade;

	void Start(){
		curWeapon = -1;
		WeaponSwitch ();
	}

	public void Update(){
		if (shootDelegate != null || aimDelegate != null) {
			shootDelegate ();
			if (Input.GetButton ("Fire2")) {
                aiming = true;
			} 
            else {
                aiming = false;
			}
            aimDelegate(aiming);
		}
		if(Input.GetAxis("Mouse ScrollWheel") > 0 && aiming == false){
			if (curWeapon < weaponList.Count-1) {
				curWeapon++;
				WeaponSwitch ();
			}
			else{
				curWeapon = 0;
				WeaponSwitch ();
			}
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0 && aiming == false) {
			if (curWeapon > 0) {
				curWeapon--;
				WeaponSwitch ();
			}
			else {
				curWeapon = weaponList.Count-1;
				WeaponSwitch ();
			}
		}
		AmmoSwitch ();
		GrenadeSwitch ();
	}

	public void WeaponSwitch(){
        for (int i = 0; i < weaponList.Count; i++) {
            if (i == curWeapon) {
                weaponList[i].SetActive(true);
            } 
            else {
                weaponList[i].SetActive(false);
            }
        }
	}

    public void WeaponObtained(GameObject newWeapon) {
        curWeapon++;
        weaponList.Add(newWeapon);
        WeaponSwitch();
    }

    public void AmmoSwitch(){
		if (Input.GetButtonDown ("SwitchAmmo") && ammoSwitchDelegate != null) {
			ammoSwitchDelegate();
		}
	}

	public void GrenadeSwitch(){
		if(Input.GetButtonDown("SwitchGrenades")){
			if (curGrenade < grenades.Length - 1) {
				curGrenade++;
			}
			else {
				curGrenade = 0;
			}
			for (int i = 0; i < grenades.Length; i++) {
				if (i == curGrenade) {
					grenades[i].SetActive(true);
				} 
				else {
					grenades[i].SetActive(false);
				}
			}
		}
	}
}
