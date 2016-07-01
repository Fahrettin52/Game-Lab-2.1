using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public enum DangerLevelState{
		Low,
		Med,
		High
	}

	public DangerLevelState dangerLevelState;
	public string stringForHealing;
	public string stringForDamage;
	private string recievedString;
	public float health;
	private float maxHealth;
	public float highDangerLevel;
	public float medDangerLevel;
	public float lowDangerLevel;
	public float deathDuration;
	public float healRate;
	public float regenerationAmount;
	private float nextHeal;
	private float maxDangerLevel;
	private float selectedDangerLevel;
	private float curLevelDangerPortion;
	private float recievedAmount;
	private int selectedHudElement;
	public Transform respawnPoint;
	public CanvasGroup gameOverHud;
	public CanvasGroup[] HudElements;
	public GameObject soundManager;

	void Start () {
		maxHealth = health;
	}

	public void Update(){
		HealerAndDamager ();
		Regeneration ();
		LevelStateFiller ();
	}

	private void HealerAndDamager(){
		while (recievedAmount > 0) {
			recievedAmount--;
			if (recievedString == stringForHealing) {
				if (health < maxHealth) {
					health++;
				}
			}  
			else if (recievedString == stringForDamage) {
				if (health > 1) {
					int randomInt = Random.Range(0, 2);
					soundManager.GetComponent<SoundManager>().TakingDamage(randomInt);
					health--;
				}  
				else {
					Death ();
				}
			}
			float dangerLevelHealth = maxDangerLevel - health;
			float dangerLevelPortion = maxDangerLevel - selectedDangerLevel;
			float dangerLevelLostHealth = curLevelDangerPortion - dangerLevelHealth;
			float dangerLevelPercentage = (dangerLevelPortion - dangerLevelLostHealth) / dangerLevelPortion;
			HudElements [selectedHudElement].alpha = dangerLevelPercentage;
			if (dangerLevelPercentage < 0 || dangerLevelPercentage > 1) {
				switch (dangerLevelState) {
				case DangerLevelState.High:
					if (recievedString == stringForHealing) {
						dangerLevelState = DangerLevelState.Med;
					}
					break;
				case DangerLevelState.Med:
					if (recievedString == stringForHealing) {
						dangerLevelState = DangerLevelState.Low;
					}  
					else if (recievedString == stringForDamage) {
						dangerLevelState = DangerLevelState.High;
					}
					break;
				case DangerLevelState.Low:
					if (recievedString == stringForDamage) {
						dangerLevelState = DangerLevelState.Med;
					}
					else if(recievedString == stringForHealing){
						dangerLevelState = DangerLevelState.High;
					}
					break;
				}
			}
		}
	}

	public void Regeneration(){
		if (health < maxHealth) {
			if (Time.time > nextHeal) {
				HealOrDamage ("heal", regenerationAmount);
				nextHeal = Time.time + healRate;
			}
		}
	}

	public void HealOrDamage(string enterHealOrDamage, float amount){
		print ("Damage Calculation");
		recievedString = enterHealOrDamage;
		recievedAmount += amount;
	}


	public void LevelStateFiller(){
		switch (dangerLevelState) {
		case DangerLevelState.Low:
			maxDangerLevel = maxHealth;
			selectedDangerLevel = lowDangerLevel;
			float lowLevelPortion = maxHealth - lowDangerLevel;
			curLevelDangerPortion = lowLevelPortion;
			selectedHudElement = (int)DangerLevelState.Low;
			break;
		case DangerLevelState.Med:
			maxDangerLevel = lowDangerLevel;
			selectedDangerLevel = medDangerLevel;
			float medLevelPortion = lowDangerLevel - medDangerLevel;
			curLevelDangerPortion = medLevelPortion;
			selectedHudElement = (int)DangerLevelState.Med;
			break;
		case DangerLevelState.High:
			maxDangerLevel = medDangerLevel;
			selectedDangerLevel = highDangerLevel;
			float highLevelPortion = medDangerLevel - highDangerLevel;
			curLevelDangerPortion = highLevelPortion;
			selectedHudElement = (int)DangerLevelState.High;
			break;
		} 
	}

	public void Death(){
		gameOverHud.alpha = 1;
		GetComponent<Movement> ().myMovement = Movement.MovementType.Dead;
		GetComponent<CameraControl> ().myView = CameraControl.ViewType.Dead;
		transform.position = respawnPoint.position;
		transform.rotation = respawnPoint.rotation;
		HealOrDamage (stringForHealing, maxHealth);
		StartCoroutine(Rebirth (deathDuration));
	}

	public IEnumerator Rebirth(float rebirthDelay){
		yield return new WaitForSeconds(rebirthDelay);
		gameOverHud.alpha = 0;
		GetComponent<Movement> ().myMovement = Movement.MovementType.Normal;
		GetComponent<CameraControl> ().myView = CameraControl.ViewType.Normal;
	}
		
}
