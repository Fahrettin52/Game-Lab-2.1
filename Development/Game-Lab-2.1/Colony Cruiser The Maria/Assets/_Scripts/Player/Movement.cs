using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    public enum MovementType { Normal, Ladder, Cover, Dead, Stunned};
    public MovementType myMovement;
	public Sprite walk, crouch, ladder, cover;
	public Image currentState;
	public Rigidbody myRB;
	public float slowmoCorrection;
	public float terminalFallVelocity;
	public float myGravity;
    public float moveSpeed;
    public float ladderSpeed;
    public float coverSpeed;
	public float runSpeed;
	public float startSpeed;
	public float crouchSpeed;
	public float stunSpeed;
	public float stunTime;
	public GameObject stunScreen;
    public bool iscrouching;
    public bool isCovering;
    public bool mayJump;
    public float rayDistance;
    public RaycastHit hitObject;
    private Vector3 myNormal;

    public void Start() {
		myRB = GetComponent<Rigidbody>();
        startSpeed = moveSpeed;
        iscrouching = false;
        mayJump = true;
//		myGravity = -Physics.gravity.y;
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
		case MovementType.Stunned:
			StunChecker ();
			break;
        }
    }

    public void MovingChecker() {
		if (Input.GetButtonDown ("Interact")) {
			Cover ();
		} 

        if (Input.GetButtonDown("Crouch") && iscrouching == false){
			currentState.sprite = crouch;
            moveSpeed = crouchSpeed;
            GetComponent<BoxCollider>().size = new Vector3(1, 0.5f, 1);
            iscrouching = true;     
        } 
        else if (Input.GetButtonDown("Crouch") && iscrouching == true){
			currentState.sprite = walk;
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
			if (slowmoCorrection > 1) {
				myRB.AddForce (transform.up * (250 * slowmoCorrection));
			} 
			else {
				myRB.AddForce (transform.up * (250 * slowmoCorrection));
			}
			print (250 * slowmoCorrection);
            mayJump = false;
        }
		if (myRB.velocity.y < 0 && !mayJump) {
			if(slowmoCorrection > 1){
				myRB.AddForce (-transform.up * (-Physics.gravity.y * slowmoCorrection));
			}
			if (myRB.velocity.y < terminalFallVelocity) {
				myRB.velocity = new Vector3 (0, terminalFallVelocity, 0);
			}
		}

		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * (moveSpeed * slowmoCorrection));
    }

    public void LadderChecker() {
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * (ladderSpeed * slowmoCorrection));
    }

    public void CoverChecker() {
            if (GetComponent<CameraControl>().camRotationX >= 15 && GetComponent<CameraControl>().camRotationX <= 120) {
			transform.Translate(new Vector3(Input.GetAxis("Vertical"), 0, 0) * Time.deltaTime * (coverSpeed * slowmoCorrection));
                if (Input.GetAxis("Horizontal") < 0) {
					currentState.sprite = walk;
                    GetComponent<CameraControl>().myView = CameraControl.ViewType.Normal;
                    GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    myMovement = MovementType.Normal;
                }
            }

            if (GetComponent<CameraControl>().camRotationX <= -15 && GetComponent<CameraControl>().camRotationX >= -120) {
			transform.Translate(new Vector3(-Input.GetAxis("Vertical"), 0, 0) * Time.deltaTime * (coverSpeed * slowmoCorrection));
                if (Input.GetAxis("Horizontal") > 0) {
					currentState.sprite = walk;
                    GetComponent<CameraControl>().myView = CameraControl.ViewType.Normal;
                    GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    myMovement = MovementType.Normal;
                }
            }

            if (GetComponent<CameraControl>().camRotationX > -15 && GetComponent<CameraControl>().camRotationX < 15) {
                if (Input.GetAxis("Vertical") > 0) {
					currentState.sprite = walk;
                    GetComponent<CameraControl>().myView = CameraControl.ViewType.Normal;
                    GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    myMovement = MovementType.Normal;
                }
            }
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.transform.tag == "Ladder") {
			currentState.sprite = ladder;
			myRB.useGravity = false;
            myMovement = MovementType.Ladder;
        }
    }

    public void OnTriggerExit(Collider collider) {
        if (collider.transform.tag == "Ladder") {
			myRB.useGravity = true;
            mayJump = false;
            myMovement = MovementType.Normal;
			currentState.sprite = walk;
        }
    }      

    public void OnCollisionEnter(Collision collider) {
        mayJump = true;
    }

    public void Cover() {
        if (Physics.Raycast(transform.position, GetComponentInChildren<Camera>().transform.forward, out hitObject, rayDistance)) {
			if (hitObject.transform.tag == "Cover") {
				currentState.sprite = cover;
				myNormal = hitObject.normal * 10 + hitObject.point;
				GetComponentInChildren<CameraControl> ().myView = CameraControl.ViewType.Cover;
				transform.position = hitObject.normal + hitObject.point;
				transform.LookAt (myNormal);
				GetComponentInChildren<CameraControl> ().camRotationX = 0;
				GetComponentInChildren<CameraControl> ().camRotationY = 0;
				myMovement = MovementType.Cover;
			} 
        }
    }      

	public void StunChecker(){
		if (stunTime > 0) {
			stunTime -= Time.deltaTime * slowmoCorrection;
			stunScreen.GetComponent<CanvasGroup> ().alpha = stunTime;
		}
		else {
			myMovement = MovementType.Normal;
		}
		if (Input.GetButtonDown("Interact")) {
			Cover();
		}

		if (Input.GetButtonDown("Crouch") && iscrouching == false){
			moveSpeed = (crouchSpeed / stunSpeed) * slowmoCorrection;
			GetComponent<BoxCollider>().size = new Vector3(1, 0.5f, 1);
			iscrouching = true;     
		} 
		else if (Input.GetButtonDown("Crouch") && iscrouching == true){
			GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
			moveSpeed = (startSpeed / stunSpeed) * slowmoCorrection;
			iscrouching = false;
		}

		if (Input.GetButtonDown("Jump") && mayJump == true) {
			myRB.AddForce(transform.up * 250);
			mayJump = false;
		}
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * ((moveSpeed / stunSpeed) * slowmoCorrection));
	}
}
