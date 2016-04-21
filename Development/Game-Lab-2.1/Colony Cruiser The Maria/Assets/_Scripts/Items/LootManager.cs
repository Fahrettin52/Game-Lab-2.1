using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LootManager : MonoBehaviour {

	public List<LootPackage> myLoot = new List<LootPackage> ();

	public int currentScene;

	public Sprite[] loot1, loot2, loot3;

	public Image[] currentLoot;

	public GameObject lootHUD;

	public void SceneChecker(){
		currentScene =  SceneManager.GetActiveScene().buildIndex;
	}

	public void LootChecker(){
		lootHUD.SetActive (true);

		Sprite[] ts;
		int r = Random.Range (0, myLoot.Count);

		//ts = ;

		//Sprite[] ts;
		//int r = Random.Range (0, 3);
//		if (r == 0) {
//			ts = loot1;
//		} 
//		else if (r == 1) {
//			ts = loot2;
//		} 
//		else {
//			ts = loot3;
//		}

			
		Sprite[] randomLoot = ts;
		for (int i = 0; i < currentLoot.Length; i++){
			foreach( Image image in currentLoot ){
				currentLoot[i].sprite = randomLoot [i];
			}
		}
	}
}
