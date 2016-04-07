using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

    public enum InteractionType { Door, Weapon, Displai, CodeBreaker, Loot, Cabinet };

    public InteractionType myType;

    public void Interact(GameObject player) {
        switch (myType) {
        case InteractionType.Door:
            InteractDoor();
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

    public void InteractDoor() {
        print("This is a door");
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
		print("Loot Info");
	}

	public void InteractCabinet () {
		print("This is a Cabinet");
		gameObject.GetComponent<AmmoPack> ().DetermenAmmo ();
	}
}
