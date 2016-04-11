using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//Ik moet er nog voor zorgen dat wanneer we in slowmo zitten hij niet terug gaat naar 1 maar naar de slowmo
	//Wanneer het spel eenmaal gepaused is zal de HUD moeten veranderen naar de pause menu
	// Use this for initialization
	public enum GameState{
		Normal,
		Slowmo
	}
	public GameState gameState;
	public static float keyCard;

	public float curTimeScale;
	private float defaultTimeScale;
	public float slowMoTimeScale;

	void Start(){
		defaultTimeScale = Time.timeScale;
	}

	void Update(){
		switch (gameState) {
		case GameState.Normal:
			curTimeScale = defaultTimeScale;
			break;
		case GameState.Slowmo:
			curTimeScale = slowMoTimeScale;
			break;
		}
		if (Input.GetButtonDown ("Cancel")) {
			PauseGame (curTimeScale);
		}
	}

	public void SceneChcker(){
		//Alles zal misschien in 1 scene komen, dus wordt dit gebruikt om alleen te chekcen of we in de
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

	public void AddKeyCard(){
		keyCard++;
	}
}
