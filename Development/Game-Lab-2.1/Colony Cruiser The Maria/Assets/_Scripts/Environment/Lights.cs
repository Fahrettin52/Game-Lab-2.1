using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {

	public GameObject light;
	public float shiningRate;
	private float nextShine;
	private bool mayShine;
	public float maxRandomRange;

	void FixedUpdate(){
		if (Time.time > nextShine) {
			mayShine = !mayShine;
			light.SetActive (mayShine);
			float randomRate = Random.Range (0, maxRandomRange);
			nextShine = Time.time + shiningRate + randomRate;
		}
	}
}
