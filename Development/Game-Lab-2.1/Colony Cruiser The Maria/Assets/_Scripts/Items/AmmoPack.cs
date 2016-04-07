using UnityEngine;
using System.Collections;

public class AmmoPack : MonoBehaviour {
	
	public enum AmmoType { AssaultNormal, AssaultFlachette, };

	public AmmoType myType;

	public GameObject assaultRifle;

	public void DetermenAmmo (){
		switch (myType) {
		case AmmoType.AssaultNormal:
			print ("1") ;
			AddAmmo (0);
			break; 

		case AmmoType.AssaultFlachette:
			AddAmmo (1);	
			break;
		}
	}

	public void AddAmmo (int number){
		switch (number) {
		case 0:
			print ("2") ;
			assaultRifle.GetComponent<AssaultRifle>().AmmoAdd();
			break; 

		case 1:
			
			break;
		}
	}
}
