using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public enum MovementType { Normal, Ladder, Cover};

    public MovementType myMovement;

    public int moveSpeed;
    public int ladderSpeed;

    void FixedUpdate() {
        switch (myMovement) {
            case MovementType.Normal:
                MovingChecker();
                break;
            case MovementType.Ladder:
                LadderChecker();
                break;
            case MovementType.Cover:
                CoverChecker();
                break;
        }
	}

    public void MovingChecker() {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * moveSpeed);
        }
    }

    public void LadderChecker() {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * ladderSpeed);
        }
    }

    public void CoverChecker() {
    }

    public void OnTriggerStay(Collider collider) {
        if (collider.transform.tag == "Ladder") {
            GetComponent<Rigidbody>().useGravity = false;
            myMovement = MovementType.Ladder;
        } 
    }
    
    public void OnTriggerExit(Collider collider) {
        if (collider.transform.tag == "Ladder") {
            GetComponent<Rigidbody>().useGravity = true;
            myMovement = MovementType.Normal;
        }
    }
}
