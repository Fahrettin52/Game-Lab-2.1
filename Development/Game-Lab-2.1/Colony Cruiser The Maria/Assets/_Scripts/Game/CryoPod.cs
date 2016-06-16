using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CryoPod : MonoBehaviour {

	public int startAwake;
	public int openCryoPod;
	public int setCollidersOn;
	public int activateHUD;
	public int activateHands;

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
		smoke.SetActive (true);
		yield return new WaitForSeconds (setCollidersOn);
		foreach (Collider c in cryoHatchC) {
			c.enabled = true;
		}
		yield return new WaitForSeconds (2f);
		player.GetComponentInChildren<Animator> ().SetTrigger ("Awake");
		survivor.GetComponent<SurvivorManager> ().Startconvo ();
		camero.GetComponent<BlurFade>().mayBlur = true;
		yield return new WaitForSeconds (activateHUD);
		player.GetComponentInChildren<CameraControl> ().myView = CameraControl.ViewType.Dead;
		yield return new WaitForSeconds (activateHUD);
		player.GetComponentInChildren<Animator> ().speed = 0;
		hUD.SetActive (true);

//		player.transform.rotation = Quaternion.Euler (0, 90, 0);
//		player.GetComponentInChildren<CameraControl> ().myView = CameraControl.ViewType.Dead;
//		camero.transform.LookAt (survivor);
	}

	public void White(){
		white.alpha -= Time.deltaTime *0.1f;
	}
}
