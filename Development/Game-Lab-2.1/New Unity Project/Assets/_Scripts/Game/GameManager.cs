using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//Ik moet er nog voor zorgen dat wanneer we in slowmo zitten hij niet terug gaat naar 1 maar naar de slowmo
	//Wanneer het spel eenmaal gepaused is zal de HUD moeten veranderen naar de pause menu
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			PauseGame (1);
		}
	}

	public void SceneChcker(){
		//Alles zal misschien in 1 scebe komen, dus wordt dit gebruikt om alleen te chekcen of we in de
		//startmenu zitten of in de play scene
		//Als het bovenstaande niet mocht lukken zullen we de scene checker gebruiken om gewoon alle scenes in te vullen enzo
	}

	public float PauseGame(float timeType){
		if (Time.timeScale > 0) {
			Time.timeScale = 0;
		}
		else {
			Time.timeScale = timeType;
		}
		//Switch HudStates hier!! maar moet dan eerst de MenuMng maken hiervoor
		return timeType;
	}
}
