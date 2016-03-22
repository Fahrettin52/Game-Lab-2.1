using UnityEngine;
using System.Collections;

public abstract class AbstractWeapon : MonoBehaviour {
	public GameObject player;
	public GameObject camero;
	public Sprite myWeaponIcon;
	public string myAmmoCount;
	public bool aiming;
	public float rayDis;
	public float shootDirValueX;
	public float shootDirValueY;
	public Vector3 shootDir;
	public RaycastHit rayHit;

	public float zoomSpeed;
	public float maxFieldOfView;
	public float minFieldOfView;

	public float projectileDamage;
	public float projectileSpeed;
	public float effectiveRange;

    public abstract void Shooting();

    public abstract void Reloading();

    public abstract void OnEnable ();

    public abstract bool Aiming(bool aim);

    public abstract void AmmoRemove();

    public abstract void AmmoAdd();

    public abstract void AmmoEffect();

    public abstract void HitChecker();

    public abstract void FillDelegate ();	

	public abstract void DistanceChecker (Vector3 savedPos);

	public abstract IEnumerator ImpactDelay (float impactTime, float damage);

	public abstract void QuickMelee ();

	public abstract void UIChecker();
}
