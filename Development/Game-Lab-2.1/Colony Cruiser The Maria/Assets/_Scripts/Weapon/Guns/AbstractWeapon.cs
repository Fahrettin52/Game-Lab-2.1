using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class AbstractWeapon : MonoBehaviour {

	public Image recticale;
	public Image recticaleHit;

	public GameObject player;
	public GameObject camero;

	public WeaponManager weaponManager;

	public Animator myAnimator;

	public AnimatorStateInfo myStateInfo;

	public Transform cameroTransform;

	public Sprite myWeaponIcon;

	public string myAmmoCount;
	public string idleHash;

	public bool aiming;

	public Vector3 shootDir;

	public RaycastHit rayHit;

	public float zoomSpeed;
	public float maxFieldOfView;
	public float minFieldOfView;
	public float projectileDamage;
	public float projectileSpeed;
	public float effectiveRange;
	public float shotCount;
	public float rayDis;
	public float shootDirValueX;
	public float shootDirValueY;
	public float hitTime;
	public float hitRate;

	public int loadedAmmo;
	public int curAmmoType;
	public int curAmmoTypeText;
	public int maxAmmoType;
	public int magSize;

    public abstract void Shooting();

    public abstract void Reloading();

    public abstract void OnEnable ();

    public abstract bool Aiming(bool aim);

    public abstract void AmmoRemove();

	public abstract int AmmoAdd(int ammoToAdd);

	public abstract void AmmoEffect(GameObject hit);

    public abstract void FillDelegate ();	

	public abstract void DistanceChecker (Vector3 savedPos);

	public abstract IEnumerator ImpactDelay (float impactTime, float damage);

	public abstract void QuickMelee ();

	public abstract void UIChecker();

	public abstract void AmmoSwitch();

	public abstract void AmmoCycle();
}
