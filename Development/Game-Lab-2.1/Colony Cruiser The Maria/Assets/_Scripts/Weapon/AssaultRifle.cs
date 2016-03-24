using UnityEngine;
using System.Collections;

public class AssaultRifle : AbstractWeapon {

	public override void Shooting(){}

	public override void Reloading(){}

	public override void OnEnable (){}

	public override bool Aiming(bool aim){
		return aim;
	}

	public override void AmmoRemove(){}

	public override void AmmoAdd(){}

	public override void AmmoEffect(){}

	public override void HitChecker(){}

	public override void FillDelegate (){}

	public override void DistanceChecker (Vector3 savedPos){}

	public override IEnumerator ImpactDelay (float impactTime, float damage){
		yield return new WaitForSeconds(impactTime);
	}

	public override void QuickMelee (){}

	public override void UIChecker(){}
}