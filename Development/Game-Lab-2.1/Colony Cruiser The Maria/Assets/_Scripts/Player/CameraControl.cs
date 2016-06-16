using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public enum ViewType { Normal, Cover, Dead, Cryo};

    public ViewType myView;

    public float camRotationX;
	public float camRotationY;
	public float mouseSensetivity;
	public float rotationLimit;
    public float rotationCover;
	public GameObject camero;

	public float cryoRoatationLimitY;
	public float cryoRoatationLimitX;

	void Start(){
		camero = GameObject.Find ("Main Camera");
	}

	void Update() {
        switch (myView) {
        case ViewType.Normal:
            RotationChecker();
            break;
        case ViewType.Cover:
            CoverRotation();
            break;
		case ViewType.Cryo:
			CryoStartScene();
			break;
        }
	}

	public void RotationChecker(){
		camRotationX += Input.GetAxis ("Mouse X") * mouseSensetivity * Time.deltaTime;
		camRotationY -= Input.GetAxis ("Mouse Y") * mouseSensetivity * Time.deltaTime;
		camRotationY = Mathf.Clamp (camRotationY, -rotationLimit, rotationLimit);
		transform.localRotation = Quaternion.Euler(0, camRotationX, 0);
		camero.GetComponent<Transform>().localRotation = Quaternion.Euler(camRotationY, 0, 0);
	}

    public void CoverRotation() {
        camRotationX += Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        camRotationY -= Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;
        camRotationX = Mathf.Clamp(camRotationX, -rotationCover, rotationCover);
        camRotationY = Mathf.Clamp(camRotationY, -rotationLimit, rotationLimit);
        camero.GetComponent<Transform>().localRotation = Quaternion.Euler(camRotationY, camRotationX, 0);
    }

	public void CryoStartScene() {
		camRotationX += Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
		camRotationY -= Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;
		camRotationX = Mathf.Clamp(camRotationX, -cryoRoatationLimitX, cryoRoatationLimitX);
		camRotationY = Mathf.Clamp(camRotationY, -cryoRoatationLimitY, cryoRoatationLimitY);
		//camero.GetComponent<Transform>().localRotation = Quaternion.Euler(camRotationY, camRotationX, 0);
	}
}
