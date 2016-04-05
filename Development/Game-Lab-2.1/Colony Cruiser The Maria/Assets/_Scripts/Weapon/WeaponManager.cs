﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {
	public delegate void WeaponDelegate();
	public delegate bool AimDelegate (bool aims);
	public WeaponDelegate shootDelegate;
	public WeaponDelegate ammoSwitchDelegate;
	public WeaponDelegate quickMeleeDelegate;
	public AimDelegate aimDelegate;
    private bool aiming;

	public List<GameObject> weaponList = new List<GameObject>();
	public float curWeapon;
	public Image weaponIcon;
	public Text ammoCountHolder;

	public GameObject[] grenades;
	public Transform hand;
	public int curGrenade;

	public void Update(){
		WeaponAction ();
		WeaponControl ();
		ThrowGrenade ();
	}

	public void WeaponAction(){
		if (shootDelegate != null) {
			shootDelegate ();
		}
		if (aimDelegate != null) {
			if(Input.GetButton ("Fire2")){
				aiming = true;
			} 
			else {
				aiming = false;
			}
			aimDelegate (aiming);
		}
		else {
			quickMeleeDelegate ();
		}
	}

	public void WeaponControl(){
		AmmoSwitch ();
		GrenadeSwitch ();
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

	public void ThrowGrenade(){
		if(Input.GetButtonDown("Grenade")){
			//moet hier de granaat gooien
			Instantiate(grenades[curGrenade], hand.position, hand.rotation);
		}
	}

	public void GrenadeSwitch(){
		if(Input.GetButtonDown("SwitchGrenade")){
			if (curGrenade < grenades.Length) {
				curGrenade++;
			}
			else {
				curGrenade = 0;
			}
		}
	}
}
