using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoPack : MonoBehaviour {
	
	public enum AmmoType { AssaultNormal, AssaultFlachette };

	public AmmoType myType;

	public GameObject player;
	public GameObject assaultRifle;
	public GameObject shotgun;
	public GameObject pistol;

	public void DetermenAmmo(){
		switch (myType){
		case AmmoType.AssaultNormal:
			if (assaultRifle.GetComponent<AssaultRifle>().normalTotalAmmo < assaultRifle.GetComponent<AssaultRifle>().fullNormalAmmo){
				List<GameObject> weaponList = player.GetComponent<WeaponManager> ().weaponList;
				for (int i = 0; i < weaponList.Count; i++) {
					if (weaponList[i].name == "M4a1") {
						AddAmmo(0);
					}
				}
			}	
			break; 

		case AmmoType.AssaultFlachette:
			AddAmmo(1);	
			break;
		}
	}

	public void AddAmmo(int number){
		switch (number){
		case 0:
			assaultRifle.GetComponent<AssaultRifle>().AmmoAdd();
			break; 

		case 1:
			
			break;
		}
	}
}
