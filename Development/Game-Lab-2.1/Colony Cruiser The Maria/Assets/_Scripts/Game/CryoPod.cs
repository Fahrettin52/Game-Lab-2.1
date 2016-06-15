using UnityEngine;
using System.Collections;

public class CryoPod : MonoBehaviour {

	public int openCryoPod;
	public int setCollidersOn;
	public int playAwakeAnimation;
	public int camerView;
	public int disableAnimation;

	public GameObject cryoHatch;
	public GameObject player;
	public GameObject camero;

	public Transform survivor;

	public Collider[] cryoHatchC;

	public GameObject smoke;

	void Start () {
//		lastRotation = player.GetComponent<Transform> ().transform.rotation;
		cryoHatchC = cryoHatch.GetComponents<BoxCollider>();
		StartCoroutine (PlayAnimation());
	}

	IEnumerator PlayAnimation(){
		yield return new WaitForSeconds (openCryoPod);
		GetComponentInParent<Animator>().SetTrigger ("Open");
		yield return new WaitForSeconds (0.5f);
		GetComponent<AudioSource> ().Play();
		camero.GetComponent<BlurFade>().mayBlur = true;
		smoke.SetActive (true);
		yield return new WaitForSeconds (setCollidersOn);
		foreach (Collider c in cryoHatchC) {
			c.enabled = true;
		}
		yield return new WaitForSeconds (playAwakeAnimation);
		player.GetComponentInChildren<Animator> ().SetTrigger ("Awake");
		yield return new WaitForSeconds (camerView);
		player.GetComponentInChildren<CameraControl> ().myView = CameraControl.ViewType.Normal; 
		yield return new WaitForSeconds (disableAnimation);
		player.GetComponentInChildren<Animator> ().enabled = false;
		player.transform.rotation = Quaternion.Euler (0, 90, 0);
		player.GetComponentInChildren<CameraControl> ().myView = CameraControl.ViewType.Dead;
		camero.transform.LookAt (survivor);
	}
}
