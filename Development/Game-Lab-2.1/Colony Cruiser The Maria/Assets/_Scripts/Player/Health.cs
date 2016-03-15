using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public int health;
	public int maxHealth;
	private int startHealth;
	public float deathDuration;
	public Transform respawnPoint;
	public CanvasGroup gameOverHud;

	void Start () {
		startHealth = health;
		maxHealth = health;
	}

	//Dit is om damaging te testen
	void Update () {
		if(Input.GetButtonDown("Jump")){
			Damaging (25);
		}
	}

	public void Healing(int toHeal){
		if(health < maxHealth){
			health += toHeal;
			if(health > maxHealth){
				health = maxHealth;
			}
		}
	}

	public void Damaging(int toDamage){
		if (health > 1) {
			health -= toDamage;
			if (health < 1) {
				health = 0;
				Death ();
			}
		}
	}

	public void Death(){
		print ("Dieded!!");
		gameOverHud.alpha = 1;
		GetComponent<Movement> ().myMovement = Movement.MovementType.Dead;
		GetComponent<CameraControl> ().myView = CameraControl.ViewType.Dead;
		transform.position = respawnPoint.position;
		transform.rotation = respawnPoint.rotation;
		health = startHealth;
		StartCoroutine(Rebirth (deathDuration));
	}

	public IEnumerator Rebirth(float rebirthDelay){
		yield return new WaitForSeconds(rebirthDelay);
		gameOverHud.alpha = 0;
		GetComponent<Movement> ().myMovement = Movement.MovementType.Normal;
		GetComponent<CameraControl> ().myView = CameraControl.ViewType.Normal;
	}
}
