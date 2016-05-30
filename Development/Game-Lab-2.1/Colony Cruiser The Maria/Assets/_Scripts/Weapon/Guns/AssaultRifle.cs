using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AssaultRifle : AbstractWeapon {

	public int normalAmmo;
	public int flechetteAmmo;
	public int normalTotalAmmo;
	public int flechetteTotalAmmo;
	public int normalMagSize;
	public int flechetteMagSize;
	public int maxNormalAmmo;
	public int maxFlechetteAmmo;
	public int damageNormal;
	public int damageFlechette;

	public float normalShotCount;
	public float flechetteShotCount;
	public float NormalDirValueX;
	public float NormalDirValueY;
	public float flechetteDirValueX;
	public float flechetteirValueY;
	public float fireSpeed;
	public float waitTilNextFire; 

	public bool mayFlechette;
	public bool mayNormal;

	public GameObject bulletHole;
	public GameObject flechette;
	public GameObject ammoHUD;
	public GameObject bulletHUD;


	public void Update (){
		cameroTransform = camero.transform;
	}

	public override void Shooting(){
		if (Input.GetButtonDown ("Reload") || loadedAmmo == 0) {
			if (loadedAmmo < magSize) {
				Reloading ();
			}
		}
		if (Input.GetButton ("Fire1") && loadedAmmo > 0) {
			if (waitTilNextFire <= 0) {
				Vector3 playerPos = player.transform.position;
				shootDir = cameroTransform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
				Debug.DrawRay (cameroTransform.position, shootDir * 5000, Color.blue, 3);
				waitTilNextFire = 1;
				if (Physics.Raycast (cameroTransform.position, shootDir, out rayHit, rayDis)) {
					DistanceChecker (playerPos);
					AmmoEffect(rayHit.transform.gameObject);
				} 
				else {
					print ("MISSED");
				}
				AmmoRemove ();
			}
		}
		UIChecker ();
		waitTilNextFire -= Time.deltaTime * fireSpeed;
	}

	public override void Reloading(){
		switch (curAmmoType) {
		case 0:
			if (normalTotalAmmo > 0) {
				normalAmmo = normalMagSize;
				normalTotalAmmo -= normalMagSize;
				loadedAmmo = normalAmmo;
				magSize = normalMagSize;
				curAmmoTypeText = normalTotalAmmo;
			} 
			else {
				print ("Out of Normal Magazines!");
			}
			break;
		case 1:
			if (flechetteTotalAmmo > 0) {
				flechetteAmmo = flechetteMagSize;
				flechetteTotalAmmo -= flechetteMagSize;
				loadedAmmo = flechetteAmmo;
				magSize = flechetteMagSize;
				curAmmoTypeText = flechetteTotalAmmo;
			} 
			else {
				print ("Out of Normal Magazines!");
			}
			break;
		}
	}

	public override void OnEnable(){
		ammoHUD.SetActive (true);
		bulletHUD.SetActive (true);
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		weaponManager = player.GetComponent<WeaponManager> ();
		weaponManager.weaponIcon.sprite = myWeaponIcon;
		switch (curAmmoType) {
		case 0:
			magSize = normalMagSize;
			break;
		case 1:
			magSize = flechetteMagSize;
			break;
		}
		FillDelegate ();
		AmmoCycle ();
		UIChecker ();
	}

	public override bool Aiming(bool aim){
		if (!aim) {
			if (camero.GetComponent<Camera> ().fieldOfView < maxFieldOfView) {
				camero.GetComponent<Camera> ().fieldOfView += zoomSpeed * Time.deltaTime;
				GetComponent<Animator>().SetBool("aimAnimation", false);
			}
		}
		else {
			if (camero.GetComponent<Camera> ().fieldOfView > minFieldOfView) {
				camero.GetComponent<Camera> ().fieldOfView -= zoomSpeed * Time.deltaTime;
				GetComponent<Animator>().SetBool("aimAnimation", true);
			}
		}
		return aim;
	}

	public override void AmmoSwitch(){
		if (curAmmoType < maxAmmoType) {
			curAmmoType++;
		}
		if(curAmmoType == maxAmmoType){
			curAmmoType = 0;
		}
		AmmoCycle ();
	}

	public override void AmmoRemove(){
		switch (curAmmoType){
		case 0:
			normalAmmo--;
			loadedAmmo = normalAmmo;
			break;
		case 1:
			flechetteAmmo--;
			loadedAmmo = flechetteAmmo;
			break;
		}
	}

	public override int AmmoAdd(int ammoToAdd){
		switch (ammoToAdd) {
		case 0:
			if (normalTotalAmmo < maxNormalAmmo) {
				normalTotalAmmo += normalMagSize;
				if(normalTotalAmmo > maxNormalAmmo){
					normalTotalAmmo = maxNormalAmmo;
				}
			}
			break;
		case 1:
			if (flechetteTotalAmmo < maxFlechetteAmmo) {
				flechetteTotalAmmo += flechetteMagSize;
				if(flechetteTotalAmmo > maxFlechetteAmmo){
					flechetteTotalAmmo = maxFlechetteAmmo;
				}
			}
			break;
		}
		switch(curAmmoType){
		case 0:
			curAmmoTypeText = normalTotalAmmo;
			break;
		case 1: 
			curAmmoTypeText = flechetteTotalAmmo;
			break;
		}
		return ammoToAdd;
	}

	public override void AmmoEffect(GameObject target){
		Transform targetTransform = target.transform;
		switch (targetTransform.tag) {
		case "Head":
			print ("Hit the Head, damage x3");
			Destroy (targetTransform.gameObject);
			break;
		case "Limbs":
			print ("Hit Limb, damage x1");
			Destroy (targetTransform.gameObject);
			break;
		case "Body":
			print ("Hit the Body, damage x2");
			//Destroyen van de parent voorbeeld:
			GameObject parento = targetTransform.parent.gameObject;
			Destroy (parento);
			break;
		case "Barrel":
			targetTransform.GetComponent<SmokeBarrel> ().Explode ();
			break;
		default:
			print ("NOT AN ENEMY!");
			break;
		}
	}

	public override void FillDelegate (){
		weaponManager.shootDelegate = null;
		weaponManager.aimDelegate = null;
		weaponManager.ammoSwitchDelegate = null;
		weaponManager.quickMeleeDelegate = null;
		weaponManager.shootDelegate = Shooting;
		weaponManager.aimDelegate = Aiming;
		weaponManager.ammoSwitchDelegate = AmmoSwitch;
		weaponManager.quickMeleeDelegate = QuickMelee;
	}

	public override void DistanceChecker (Vector3 savedPos){
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

	public override IEnumerator ImpactDelay (float impactTime, float damage){
		yield return new WaitForSeconds(impactTime);
		switch (rayHit.transform.tag) {
		case "Head":
			recticale.enabled = false;
			recticaleHit.enabled = true;	
			print ("Hit the Head, damage x3");
			Destroy (rayHit.transform.gameObject);
			break;
		case "Limbs":
			print ("Hit Limb, damage x1");
			Destroy (rayHit.transform.gameObject);
			break;
		case "Body":
			print ("Hit the Body, damage x2");
			//Destroyen van de parent voorbeeld:
			GameObject parento = rayHit.transform.parent.gameObject;
			Destroy (parento);
			break;
		default:
			print ("NOT AN ENEMY!");
			if (mayNormal == true) {
				Instantiate (bulletHole, rayHit.point, Quaternion.FromToRotation (Vector3.up, rayHit.normal));
			}
			if (mayFlechette == true){
				Instantiate (flechette, rayHit.point, Quaternion.FromToRotation (Vector3.forward, rayHit.normal));
			}
			break;
		}
	}

	public override void QuickMelee (){
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

	public override void AmmoCycle(){
		switch (curAmmoType){
		case 0:
			mayNormal = true;
			mayFlechette = false;
			loadedAmmo = normalAmmo;
			shotCount = normalShotCount;
			shootDirValueX = NormalDirValueX;
			shootDirValueY = NormalDirValueY;
			curAmmoTypeText = normalTotalAmmo;
			break;
		case 1:
			mayFlechette = true;
			mayNormal = false;
			loadedAmmo = flechetteAmmo;
			shotCount = flechetteShotCount;
			shootDirValueX = flechetteDirValueX;
			shootDirValueY = flechetteirValueY;
			curAmmoTypeText = flechetteTotalAmmo;
			break;
		}
	}
}