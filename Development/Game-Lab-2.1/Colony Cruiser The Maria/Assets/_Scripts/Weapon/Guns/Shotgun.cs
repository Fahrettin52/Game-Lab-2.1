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

	public int maxBirdAmmo;
	public int maxBuckAmmo;
	public int maxSlugAmmo;
	public int birdTotalAmmo;
	public int buckTotalAmmo;
	public int slugTotalAmmo;
	public int birdAmmo;
	public int buckAmmo;
	public int slugAmmo;
	public int birdMag;
	public int buckMag;
	public int slugMag;

	public override void OnEnable(){
		player = GameObject.FindWithTag ("Player");
		camero = GameObject.Find ("Main Camera");
		player.GetComponent<WeaponManager> ().weaponIcon.sprite = myWeaponIcon;
		FillDelegate ();
		AmmoCycle ();
		UIChecker ();
	}

	public void AmmoSwitch(){
		print ("switched");
		if (curAmmoType < maxAmmoType) {
			curAmmoType++;
		}
		if(curAmmoType == maxAmmoType){
			curAmmoType = 0;
		}
		AmmoCycle ();
	}

	public void AmmoCycle(){
		print ("Chosen ammo");
		switch (curAmmoType){
		case 0:
			print ("1");
			loadedAmmo = birdAmmo;
			shotCount = birdShotCount;
			shootDirValueX = birdDirValueX;
			shootDirValueY = birdDirValueY;
			curAmmoTypeText = birdTotalAmmo;
			break;
		case 1:
			print ("2");
			loadedAmmo = buckAmmo;
			shotCount = buckShotCount;
			shootDirValueX = buckDirValueX;
			shootDirValueY = buckDirValueY;
			curAmmoTypeText = buckTotalAmmo;
			break;
		case 2:
			print ("3");
			loadedAmmo = slugAmmo;
			shotCount = slugShotCount;
			shootDirValueX = slugDirValueX;
			shootDirValueY = slugDirValueY;
			curAmmoTypeText = slugTotalAmmo;
			break;
		}
	}

	public override void FillDelegate(){
		player.GetComponent<WeaponManager> ().shootDelegate = null;
		player.GetComponent<WeaponManager> ().aimDelegate = null;
		player.GetComponent<WeaponManager> ().ammoSwitchDelegate = null;
		player.GetComponent<WeaponManager> ().quickMeleeDelegate = null;
		player.GetComponent<WeaponManager> ().shootDelegate = Shooting;
		player.GetComponent<WeaponManager> ().aimDelegate = Aiming;
		player.GetComponent<WeaponManager> ().ammoSwitchDelegate = AmmoSwitch;
		player.GetComponent<WeaponManager> ().quickMeleeDelegate = QuickMelee;
	}

	public override void Shooting(){
		if (Input.GetButtonDown ("Fire1")) {
			if (loadedAmmo > 0) {
				for (int i = 0; i < shotCount; i++) {
					Vector3 playerPos = player.transform.position;
					shootDir = camero.transform.forward + new Vector3 (Random.Range (-shootDirValueX, shootDirValueX), Random.Range (-shootDirValueY, shootDirValueY), 0);
					//Debug.DrawRay (camero.transform.position, shootDir * 5000, Color.blue, 3);
					if (Physics.Raycast (camero.transform.position, shootDir, out rayHit, rayDis)) {
						DistanceChecker (playerPos);		
					}
					else {
						print ("MISSED");
					}
				}
				AmmoRemove ();
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
		switch (rayHit.transform.tag) {
		case "Head":
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
			break;
		}
		yield return new WaitForSeconds(impactTime);
	}

	public override void Reloading(){
		int curLoadedAmmo = curAmmoType;
		switch (curLoadedAmmo){
		case 0:
			if (birdTotalAmmo > 0) {
				int leftoverAmmo = birdMag - birdAmmo;
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
				int leftoverAmmo = buckMag - buckAmmo;
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
				int leftoverAmmo = slugMag - slugAmmo;
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
		int curLoadedAmmo = curAmmoType;
		switch (curLoadedAmmo){
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

	public override void AmmoAdd(){

	}

	public override void AmmoEffect(GameObject hit){

	}

	public override void QuickMelee(){

	}

	public override void UIChecker(){
		player.GetComponent<WeaponManager> ().ammoCountHolder.text = (loadedAmmo + "/" + curAmmoTypeText);
	}
}
