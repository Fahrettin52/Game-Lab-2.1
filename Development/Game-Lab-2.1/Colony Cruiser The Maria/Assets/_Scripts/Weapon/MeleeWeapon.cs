using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleeWeapon : MonoBehaviour {
	private GameObject player;
	private Animator myAnimator;
	public AnimatorStateInfo myStateInfo;
	private int idleHash = Animator.StringToHash("FistIdlePH");
	private float hitTime;
	public float hitRate;
	public Sprite myWeaponIcon;

	void OnEnable(){
		myAnimator = GetComponent<Animator> ();
		player = GameObject.FindWithTag ("Player");
		player.GetComponent<WeaponManager> ().weaponIcon.sprite = myWeaponIcon;
		FillDelegate ();
	}

	public void FillDelegate(){
		player.GetComponent<WeaponManager> ().shootDelegate = null;
		player.GetComponent<WeaponManager> ().aimDelegate = null;
		player.GetComponent<WeaponManager> ().ammoSwitchDelegate = null;
		player.GetComponent<WeaponManager> ().shootDelegate = Melee;
		player.GetComponent<WeaponManager> ().quickMeleeDelegate = QuickMelee;
	}

	public void Melee(){
		myStateInfo = myAnimator.GetCurrentAnimatorStateInfo (0);
		if (Input.GetButtonDown ("Fire1")) {
			if(Time.time > hitTime){
				hitTime = Time.time + hitRate;
				if (myStateInfo.shortNameHash == idleHash) {
					myAnimator.SetTrigger ("Melee");
				}
			}
		}
	}

	public void QuickMelee(){
		myStateInfo = myAnimator.GetCurrentAnimatorStateInfo (0);
		if (Input.GetButtonDown ("Fire2")) {
			if (Time.time > hitTime) {
				if (myStateInfo.shortNameHash == idleHash) {
					myAnimator.SetTrigger ("QuickMelee");
				}
				hitTime = Time.time + hitRate;
			}
		}
	}

	public void ActivateCollider(){
		GetComponent<BoxCollider> ().enabled = true;
	}

	public void DeactivateCollider(){
		GetComponent<BoxCollider> ().enabled = false;
	}

	public void OnTriggerEnter(Collider col){
		print ("Collided!");
		switch (col.transform.tag) {
		case "Head":
			print ("Hit the Head, damage x3");
			Destroy (col.transform.gameObject);
			break;
		case "Limbs":
			print ("Hit Limb, damage x1");
			Destroy (col.transform.gameObject);
			break;
		case "Body":
			print ("Hit the Body, damage x2");
			//Destroyen van de parent voorbeeld:
			GameObject parento = col.transform.parent.gameObject;
			Destroy (parento);
			break;
		default:
			print ("NOT AN ENEMY!");
			break;
		}
	}
}
