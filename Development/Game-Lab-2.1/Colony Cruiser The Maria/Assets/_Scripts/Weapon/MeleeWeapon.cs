using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {
	private GameObject player;
	private Animator myAnimator;

	void Start(){
		myAnimator = GetComponent<Animator> ();
		player = GameObject.FindWithTag ("Player");
		FillDelegate ();
	}

	void OnEnabled(){
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
		myAnimator.SetTrigger("Melee");
	}

	public void QuickMelee(){
		myAnimator.SetTrigger("QuickMelee");
	}

	public void ActivateCollider(){
		GetComponent<BoxCollider> ().enabled = true;
	}

	public void DeactivateCollider(){
		GetComponent<BoxCollider> ().enabled = false;
	}

	public void OnCollisionEnter(Collision col){
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
