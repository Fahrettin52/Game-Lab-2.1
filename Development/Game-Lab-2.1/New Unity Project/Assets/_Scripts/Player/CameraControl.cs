using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	private float camRotationX;
	private float camRotationY;
	public float mouseSensetivity;
	public float rotationLimit;
	public GameObject camero;

	void Start(){
		camero = GameObject.Find ("Main Camera");
	}

	void Update(){
		CamRotation ();
	}

	public void CamRotation(){
		camRotationX += Input.GetAxis ("Mouse X") * mouseSensetivity * Time.deltaTime;
		camRotationY -= Input.GetAxis ("Mouse Y") * mouseSensetivity * Time.deltaTime;
		camRotationY = Mathf.Clamp (camRotationY, -rotationLimit, rotationLimit);
		transform.localRotation = Quaternion.Euler(0, camRotationX, 0);
		camero.GetComponent<Transform>().localRotation = Quaternion.Euler(camRotationY, 0, 0);
	}
}
