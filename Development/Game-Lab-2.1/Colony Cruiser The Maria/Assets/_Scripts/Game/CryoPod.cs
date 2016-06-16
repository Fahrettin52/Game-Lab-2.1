using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CryoPod : MonoBehaviour {

	public int startAwake;
	public int openCryoPod;
	public int setCollidersOn;

	public GameObject cryoHatch;
	public GameObject player;
	public GameObject camero;

	public Transform survivor;

	public Collider[] cryoHatchC;

	public GameObject smoke;

	public CanvasGroup white;

	public bool mayWhite;

	public AudioSource[] audioSource;

	public AudioClip oxygen;
	public AudioClip hydraulic;
	public AudioClip wakingShock;
	public AudioClip awakeFallout;

	public GameObject hUD;

	void Start () {
		cryoHatchC = cryoHatch.GetComponents<BoxCollider>();
		StartCoroutine (PlayAnimation());
	}

	public void Update(){
		if (mayWhite) {
			White ();
		}
	}

	IEnumerator PlayAnimation(){
		audioSource[0].clip = awakeFallout;
		audioSource[0].Play();
		yield return new WaitForSeconds (startAwake);

		mayWhite = true;
		yield return new WaitForSeconds (openCryoPod);

		GetComponentInParent<Animator>().SetTrigger ("Open");
		audioSource[1].clip = hydraulic;
		audioSource[1].Play();
		camero.GetComponent<BlurFade>().mayBlur = true;
		smoke.SetActive (true);

		yield return new WaitForSeconds (setCollidersOn);
		foreach (Collider c in cryoHatchC) {
			c.enabled = true;
		}
		hUD.SetActive (true);
		survivor.GetComponent<SurvivorManager> ().Startconvo ();
//		yield return new WaitForSeconds (playAwakeAnimation);
//		player.GetComponentInChildren<Animator> ().SetTrigger ("Awake");
//		yield return new WaitForSeconds (camerView);
//		player.GetComponentInChildren<CameraControl> ().myView = CameraControl.ViewType.Normal; 
//		yield return new WaitForSeconds (disableAnimation);
//		player.GetComponentInChildren<Animator> ().enabled = false;
//		player.transform.rotation = Quaternion.Euler (0, 90, 0);
//		player.GetComponentInChildren<CameraControl> ().myView = CameraControl.ViewType.Dead;
//		camero.transform.LookAt (survivor);
	}

	public void White(){
		white.alpha -= Time.deltaTime *0.1f;
	}
}
