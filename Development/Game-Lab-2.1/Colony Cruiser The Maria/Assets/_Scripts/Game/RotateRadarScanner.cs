using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotateRadarScanner : MonoBehaviour {

	void Update () {
		GetComponent<RectTransform> ().Rotate(new Vector3( 0, 0, -90) * Time.deltaTime) ;
	}
}
