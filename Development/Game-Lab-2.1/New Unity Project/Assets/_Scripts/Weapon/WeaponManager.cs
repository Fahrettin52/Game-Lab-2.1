using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {
	public delegate void WeaponDelegate();
	public delegate bool AimDelegate (bool aims);
	public WeaponDelegate shootDelegate;
	public AimDelegate aimDelegate;

	public void Update(){
		if(Input.GetButtonDown("Fire1")){
			shootDelegate();
		}
		if (Input.GetButton("Fire2")) {
			aimDelegate (true);
		}
		else {
			aimDelegate (false);
		}
	}

	public void WeaponSwitch(){
	
	}

	public void WeaponObtained(){
		
	}

	public void AmmoSwitch(){
	
	}

	public void GrenadeSwitch(){
		
	}
}
