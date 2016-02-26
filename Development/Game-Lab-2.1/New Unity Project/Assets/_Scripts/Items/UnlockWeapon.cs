using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnlockWeapon : WeaponManager {

    public GameObject pistol;

    public void OnTriggerEnter(Collider trigger) {
        if (trigger.tag == "Player") {
            weaponList.Add(pistol);
            WeaponSwitch();
        }
        Destroy(gameObject);
    }
}
