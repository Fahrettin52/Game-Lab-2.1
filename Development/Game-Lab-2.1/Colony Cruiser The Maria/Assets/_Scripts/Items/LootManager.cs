using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LootManager : MonoBehaviour {

	public int currentScene;

	public List<LootPackage> myLoot = new List<LootPackage> ();

	public Image[] currentLoot;

	public GameObject lootHUD;

	public GameObject player;

	public void SceneChecker(){
		currentScene =  SceneManager.GetActiveScene().buildIndex;
	}

	public void LootChecker(){
		lootHUD.SetActive (true);	
		int r = Random.Range (0,0);
		List<Sprite> ts = myLoot [r].myImages;
		for (int i = 0; i < currentLoot.Length; i++){
			currentLoot[i].sprite = ts [i];
		}
		player.GetComponent<CameraControl> ().myView = CameraControl.ViewType.Dead;
	}

	public void CloseLoot(){
		lootHUD.SetActive (false);
		player.GetComponent<CameraControl> ().myView = CameraControl.ViewType.Normal;
	}
}
