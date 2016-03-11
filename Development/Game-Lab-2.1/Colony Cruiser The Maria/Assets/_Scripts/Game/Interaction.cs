using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

    public enum InteractionType { Door, Weapon, HealthPack, StimPack, Displai, CodeBreaker };

    public InteractionType myType;

    public void Interact(GameObject player) {
        switch (myType) {
            case InteractionType.Door:
                InteractDoor();
                break;
            case InteractionType.Weapon:
                InteractWeapon(player);
                break;
            case InteractionType.HealthPack:
                InteractHealthPack(player);
                break;
            case InteractionType.StimPack:
                InteractStimPack(player);
                break;
            case InteractionType.Displai:
                InteractDisplai();
                break;
            case InteractionType.CodeBreaker:
                InteractCodeBreaker();
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

    public void InteractHealthPack(GameObject player) {
        print("This is a Healthpack");
    }

    public void InteractStimPack(GameObject player) {
        print("This is a Stimpack");
    }

    public void InteractDisplai() {
        print("This is a Display");
    }

    public void InteractCodeBreaker() {
        print("This is a Codebreaker");
    }
}
