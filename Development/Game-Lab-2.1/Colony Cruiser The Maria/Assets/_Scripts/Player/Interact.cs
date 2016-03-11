using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {

    public float rayDistance;
    public RaycastHit hitObject;

	void Update () {
	    if (Input.GetButtonDown("Interact")) {
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.blue, 1);
            if (Physics.Raycast(transform.position, GetComponentInChildren<Camera>().transform.forward, out hitObject, rayDistance)){
                print(hitObject.transform.tag);
                if (hitObject.transform.tag == "Interactable") {
                    hitObject.transform.GetComponent<Interaction>().Interact(gameObject);
                } 
                else {
                    print("No interaction");
                }
            } 
            else {
                return;
            }
        }
    }
}
