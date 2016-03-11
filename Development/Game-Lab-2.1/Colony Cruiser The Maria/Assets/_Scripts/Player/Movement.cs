using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public int moveSpd;

	void FixedUpdate () {
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
			transform.Translate(new Vector3(Input.GetAxis("Horizontal") , 0, Input.GetAxis("Vertical")) * Time.deltaTime * moveSpd);
		}
	}
}
