using UnityEngine;
using System.Collections;

public class Shotgun : AbstractWeapon {
	public float birdShotCount;
	public float buckShotCount;
	public float slugShotCount;

	public float birdDirValueX;
	public float birdDirValueY;
	public float buckDirValueX;
	public float buckDirValueY;
	public float slugDirValueX;
	public float slugDirValueY;

	public int shotsDividerCount;
	public int maxBirdAmmo;
	public int maxBuckAmmo;
	public int maxSlugAmmo;
	public int birdTotalAmmo;
	public int buckTotalAmmo;
	public int slugTotalAmmo;
	public int birdAmmo;
	public int buckAmmo;
	public int slugAmmo;
	public int birdMagSize;
	public int buckMagSize;
	public int slugMagSize;

	public override void OnEnable(){
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		weaponManager = player.GetComponent<WeaponManager> ();
		weaponManager.weaponIcon.sprite = myWeaponIcon;
		switch (curAmmoType) {
		case 0:
			magSize = birdMagSize;
			break;
		case 1:
			magSize = buckMagSize;
			break;
		case 2:
			magSize = slugMagSize;
			break;
		}
		FillDelegate ();
		AmmoCycle ();
		UIChecker ();
	}

	public override void WeaponChecker(){
		//myAnimator.SetBool ("SwordEquip", false);
		//myAnimator.SetBool ("RevolverEquip", true);
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
			loadedAmmo = birdAmmo;
			shotCount = birdShotCount;
			shootDirValueX = birdDirValueX;
			shootDirValueY = birdDirValueY;
			curAmmoTypeText = birdTotalAmmo;
			break;
		case 1:
			loadedAmmo = buckAmmo;
			shotCount = buckShotCount;
			shootDirValueX = buckDirValueX;
			shootDirValueY = buckDirValueY;
			curAmmoTypeText = buckTotalAmmo;
			break;
		case 2:
			loadedAmmo = slugAmmo;
			shotCount = slugShotCount;
			shootDirValueX = slugDirValueX;
			shootDirValueY = slugDirValueY;
			curAmmoTypeText = slugTotalAmmo;
			break;
		}
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
			if (loadedAmmo > 0) {
				StartCoroutine (ShotDivider ());
				AmmoRemove ();
			}
			if (Input.GetButtonDown ("Reload") || loadedAmmo == 0) {
				if (loadedAmmo < magSize) {
					Reloading ();
				}
			}
			UIChecker ();
		}
	}

		public IEnumerator ShotDivider(){
			int shotsFired = 0;
			cameroTransform = camero.transform;
			for (int i = 0; i < shotCount; i++) {
				shootDir = cameroTransform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
				//Debug.DrawRay (cameroTransform.position, shootDir * 5000, Color.blue, 3);
				if (Physics.Raycast (cameroTransform.position, shootDir, out rayHit, rayDis)) {
					DistanceChecker (player.transform.position);		
				} 
				else {
					print ("MISSED");
				}
				shotsFired++;
				if (shotCount >= shotsFired && shotsFired == shotsDividerCount) {
					yield return 0;
					shotsFired = 0;
				}
			}
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
			print ("Hit the Head, damage x3");
			Destroy (rayHitTransform.gameObject);
			break;
		case "Limbs":
			print ("Hit Limb, damage x1");
			Destroy (rayHitTransform.gameObject);
			break;
		case "Body":
			print ("Hit the Body, damage x2");
			//Destroyen van de parent voorbeeld:
			GameObject parento = rayHitTransform.parent.gameObject;
			Destroy (parento);
			break;
		case "Barrel":
			rayHitTransform.GetComponent<SmokeBarrel> ().Explode ();
			break;
		default:
			print ("NOT AN ENEMY!");
			break;
		}
		yield return new WaitForSeconds(impactTime);
	}

	public override void Reloading(){
		switch (curAmmoType){
		case 0:
			if (birdTotalAmmo > 0) {
				int leftoverAmmo = birdMagSize - birdAmmo;
				print (curAmmoTypeText);
				for (int i = 0; leftoverAmmo > 0; i++) {
					birdTotalAmmo--;
					birdAmmo++;
					leftoverAmmo--;
					if (birdTotalAmmo < 1) {
						break;
					}
				}
				loadedAmmo = birdAmmo;
				curAmmoTypeText = birdTotalAmmo;
			}
			else {
				print ("Out of birdshot Magazines!");
			}
			break;
		case 1:
			if (buckTotalAmmo > 0) {
				int leftoverAmmo = buckMagSize - buckAmmo;
				for (int i = 0; leftoverAmmo > 0; i++) {
					buckTotalAmmo--;
					buckAmmo++;
					leftoverAmmo--;
					if (buckTotalAmmo < 1) {
						break;
					}
				}
				loadedAmmo = buckAmmo;
				curAmmoTypeText = buckTotalAmmo;
			}
			else {
				print ("Out of buckshot Magazines!");
			}
			break;
		case 2:
			if (slugTotalAmmo > 0) {
				int leftoverAmmo = slugMagSize - slugAmmo;
				for (int i = 0; leftoverAmmo > 0; i++) {
					slugTotalAmmo--;
					slugAmmo++;
					leftoverAmmo--;
					if (slugTotalAmmo < 1) {
						break;
					}
				}
				loadedAmmo = slugAmmo;
				curAmmoTypeText = slugTotalAmmo;
			}
			else {
				print ("Out of slugshot Magazines!");
			}
			break;
		}
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

	public override void AmmoRemove(){
		switch (curAmmoType){
		case 0:
			birdAmmo--;
			loadedAmmo = birdAmmo;
			break;
		case 1:
			buckAmmo--;
			loadedAmmo = buckAmmo;
			break;
		case 2:
			slugAmmo--;
			loadedAmmo = slugAmmo;
			break;
		}
	}

	public override int AmmoAdd(int ammoToAdd){
		switch (ammoToAdd) {
		case 0:
			if (birdTotalAmmo < maxBirdAmmo) {
				birdTotalAmmo += birdMagSize;
				if(birdTotalAmmo > maxBirdAmmo){
					birdTotalAmmo = maxBirdAmmo;
				}
			}
			break;
		case 1:
			if (buckTotalAmmo < maxBuckAmmo) {
				buckTotalAmmo += buckMagSize;
				if(buckTotalAmmo > maxBuckAmmo){
					buckTotalAmmo = maxBuckAmmo;
				}
			}
			break;
		case 2:
			if (slugTotalAmmo < maxSlugAmmo) {
				slugTotalAmmo += slugMagSize;
				if(slugTotalAmmo > maxSlugAmmo){
					slugTotalAmmo = maxSlugAmmo;
				}
			}
			break;
		}
		switch(curAmmoType){
		case 0:
			curAmmoTypeText = birdTotalAmmo;
			break;
		case 1: 
			curAmmoTypeText = buckTotalAmmo;
			break;
		case 2: 
			curAmmoTypeText = slugTotalAmmo;
			break;
		}
		return ammoToAdd;
	}

	public override void AmmoEffect(GameObject hit){

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
}
