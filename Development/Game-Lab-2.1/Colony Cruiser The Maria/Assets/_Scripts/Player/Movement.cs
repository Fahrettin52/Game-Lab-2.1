using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public enum MovementType { Normal, Ladder, Cover, Dead};

    public MovementType myMovement;

    public int moveSpeed;
    public int ladderSpeed;
    public int coverSpeed;
    public int runSpeed;
    public int startSpeed;
    public int crouchSpeed;
    public bool Iscrouching;
    public bool mayJump;

    public void Start() {
        startSpeed = moveSpeed;
        Iscrouching = false;
        mayJump = true;
    }

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

        if (Input.GetButtonDown("Crouch") && Iscrouching == false){
            moveSpeed = crouchSpeed;
            GetComponent<BoxCollider>().size = new Vector3(1, 0.5f, 1);
            Iscrouching = true;     
        } 
        else if (Input.GetButtonDown("Crouch") && Iscrouching == true){
            GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            moveSpeed = startSpeed;
            Iscrouching = false;
        }

        if (Input.GetButton("Run") && Iscrouching == false) {
            moveSpeed = runSpeed;
        } 

        if (Input.GetButtonUp("Run")) {
            moveSpeed = startSpeed;
        }

        if (Input.GetButtonDown("Jump") && mayJump == true) {
            GetComponent<Rigidbody>().AddForce(transform.up * 250);
            mayJump = false;
        }
    }

    public void LadderChecker() {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * ladderSpeed);
        }
    }

    public void CoverChecker() {
        if (Input.GetAxis("Vertical") != 0) {
            if (GetComponent<CameraControl>().camRotationX >= 25 && GetComponent<CameraControl>().camRotationX <= 125) {
                transform.Translate(new Vector3(Input.GetAxis("Vertical"), 0, 0) * Time.deltaTime * coverSpeed);
            }

            if (GetComponent<CameraControl>().camRotationX <= -25 && GetComponent<CameraControl>().camRotationX >= -125) {
                transform.Translate(new Vector3(-Input.GetAxis("Vertical"), 0, 0) * Time.deltaTime * coverSpeed);
            }
        }
        if (Input.GetAxis("Vertical") != 0) {
            if (GetComponent<CameraControl>().camRotationX > -25 && GetComponent<CameraControl>().camRotationX < 25) {
                GetComponent<CameraControl>().myView = CameraControl.ViewType.Normal;
                GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                myMovement = MovementType.Normal;
            }
        }
     }

    public void OnTriggerStay(Collider collider) {
        if (collider.transform.tag == "Ladder") {
            GetComponent<Rigidbody>().useGravity = false;
            myMovement = MovementType.Ladder;
        }
        if (collider.transform.tag == "Cover" && Input.GetButtonDown ("Interact")) {
            print("1");
            if (myMovement == MovementType.Normal) {
                print("2");
                GetComponent<CameraControl>().myView = CameraControl.ViewType.Cover;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                transform.localRotation = Quaternion.Euler(0, collider.transform.rotation.y -180, 0);
                GetComponent<BoxCollider>().size = new Vector3(1, 0.5f, 1);
                myMovement = MovementType.Cover;
            } 
            else if (Input.GetButtonDown("Interact")) {
                print("3");
                if (myMovement == MovementType.Cover) {
                    print("4");
                    GetComponent<CameraControl>().myView = CameraControl.ViewType.Normal;
                    GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    myMovement = MovementType.Normal;
                }
            }
        }
    }
    
    public void OnTriggerExit(Collider collider) {
        if (collider.transform.tag == "Ladder") {
            GetComponent<Rigidbody>().useGravity = true;
            myMovement = MovementType.Normal;
        }

        if (collider.transform.tag == "Cover") {
            if (myMovement == MovementType.Cover) { 
                GetComponent<CameraControl>().myView = CameraControl.ViewType.Normal;
                GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                myMovement = MovementType.Normal;
            }
        }
    }
        
    public void OnCollisionEnter(Collision collider) {
        mayJump = true;
    }
}
