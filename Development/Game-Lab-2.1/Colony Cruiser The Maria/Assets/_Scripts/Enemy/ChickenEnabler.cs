using UnityEngine;
using System.Collections;

public class ChickenEnabler : MonoBehaviour {

	void Start () {
		if(!GetComponent<Chicken>().enabled){
			GetComponent<Chicken> ().enabled = true;
		}
	}
}
