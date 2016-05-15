using UnityEngine;
using System.Collections;

public class VirtualGrenades : AbstractGrenades {
	public override IEnumerator TimerToExplode(){
		yield return new WaitForSeconds (myTimer);
	}

	public override void GrenadeEffect(GameObject objectHit){
		
	}
}
