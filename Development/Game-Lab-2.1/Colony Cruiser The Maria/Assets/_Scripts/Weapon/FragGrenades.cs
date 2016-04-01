using UnityEngine;
using System.Collections;

public class FragGrenades : AbstractGrenades {
	
	public override void TimerToExplode(){
		myTimer -= Time.deltaTime;
		if(myTimer < 0){
			//Activeer triggerveld hier
		}
	}

	public override void GrenadeEffect(){
		
	}

	public void OnTriggerEnter(Collider col){
		if(col.transform.tag == "Enemy"){
			//Damage hier de enemy
		}
		if(col.transform.tag == "Player"){
			col.transform.GetComponent<Health> ().HealOrDamage ("damage", damage);
		}
	}
}
