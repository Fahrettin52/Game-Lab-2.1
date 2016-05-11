using UnityEngine;
using System.Collections;

public class Pistol : AbstractWeapon {
	public float maxAmmo;
	public float curAmmoInMag;
	public float curAmmoHeld;

	public override void OnEnable(){
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		weaponManager = player.GetComponent<WeaponManager> ();
		weaponManager.weaponIcon.sprite = myWeaponIcon;
		FillDelegate ();
	}

	public override void FillDelegate(){
		weaponManager.shootDelegate = null;
		weaponManager.aimDelegate = null;
		weaponManager.ammoSwitchDelegate = null;
		weaponManager.quickMeleeDelegate = null;
		weaponManager.shootDelegate = Shooting;
		weaponManager.aimDelegate = Aiming;
		weaponManager.ammoSwitchDelegate = AmmoSwitch;
		weaponManager.quickMeleeDelegate = QuickMelee;
	}

	public override void Shooting(){
		if (Input.GetButtonDown ("Fire1")) {
			cameroTransform = camero.transform;
			shootDir = cameroTransform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
			Debug.DrawRay (cameroTransform.position, shootDir * 5000, Color.blue, 3);
			if (Physics.Raycast (cameroTransform.position, shootDir, out rayHit, rayDis)) {
				DistanceChecker (player.transform.position);		
			} else {
				print ("MISSED");
			}
		}
		if (Input.GetButtonDown ("Reload") || loadedAmmo == 0) {
			if (loadedAmmo < magSize) {
				print ("Reloading");
				Reloading ();
			}
		}
		UIChecker ();
	}

	public override void DistanceChecker(Vector3 savedPos){
		float impactDistance = Vector3.Distance(savedPos, rayHit.transform.position);
		float timeToImpact = impactDistance / projectileSpeed;
		float impactDamage = new float ();
		if (impactDistance < effectiveRange) {
			impactDamage = projectileDamage - (effectiveRange - impactDistance);
		}
		else {
			impactDamage = 0;
		}
		StartCoroutine(ImpactDelay(timeToImpact, impactDamage));
	}

	public override IEnumerator ImpactDelay(float impactTime, float damage){
		Transform rayHitTransform = rayHit.transform;
		switch (rayHitTransform.tag) {
		case "Head":
			print ("Hit the Head, damage x5");
			GameObject parento = rayHitTransform.parent.gameObject;
            Destroy(parento);
			break;
		case "Limbs":
			print ("Hit Limb, damage x2");
			Destroy (rayHitTransform.gameObject);
			break;
		case "Body":
			print ("Hit the Body, damage x3");
            //Destroyen van de parent voorbeeld:
			Destroy(rayHitTransform.gameObject);
            break;
		default:
			print ("NOT AN ENEMY!");
			break;
		}
		yield return new WaitForSeconds(impactTime);
	}

	public override bool Aiming(bool aim){
		Camera cameroCamera = camero.GetComponent<Camera> ();
		if (!aim) {
			if (cameroCamera.fieldOfView < maxFieldOfView) {
				cameroCamera.fieldOfView += zoomSpeed * Time.deltaTime;
                GetComponent<Animator>().SetBool("aimAnimation", false);
			}
		}
		else {
			if (cameroCamera.fieldOfView > minFieldOfView) {
				cameroCamera.fieldOfView -= zoomSpeed * Time.deltaTime;
                GetComponent<Animator>().SetBool("aimAnimation", true);
            }
		}
		return aim;
	}

	public override void AmmoRemove(){
		if (curAmmoInMag > 0) {
			curAmmoInMag--;
		}
		else {
			Reloading ();
		}
	}

	public override void AmmoAdd(){
		curAmmoHeld += magSize;
	}

	public override void AmmoEffect(GameObject hit){

	}

    public override void Reloading() {
		float reloadAmmount = magSize - curAmmoInMag;
		for(int i = 0; reloadAmmount > 0; i++){
			if (curAmmoHeld > 0) {
				curAmmoInMag++;
				curAmmoHeld--;
				reloadAmmount--;
			}
		}
    }

	public override void QuickMelee(){
//		myStateInfo = myAnimator.GetCurrentAnimatorStateInfo (0);
//		if (Input.GetButtonDown ("Fire2")) {
//			if (Time.time > hitTime) {
//				if (myStateInfo.shortNameHash == idleHash) {
//					myAnimator.SetTrigger ("QuickMelee");
//				}
//				hitTime = Time.time + hitRate;
//			}
//		}
	}

	public override void UIChecker(){
		weaponManager.ammoCountHolder.text = (loadedAmmo + "/" + curAmmoTypeText);
	}

	public override void AmmoSwitch(){
		print ("switched");
		if (curAmmoType < maxAmmoType) {
			curAmmoType++;
		}
		if(curAmmoType == maxAmmoType){
			curAmmoType = 0;
		}
		AmmoCycle ();
	}

	public override void AmmoCycle(){
		print ("Chosen ammo");
		switch (curAmmoType){
		case 0:
			print ("1");
//			loadedAmmo = birdAmmo;
//			shotCount = birdShotCount;
//			shootDirValueX = birdDirValueX;
//			shootDirValueY = birdDirValueY;
//			curAmmoTypeText = birdTotalAmmo;
			break;
		case 1:
			print ("2");
//			loadedAmmo = buckAmmo;
//			shotCount = buckShotCount;
//			shootDirValueX = buckDirValueX;
//			shootDirValueY = buckDirValueY;
//			curAmmoTypeText = buckTotalAmmo;
			break;
		}
	}
}
