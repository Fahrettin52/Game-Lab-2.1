using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interaction : MonoBehaviour {

    public enum InteractionType { Door, Weapon, Displai, CodeBreaker, Loot, Cabinet };

    public InteractionType myType;

	public GameObject particleSpark;

    public void Interact(GameObject player) {
        switch (myType) {
		case InteractionType.Door:
			InteractDoor (transform.gameObject);
            break;
        case InteractionType.Weapon:
            InteractWeapon(player);
            break;
        case InteractionType.Displai:
            InteractDisplai();
            break;
        case InteractionType.CodeBreaker:
            InteractCodeBreaker();
            break;
		case InteractionType.Loot:
			InteractLoot();
			break;
		case InteractionType.Cabinet:
			InteractCabinet();
			break;
        }
    }

	public void InteractDoor(GameObject currentObject) {		
		if (currentObject.name == "Door") {
			currentObject.GetComponentInParent<Animator>().SetTrigger ("mayOpen");
			particleSpark.SetActive (true);
		}
		if (currentObject.name == "DoorBroken") {
			currentObject.GetComponentInParent<Animator>().SetTrigger("broken");
			particleSpark.SetActive (true);
		}
    }

    public void InteractWeapon(GameObject player) {
        gameObject.GetComponent<UnlockWeapon>().Unlock(player);
        print("This is a Weapon");
    }

    public void InteractDisplai() {
        print("This is a Display");
    }

    public void InteractCodeBreaker() {
        print("This is a Codebreaker");
    }

	public void InteractLoot() {
		GameObject.Find ("LootManager").GetComponent<LootManager> ().SceneChecker ();
		GameObject.Find ("LootManager").GetComponent<LootManager> ().LootChecker ();
		print("Loot Info");
	}

	public void InteractCabinet () {
		print("This is a Cabinet");
		gameObject.GetComponent<AmmoPack> ().DetermenAmmo ();
	}
}
