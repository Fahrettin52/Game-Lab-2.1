using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {

    public float rayDistance;
    public RaycastHit hitObject;

	void Update () {
        Interaction();
    }

    public void Interaction() {
        if (Input.GetButtonDown("Interact")) {
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.blue, rayDistance);
            if (Physics.Raycast(transform.position, GetComponentInChildren<Camera>().transform.forward, out hitObject, rayDistance)) {
                if (hitObject.transform.tag == "Interactable") {
					hitObject.transform.GetComponent<Interaction>().Interact(GameObject.Find("PlayerPlaceholder").gameObject);	
					print (gameObject);
                } 
            } 
            else {
                return;
            }
        }
    }
}
