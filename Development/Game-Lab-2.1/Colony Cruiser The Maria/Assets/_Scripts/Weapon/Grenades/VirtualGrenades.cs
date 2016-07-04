using UnityEngine;
using System.Collections;

public abstract class VirtualGrenades : MonoBehaviour {
	protected GameObject myPoolManager;
	public string poolManagerString;
	public RaycastHit rayHit;
	public string[] blockade;
	public float myTimer;
	public float grenadeDamage;
	public Rigidbody rigidBody;
	public float throwingPower;
	public float radius;
	public float power;

	public virtual IEnumerator TimerToExplode(){
		yield return new WaitForSeconds (myTimer);
	}

	public virtual void GrenadeEffect(GameObject objectHit){

	}
}