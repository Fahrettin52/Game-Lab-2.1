using UnityEngine;
using System.Collections;

public class UnlockWeapon : MonoBehaviour {

    public GameObject pickedWeapon;

    public void Unlock(GameObject player) {
		GameObject.Find("PlayerPlaceholder").GetComponent<WeaponManager>().WeaponObtained(pickedWeapon);
    }
}
