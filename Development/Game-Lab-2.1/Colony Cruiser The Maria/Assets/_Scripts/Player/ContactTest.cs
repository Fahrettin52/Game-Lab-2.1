using UnityEngine;
using System.Collections;

public class ContactTest : MonoBehaviour {
	//Ditmoet op de melee enemy komen te staan
	public void OnCollisionEnter(Collision col){
		if (col.transform.tag != "TestGrond") {
			ContactPoint contact = col.contacts[0];
			print (contact.normal);
			Debug.DrawRay (contact.point, contact.normal, Color.blue, 3f);
		}
	}
}
