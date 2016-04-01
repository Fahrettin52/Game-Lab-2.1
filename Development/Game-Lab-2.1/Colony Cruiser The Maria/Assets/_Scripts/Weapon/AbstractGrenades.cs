using UnityEngine;
using System.Collections;

public abstract class AbstractGrenades : MonoBehaviour {
	public float myTimer;
	public float damage;

	public abstract void TimerToExplode();

	public abstract void GrenadeEffect(); 
}
