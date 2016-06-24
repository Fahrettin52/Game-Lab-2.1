using UnityEngine;
using System.Collections;

public class UnlockWeapon : MonoBehaviour {

    public GameObject pickedWeapon;

    public void Unlock(GameObject player) {
		print ("Unlocked Weapon");
		GameObject.Find("PlayerPlaceholder").GetComponent<WeaponManager>().WeaponObtained(pickedWeapon);
    }
}
