using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObtainPistol : MonoBehaviour {

    public GameObject weaponManager;
    public GameObject pistol;

    public void OnTriggerEnter(Collider trigger) {
        if (trigger.tag == "Player") {
            weaponManager.GetComponent<WeaponManager>().weaponList.Add(pistol);
            weaponManager.GetComponent<WeaponManager>().WeaponSwitch();
        }
        Destroy(gameObject);
    }
}
