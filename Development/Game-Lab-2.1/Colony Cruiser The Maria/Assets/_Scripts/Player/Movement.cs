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
    public bool iscrouching;
    public bool isCovering;
    public bool mayJump;

    public float rayDistance;
    public RaycastHit hitObject;

    private Vector3 myNormal;

    public GameObject testPosition;

    public void Start() {
        startSpeed = moveSpeed;
        iscrouching = false;
        mayJump = true;
    }

    void Update() {
        Cover();
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
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * moveSpeed);

        if (Input.GetButtonDown("Crouch") && iscrouching == false){
            moveSpeed = crouchSpeed;
            GetComponent<BoxCollider>().size = new Vector3(1, 0.5f, 1);
            iscrouching = true;     
        } 
        else if (Input.GetButtonDown("Crouch") && iscrouching == true){
            GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            moveSpeed = startSpeed;
            iscrouching = false;
        }

        if (Input.GetButton("Run") && iscrouching == false) {
            moveSpeed = runSpeed;
        } 

        if (Input.GetButtonUp("Run") && iscrouching == false) {
            moveSpeed = startSpeed;
        }

        if (Input.GetButtonDown("Jump") && mayJump == true) {
            GetComponent<Rigidbody>().AddForce(transform.up * 250);
            mayJump = false;
        }
    }

    public void LadderChecker() {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * ladderSpeed);
    }

    public void CoverChecker() {
        if (Input.GetAxis("Vertical") != 0) {
            if (GetComponent<CameraControl>().camRotationX >= 15 && GetComponent<CameraControl>().camRotationX <= 120) {
                transform.Translate(new Vector3(Input.GetAxis("Vertical"), 0, 0) * Time.deltaTime * coverSpeed);
            }

            if (GetComponent<CameraControl>().camRotationX <= -15 && GetComponent<CameraControl>().camRotationX >= -120) {
                transform.Translate(new Vector3(-Input.GetAxis("Vertical"), 0, 0) * Time.deltaTime * coverSpeed);
            }
        }
        if (Input.GetAxis("Vertical") != 0) {
            if (GetComponent<CameraControl>().camRotationX > -15 && GetComponent<CameraControl>().camRotationX < 15) {
                GetComponent<CameraControl>().myView = CameraControl.ViewType.Normal;
                GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                myMovement = MovementType.Normal;
            }
        }
     }

    public void OnTriggerEnter(Collider collider) {
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

    public void Cover() {
        if (Input.GetButtonDown("Interact")) {
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.blue, rayDistance);
            if (Physics.Raycast(transform.position, GetComponentInChildren<Camera>().transform.forward, out hitObject, rayDistance)) {
                if (hitObject.transform.tag == "Cover") {
                    GetComponentInChildren<CameraControl>().myView = CameraControl.ViewType.Cover;
                    myNormal = hitObject.normal * 10 + hitObject.point;
                    Instantiate(testPosition, myNormal, testPosition.transform.rotation);
                    //transform.position = hitObject.point - new Vector3(0, 0 , +1);
                    transform.position = hitObject.normal * 1 + hitObject.point;
                    transform.LookAt(testPosition.transform.position);

                    //GetComponentInChildren<Camera>().transform.LookAt(myNormal);

                    //transform.rotation.SetLookRotation(myNormal, Vector3.up);

                    //print(this.transform.rotation);
                    //if (myMovement == MovementType.Normal) {
                    //    GetComponent<CameraControl>().myView = CameraControl.ViewType.Cover;
                    //    transform.localRotation = Quaternion.Euler(0, hitObject.transform.rotation.y - 180, 0);
                    //    GetComponent<BoxCollider>().size = new Vector3(1, 0.5f, 1);
                    //    myMovement = MovementType.Cover;
                    //}
                    //else if (hitObject.transform.tag == "Cover" && Input.GetButtonDown("Interact")) {
                    //    if (myMovement == MovementType.Cover) {
                    //        GetComponent<CameraControl>().myView = CameraControl.ViewType.Normal;
                    //        GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    //        myMovement = MovementType.Normal;
                    //        transform.localRotation = Quaternion.Euler(0, -180, 0);
                    //    }
                    //}
                }
            }
            else {
                return;
            }
        }      
    }
}
