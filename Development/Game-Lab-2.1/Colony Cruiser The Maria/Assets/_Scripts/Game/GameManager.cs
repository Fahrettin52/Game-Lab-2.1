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
	public static float keyCard;

	public float curTimeScale;
	private float defaultTimeScale;
	public float slowMoTimeScale;
	public float slowMoCorrecter;
	public float slowmoTimer;

	public GameObject player;
	public GameObject pauseHUD;
	public bool pausing;

	void Start(){
		defaultTimeScale = Time.timeScale;
	}

	void Update(){
		if (Input.GetButtonDown ("Cancel")) {
			PauseGame (curTimeScale);
			pausing = !pausing;
			pauseHUD.SetActive (pausing);
		}
	}

	public void GameStateChecker(GameState sendState){
		switch (sendState) {
		case GameState.Normal:
			curTimeScale = defaultTimeScale;
			Time.timeScale = defaultTimeScale;
			player.GetComponent<Movement> ().slowmoCorrection = slowMoCorrecter / slowMoCorrecter;
			break;
		case GameState.Slowmo:
			curTimeScale = slowMoTimeScale;
			player.GetComponent<Movement> ().slowmoCorrection = slowMoCorrecter;
			Time.timeScale = slowMoTimeScale;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
			StartCoroutine(SlowmoReset ());
			break;
		}
	}

	public IEnumerator SlowmoReset(){
		yield return new WaitForSeconds(slowmoTimer);
		GameStateChecker(GameState.Normal);
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
