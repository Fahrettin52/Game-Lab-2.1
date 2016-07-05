using UnityEngine;
using System.Collections;

public class Pistol : AbstractWeapon {
	public int normalAmmo;
	public int incindiaryAmmo;
	public int normalTotalAmmo;
	public int incindiaryTotalAmmo;
	public float normalDirValueX;
	public float normalDirValueY;
	public int maxNormalAmmo;
	public int maxIncindiaryAmmo;
	public int normalMagSize;
	public int incindiaryMagSize;
	public float incindiaryDirValueX;
	public float incindiaryDirValueY;
	public GameObject soundManager;
	public GameObject shotLight;
	public GameObject bulletIcon;
	public ParticleSystem pistolParticle;
	public RectTransform normalBullet;
	public RectTransform incidiaryBullet;

	public override void OnEnable(){
		bulletIcon.SetActive (true);
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		myAnimator = GetComponentInParent<Animator> ();
		WeaponChecker ();
		weaponManager = player.GetComponent<WeaponManager> ();
		weaponManager.weaponIcon.sprite = myWeaponIcon;
		switch (curAmmoType) {
		case 0:
			magSize = normalMagSize;
			break;
		case 1:
			magSize = incindiaryMagSize;
			break;
		}
		FillDelegate ();
		AmmoCycle ();
		UIChecker ();
	}

	public override void WeaponChecker(){
		myAnimator.SetBool ("SwordEquip", false);
		myAnimator.SetBool ("RevolverEquip", true);
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
			if(loadedAmmo > 0 && soundManager.GetComponent<SoundManager> ().myAudioSource[4].isPlaying == false){
				StartCoroutine (PlayTheParticle());
				GetComponentInParent<Animator> ().SetTrigger ("RevolverShooting");
				soundManager.GetComponent<SoundManager> ().RevolverShot ();
				cameroTransform = camero.transform;
				shootDir = cameroTransform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
				Debug.DrawRay (cameroTransform.position, shootDir * 5000, Color.blue, 3);
				if (Physics.Raycast (cameroTransform.position, shootDir, out rayHit, rayDis)) {
					DistanceChecker (player.transform.position);		
				} 
				else {
					print ("MISSED");
				}
				AmmoRemove ();
			}
		}
		if (Input.GetButtonDown ("Reload") || loadedAmmo == 0) {
			if (loadedAmmo < magSize) {
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
		yield return new WaitForSeconds(impactTime);
		Transform targetTransform = rayHit.transform;
		switch (targetTransform.tag) {
		case "Head":
			Destroy (targetTransform.gameObject);
			break;
		case "Critical":
			print ("Critcal " + damage);
			targetTransform.GetComponentInParent<AbstractEnemy> ().RecieveCriticalDamage (damage);
			break;
		case "Body":
			print ("Body " + damage);
			targetTransform.GetComponentInParent<AbstractEnemy> ().RecieveDamage (damage);
			break;
		case "Barrel":
			targetTransform.GetComponent<SmokeBarrel> ().Explode ();
			break;
		default:
			break;
		}
	}

	public override bool Aiming(bool aim){
		Camera cameroCamera = camero.GetComponent<Camera> ();
		if (!aim) {
			if (cameroCamera.fieldOfView < maxFieldOfView) {
				cameroCamera.fieldOfView += zoomSpeed * Time.deltaTime;
				//GetComponentInParent<Animator>().SetBool("aimAnimation", false);
			}
		}
		else {
			if (cameroCamera.fieldOfView > minFieldOfView) {
				cameroCamera.fieldOfView -= zoomSpeed * Time.deltaTime;
				//GetComponentInParent<Animator>().SetBool("aimAnimation", true);
            }
		}
		return aim;
	}

	public override void AmmoRemove(){
		switch (curAmmoType){
		case 0:
			normalAmmo--;
			loadedAmmo = normalAmmo;
			break;
		case 1:
			incindiaryAmmo--;
			loadedAmmo = incindiaryAmmo;
			break;
		}
	}

	public override int AmmoAdd(int ammoToAdd){
		switch (ammoToAdd) {
		case 0:
			if (normalTotalAmmo < maxNormalAmmo) {
				normalTotalAmmo += 14;
				if(normalTotalAmmo > maxNormalAmmo){
					normalTotalAmmo = maxNormalAmmo;
				}
			}
			break;
		case 1:
			if (incindiaryTotalAmmo < maxIncindiaryAmmo) {
				incindiaryTotalAmmo += 14;
				if(incindiaryTotalAmmo > maxIncindiaryAmmo){
					incindiaryTotalAmmo = maxIncindiaryAmmo;
				}
			}
			break;
		}
		switch(ammoToAdd){
		case 0:
			curAmmoTypeText = normalTotalAmmo;
			break;
		case 1: 
			curAmmoTypeText = incindiaryTotalAmmo;
			break;
		}
		return ammoToAdd;
	}

	public override void AmmoEffect(GameObject hit){

	}

    public override void Reloading() {
		switch (curAmmoType) {
		case 0:
			if (normalTotalAmmo > 0) {
				GetComponentInParent<Animator> ().SetTrigger ("RevolverReload");
				soundManager.GetComponent<SoundManager> ().RevolverReload ();
				int leftOverAmmo = normalMagSize - normalAmmo;
				for (int i = 0; leftOverAmmo > 0; i++) {
					normalTotalAmmo--;
					normalAmmo++;
					leftOverAmmo--;
					if (normalTotalAmmo < 1) {
						break;
					}
				}
				loadedAmmo = normalAmmo;
				curAmmoTypeText = normalTotalAmmo;
			}
			else {
				print ("Out of normal Magazines!");
			}
			break;
		case 1:
			if (incindiaryTotalAmmo > 0) {
				GetComponentInParent<Animator> ().SetTrigger ("RevolverReload");
				soundManager.GetComponent<SoundManager> ().RevolverReload ();
				int leftOverAmmo = incindiaryMagSize - incindiaryAmmo;
				for (int i = 0; leftOverAmmo > 0; i++) {
					incindiaryTotalAmmo--;
					incindiaryAmmo++;
					leftOverAmmo--;
					if (incindiaryTotalAmmo < 1) {
						break;
					}
				}
				loadedAmmo = incindiaryAmmo;
				curAmmoTypeText = incindiaryTotalAmmo;
			}
			else {
				print ("Out of incindiary Magazines!");
			}
			break;
		}
    }

	public override void QuickMelee(){
		myStateInfo = myAnimator.GetCurrentAnimatorStateInfo (0);
		if (Input.GetButtonDown ("Fire2")) {
			if (Time.time > hitTime) {
				if (myStateInfo.shortNameHash == Animator.StringToHash(idleHash)) {
					myAnimator.SetTrigger ("QuickMelee");
				}
				hitTime = Time.time + hitRate;
			}
		}
	}

	public override void UIChecker(){
		weaponManager.ammoCountHolder.text = (loadedAmmo + "/" + curAmmoTypeText);
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

	public override void AmmoCycle(){
		switch (curAmmoType){
		case 0:
			Vector2 tmpPos = new Vector2 (normalBullet.anchoredPosition.x, normalBullet.anchoredPosition.y);
			normalBullet.anchoredPosition = incidiaryBullet.anchoredPosition;
			incidiaryBullet.anchoredPosition = tmpPos;
			loadedAmmo = normalAmmo;
			shootDirValueX = normalDirValueX;
			shootDirValueY = normalDirValueY;
			curAmmoTypeText = normalTotalAmmo;
			break;
		case 1:
			Vector2 tmpPos2 = new Vector2 (incidiaryBullet.anchoredPosition.x, incidiaryBullet.anchoredPosition.y);
			incidiaryBullet.anchoredPosition = normalBullet.anchoredPosition;
			normalBullet.anchoredPosition = tmpPos2;
			loadedAmmo = incindiaryAmmo;
			shootDirValueX = incindiaryDirValueX;
			shootDirValueY = incindiaryDirValueY;
			curAmmoTypeText = incindiaryTotalAmmo;
			break;
		}
	}

	public IEnumerator PlayTheParticle(){
		pistolParticle.Play ();
		shotLight.SetActive (true);
		yield return new WaitForSeconds (0.05f);
		shotLight.SetActive (false);
		pistolParticle.Stop ();
	}
}
