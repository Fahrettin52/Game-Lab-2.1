using UnityEngine;
using System.Collections;

public class GrenadePack : MonoBehaviour {
	public GameObject player;
	public int randomGrenade;

	//Hieronder is tekst bedoeld voor het testen. Verwijder dit bij het implementeren.
	public void OnCollisionEnter(Collision col){
		DetermenGrenade ();
	}

	public void OnCollisionExit(Collision col){
		AddGrenade ();
	}

	public void DetermenGrenade(){
		randomGrenade = Random.Range (0, player.GetComponent<WeaponManager>().grenadesCount.Length);
	}

	public void AddGrenade(){
		player.GetComponent<WeaponManager> ().grenadesCount [randomGrenade]++;
	}
}
