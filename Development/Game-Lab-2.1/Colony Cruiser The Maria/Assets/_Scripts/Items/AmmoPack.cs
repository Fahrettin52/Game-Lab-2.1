using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoPack : MonoBehaviour {
	
	public enum AmmoType { AssaultNormal, AssaultFlachette, PistolNormal, PistolIncindiary };

	public AmmoType myType;

	public GameObject player;
	public GameObject assaultRifle;
	public GameObject shotgun;
	public GameObject pistol;

	public int randomAmmoInt;

	public void DetermenAmmo(){
		List<GameObject> weaponList = player.GetComponent<WeaponManager> ().weaponList;
		int randomInt = Random.Range (1, weaponList.Count);
		string randomWeaponListString = weaponList [randomInt].name;
		switch (randomWeaponListString){
		case "M4a1":
			AddAmmo(0);
			break; 
		case "PistolPH":
			AddAmmo(1);
			break; 
		}
	}

	public void AddAmmo(int number){
		switch (number){
		case 0:
			randomAmmoInt = Random.Range (0, assaultRifle.GetComponent<AssaultRifle> ().maxAmmoType);
			assaultRifle.GetComponent<AssaultRifle>().AmmoAdd(randomAmmoInt);
			break; 
		case 1:
			randomAmmoInt = Random.Range (0, pistol.GetComponent<Pistol>().maxAmmoType);
			pistol.GetComponent<Pistol>().AmmoAdd(randomAmmoInt);
			break; 
		}
	}
}
