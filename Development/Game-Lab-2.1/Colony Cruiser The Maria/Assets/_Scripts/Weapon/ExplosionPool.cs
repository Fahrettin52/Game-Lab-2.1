using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionPool : MonoBehaviour {
	public enum PoolState{
		Pooled,
		UnPooled
	}
	public List<PoolState> poolStateList = new List<PoolState>();
	public GameObject[] poolElement;
	public float rePoolTimer;

	public void PickFromPool(Vector3 explosionPosition){
		for(int i = 0; i < poolElement.Length; i++){
			if (poolStateList [i] == PoolState.Pooled) {
				poolStateList [i] = PoolState.UnPooled;
				PlacePoolElement (poolElement [i], explosionPosition);
				break;
			}
		}
	}

	public void PlacePoolElement(GameObject pickedElement, Vector3 explosionPosition){
		pickedElement.transform.position = explosionPosition;
		ParticleSystem selectedParticles;
		selectedParticles = pickedElement.GetComponent<ParticleSystem> ();
		selectedParticles.Play ();
		selectedParticles.startLifetime = selectedParticles.startLifetime;
		StartCoroutine ("Repooler");
	}

	public IEnumerator Repooler(){
		yield return new WaitForSeconds (rePoolTimer);
		for(int i = 0; i < poolElement.Length; i++){
			if (poolStateList [i] == PoolState.UnPooled) {
				ParticleSystem selectedParticles = poolElement [i].GetComponent<ParticleSystem> ();
				selectedParticles.Stop ();
				poolStateList [i] = PoolState.Pooled;
				break;
			}
		}
	}
}
