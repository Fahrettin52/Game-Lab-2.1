using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public enum ViewType { Normal, Cover, Dead};

    public ViewType myView;

    public float camRotationX;
	public float camRotationY;
	public float mouseSensetivity;
	public float rotationLimit;
    public float rotationCover;
	public GameObject camero;

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
        //camRotationX += Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        //camRotationY -= Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;
        camRotationX = Mathf.Clamp(camRotationX, -rotationCover, rotationCover);
        camRotationY = Mathf.Clamp(camRotationY, -rotationLimit, rotationLimit);
        camero.GetComponent<Transform>().localRotation = Quaternion.Euler(camRotationY, camRotationX, 0);
    }
}
