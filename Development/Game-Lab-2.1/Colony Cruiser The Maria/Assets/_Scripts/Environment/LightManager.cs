using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

	public GameObject[] lightsHallway;
	private float nextShine;
	public float shiningRate;
	public float maxRandomRange;

	void FixedUpdate () {
		if (Time.time > nextShine) {
			for (int i = 0; i < lightsHallway.Length; i++) {
				i = Random.Range (0, lightsHallway.Length);
				StartCoroutine (lightSwitcher (i));
				float randomRate = Random.Range (0, maxRandomRange);
				nextShine = Time.time + shiningRate + randomRate;
			}
		}
	}

	IEnumerator lightSwitcher(int i){
		lightsHallway [i].SetActive (false);
		float randomNumber = Random.Range (0.2f, 1f);
		yield return new WaitForSeconds (randomNumber);
		lightsHallway [i].SetActive (true);
	}
}
