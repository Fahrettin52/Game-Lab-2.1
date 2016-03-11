using UnityEngine;
using System.Collections;

public class UnlockWeapon : MonoBehaviour {

    public GameObject pickedWeapon;

    public void Unlock(GameObject player) {
        player.GetComponent<WeaponManager>().WeaponObtained(pickedWeapon);
	    Destroy(gameObject);
    }
}
