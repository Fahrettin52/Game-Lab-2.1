using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

    public enum InteractionType { Door, Weapon, HealthPack, StimPack, Displai, CodeBreaker };

    public InteractionType myType;

    public void Interact(GameObject player) {
        switch (myType) {
            case InteractionType.Door:
                interactDoor();
                break;
            case InteractionType.Weapon:
                interactWeapon(player);
                break;
            case InteractionType.HealthPack:
                interactHealthPack(player);
                break;
            case InteractionType.StimPack:
                interactStimPack(player);
                break;
            case InteractionType.Displai:
                interactDisplai();
                break;
            case InteractionType.CodeBreaker:
                interactCodeBreaker();
                break;
        }
    }

    public void interactDoor() {
        print("This is a door");
    }

    public void interactWeapon(GameObject player) {
        gameObject.GetComponent<UnlockWeapon>().Unlock(player);
        print("This is a Weapon");
    }

    public void interactHealthPack(GameObject player) {
        print("This is a Healthpack");
    }

    public void interactStimPack(GameObject player) {
        print("This is a Stimpack");
    }

    public void interactDisplai() {
        print("This is a Display");
    }

    public void interactCodeBreaker() {
        print("This is a Codebreaker");
    }
}
