using UnityEngine;
using System.Collections;

public class UnlockWeapon : MonoBehaviour {

    public GameObject pickedWeapon;

    public void OnTriggerEnter(Collider trigger) {
        if (trigger.tag == "Player") {
			trigger.GetComponent<WeaponManager>().WeaponObtained(pickedWeapon);
			Destroy (gameObject);
        }
    }
}
