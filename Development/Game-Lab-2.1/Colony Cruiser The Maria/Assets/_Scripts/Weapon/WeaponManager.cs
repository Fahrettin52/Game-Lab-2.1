using UnityEngine;
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
	public bool mouseOverUI;

	public List<GameObject> weaponList = new List<GameObject>();
	public float curWeapon;
	public Image weaponIcon;
	public Text ammoCountHolder;

	public GameObject[] grenadeIcon;
	public Text grenadeText;
	public GameObject[] grenades;
	public GameObject equipNewWeapon;
	public Transform hand;
	public int curGrenade;
	public int[] grenadesCount;
	public int maxGrenadesCount;

	public Animator playerAnimator;

	public void Start(){
		GrenadeIconCheck ();
	}

	public void Update(){
		WeaponAction ();
		WeaponControl ();
		ThrowGrenade ();
	}

	public void WeaponAction(){
		if (!mouseOverUI) {
			if (shootDelegate != null) {
				shootDelegate ();
			}
			if (aimDelegate != null) {
				if (Input.GetButton ("Fire2")) {
					aiming = true;
				} else {
					aiming = false;
				}
				aimDelegate (aiming);
			} 
			else {
				quickMeleeDelegate ();
			}
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

	public void WeapinSwitchThroughButton (){
		curWeapon++;
		WeaponSwitch ();
		equipNewWeapon.GetComponent<AbstractWeapon>().WeaponChecker ();
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
        weaponList.Add(newWeapon);
		equipNewWeapon = newWeapon;
    }

    public void AmmoSwitch(){
		if (Input.GetButtonDown ("SwitchAmmo") && ammoSwitchDelegate != null) {
			ammoSwitchDelegate();
		}
	}

	public void ThrowGrenade(){
		if(Input.GetButtonDown("Grenade") && grenadesCount[curGrenade] > 0){
			Instantiate(grenades[curGrenade], hand.position, hand.rotation);
			grenadesCount [curGrenade]--;
		}
		grenadeText.text = (grenadesCount [curGrenade] + "/" + maxGrenadesCount);
	}

	public void GrenadeSwitch(){
		if(Input.GetButtonDown("SwitchGrenade")){
			if (curGrenade < grenades.Length - 1) {
				curGrenade++;
			}
			else {
				curGrenade = 0;
			}
			GrenadeIconCheck ();
		}
	}

	public void GrenadeIconCheck(){
		for(int i = 0; i < grenadeIcon.Length; i++){
			if (i == curGrenade) {
				grenadeIcon [i].SetActive (true);
			}
			else {
				grenadeIcon [i].SetActive (false);
			}
		}
	}

	public void MouseOverUI(){
		mouseOverUI = !mouseOverUI;
	}
}
