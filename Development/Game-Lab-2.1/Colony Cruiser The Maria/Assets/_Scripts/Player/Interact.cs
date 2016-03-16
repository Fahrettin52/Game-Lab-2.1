using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {

    public float rayDistance;
    public RaycastHit hitObject;

	void Update () {
        InterAction();
    }

    public void InterAction() {
        if (Input.GetButtonDown("Interact")) {
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.blue, rayDistance);
            if (Physics.Raycast(transform.position, GetComponentInChildren<Camera>().transform.forward, out hitObject, rayDistance)) {
                if (hitObject.transform.tag == "Interactable") {
                    hitObject.transform.GetComponent<Interaction>().Interact(gameObject);
                } 
            } 
            else {
                return;
            }
        }
    }
}
