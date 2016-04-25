using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AssaultRifle : AbstractWeapon {

	public int normalAmmo;
	public int flechetteAmmo;
	public int normalTotalAmmo;
	public int flechetteTotalAmmo;
	public int normalMag;
	public int flachetteMag;
	public int fullNormalAmmo;
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

	public GameObject bulletHole;
	public GameObject bulletStart;
	public GameObject flechette;
	public GameObject ammoHUD;
	public GameObject bulletHUD;


	public void Update (){
		GetComponent<LineRenderer>().SetPosition (0, bulletStart.transform.position);
		GetComponent<LineRenderer>().SetPosition (1, camero.transform.forward * 10 + camero.transform.position);
	}

	public override void Shooting(){
		if (Input.GetButtonDown ("Reload") || loadedAmmo == 0) {
			if (loadedAmmo < magSize) {
				print ("Reloading");
				Reloading ();
			}
		}
		if (Input.GetButton ("Fire1") && loadedAmmo > 0) {
			if (waitTilNextFire <= 0) {
				Vector3 playerPos = player.transform.position;
				shootDir = camero.transform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
				Debug.DrawRay (camero.transform.position, shootDir * 5000, Color.blue, 3);
				waitTilNextFire = 1;
				if (Physics.Raycast (camero.transform.position, shootDir, out rayHit, rayDis)) {
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
		int curLoadedAmmo = curAmmoType;
		switch (curLoadedAmmo) {
		case 0:
			if (normalTotalAmmo > 0) {
				normalAmmo = magSize;
				normalTotalAmmo -= magSize;
				loadedAmmo = normalAmmo;
				curAmmoTypeText = normalTotalAmmo;
			} 
			else {
				print ("Out of Normal Magazines!");
			}
			break;
		case 1:
			if (flechetteTotalAmmo > 0) {
				int leftoverAmmo = flachetteMag - flechetteAmmo;
				for (int i = 0; leftoverAmmo > 0; i++) {
					flechetteTotalAmmo--;
					flechetteAmmo++;
					leftoverAmmo--;
					if (flechetteTotalAmmo < 1) {
						break;
					}
				}
				loadedAmmo = flechetteAmmo;
				curAmmoTypeText = flechetteTotalAmmo;
			} else {
				print ("Out of Flechette Magazines!");
			}
			break;
		}
	}

	public override void OnEnable(){
		ammoHUD.SetActive (true);
		bulletHUD.SetActive (true);
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		player.GetComponent<WeaponManager> ().weaponIcon.sprite = myWeaponIcon;
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

	public void AmmoSwitch(){
		if (curAmmoType < maxAmmoType) {
			curAmmoType++;
		}
		if(curAmmoType == maxAmmoType){
			curAmmoType = 0;
		}
		AmmoCycle ();
	}

	public override void AmmoRemove(){
		int curLoadedAmmo = curAmmoType;
		switch (curLoadedAmmo){
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

	public override void AmmoAdd(){
		normalTotalAmmo += magSize;
		curAmmoTypeText = normalTotalAmmo;
	}

	public override void AmmoEffect(GameObject target){
		switch (target.GetComponent<Transform>().tag) {
		case "EnemyLiveStock":
			print ("EnemyLiveStock");
			break;
		case "EnemyAI":
			print ("EnemyAI");
			break;
		default:
			print ("NOT AN ENEMY!");
			break;
		}
	}

	public override void FillDelegate (){
		player.GetComponent<WeaponManager> ().shootDelegate = null;
		player.GetComponent<WeaponManager> ().aimDelegate = null;
		player.GetComponent<WeaponManager> ().ammoSwitchDelegate = null;
		player.GetComponent<WeaponManager> ().quickMeleeDelegate = null;
		player.GetComponent<WeaponManager> ().shootDelegate = Shooting;
		player.GetComponent<WeaponManager> ().aimDelegate = Aiming;
		player.GetComponent<WeaponManager> ().ammoSwitchDelegate = AmmoSwitch;
		player.GetComponent<WeaponManager> ().quickMeleeDelegate = QuickMelee;
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
			Instantiate (bulletHole, rayHit.point, Quaternion.FromToRotation (Vector3.up, rayHit.normal));
			if (mayFlechette == true){
				Instantiate (flechette, rayHit.point, Quaternion.FromToRotation (Vector3.up, rayHit.normal));
			}
			break;
		}
	}

	public void FlechetteShooting(){
		if (Input.GetButton("Fire 1")){
			
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
		player.GetComponent<WeaponManager> ().ammoCountHolder.text = (loadedAmmo + "/" + curAmmoTypeText);
	}

	public void AmmoCycle(){
		switch (curAmmoType){
		case 0:
			mayFlechette = false;
			loadedAmmo = normalAmmo;
			shotCount = normalShotCount;
			shootDirValueX = NormalDirValueX;
			shootDirValueY = NormalDirValueY;
			curAmmoTypeText = normalTotalAmmo;
			break;
		case 1:
			mayFlechette = true;
			loadedAmmo = flechetteAmmo;
			shotCount = flechetteShotCount;
			shootDirValueX = flechetteDirValueX;
			shootDirValueY = flechetteirValueY;
			curAmmoTypeText = flechetteTotalAmmo;
			break;
		}
	}
}