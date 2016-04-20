using UnityEngine;
using System.Collections;

public abstract class AbstractGrenades : MonoBehaviour {
	public GameObject myPoolManager;
	public string poolManagerString;
	public RaycastHit rayHit;
	public string[] blockade;
	public float myTimer;
	public float grenadeDamage;
	public Rigidbody rigidBody;
	public float throwingPower;
	public float radius;
	public float power;

	public abstract IEnumerator TimerToExplode();

	public abstract void GrenadeEffect(GameObject objectHit); 
}
