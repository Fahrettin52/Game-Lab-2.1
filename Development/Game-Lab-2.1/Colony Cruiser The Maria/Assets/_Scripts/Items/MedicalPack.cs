using UnityEngine;
using System.Collections;

public class MedicalPack : AbstractPack {

//	public void OnCollisionEnter(Collision col){
//		AddPack ();
//	}

	public override void AddPack(){
		player.GetComponent<PackManager> ().medPackCount++;
	}
}
