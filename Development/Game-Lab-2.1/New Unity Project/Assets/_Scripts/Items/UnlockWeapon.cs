using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnlockWeapon : WeaponManager {

    public GameObject pickedWeapon;

    public void OnTriggerEnter(Collider trigger) {
        if (trigger.tag == "Player") {
            pickedWeapon = this.gameObject;
            WeaponObtained(pickedWeapon);
        }
    }
}
