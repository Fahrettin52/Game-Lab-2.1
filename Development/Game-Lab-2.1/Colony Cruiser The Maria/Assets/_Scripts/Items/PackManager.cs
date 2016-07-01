using UnityEngine;
using System.Collections;

public class PackManager : MonoBehaviour {
	public enum PackType{
		MedPack,
		StimPack
	}
	public PackType curPackType;

	public int medPackCount;
	public int stimPackCount;
	public int healAmount;
	//public GameObject gameManager;

	void Update () {
		if(Input.GetButtonDown("SwitchPack")){
			SwitchPack ();
		}
		switch (curPackType) {
		case PackType.MedPack:
			MedPackChecker ();
			break;
		case PackType.StimPack:
			StimPackChecker ();
			break;
		}
	}

	public void MedPackChecker(){
		if(medPackCount > 0 && Input.GetButtonDown("ActivatePack")){
			GetComponent<Health>().HealOrDamage("heal", healAmount);
			medPackCount--;
		}
		else if (medPackCount < 1 && stimPackCount > 0) {
			SwitchPack ();
		}
	}

	public void StimPackChecker(){
		if (stimPackCount > 0 && Input.GetButtonDown ("ActivatePack")) {
			print ("Activer slow-mo");
			//Activeer hier de slowmo
			//gameManager.GetComponent<GameManager> ().GameStateChecker(GameManager.GameState.Slowmo);
			stimPackCount--;
		} 
		else if (stimPackCount < 1 && medPackCount > 0) {
			SwitchPack ();
		}
	}

	public void SwitchPack(){
		switch (curPackType) {
		case PackType.MedPack:
			curPackType = PackType.StimPack;
			break;
		case PackType.StimPack:
			curPackType = PackType.MedPack;
			break;
		}
	}
}
