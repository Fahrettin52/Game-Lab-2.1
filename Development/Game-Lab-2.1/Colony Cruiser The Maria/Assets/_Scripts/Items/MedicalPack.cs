using UnityEngine;
using System.Collections;

public class HealthPack : AbstractPack {

//	public void OnCollisionEnter(Collision col){
//		AddPack ();
//	}

	public override void AddPack(){
		player.GetComponent<PackManager> ().medPackCount++;
	}
}
