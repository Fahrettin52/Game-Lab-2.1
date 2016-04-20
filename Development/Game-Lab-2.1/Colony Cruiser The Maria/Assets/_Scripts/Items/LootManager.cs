using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LootManager : MonoBehaviour {

	public int currentScene;

	public Sprite[] loot1, loot2, loot3;
	public Image[] currentLoot;

	public void SceneChecker(){
		currentScene = Application.loadedLevel;
	}

	public void LootChecker(){
		Sprite[] ts;
		int r = Random.Range (0, 3);
		if (r == 0) {
			ts = loot1;
		} 
		else if (r == 1) {
			ts = loot2;
		} 
		else {
			ts = loot3;
		}
		int q = Random.Range (0, ts.Length);

			
		Sprite[] randomLoot = ts;
		for (int i = 0; i < currentLoot.Length; i++){
			foreach( Image image in currentLoot ){
				currentLoot[i].sprite = randomLoot [i];
			}
		}
	}
}
