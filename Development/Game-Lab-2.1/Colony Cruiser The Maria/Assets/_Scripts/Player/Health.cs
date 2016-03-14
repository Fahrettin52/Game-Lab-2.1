using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int health;
	public int maxHealth;
	private int startHealth;
	public Transform respawnPoint;

	void Start () {
		startHealth = health;
		maxHealth = health;
	}

	//Dit is om healing en damaging te testen
//	void Update () {
//		if(Input.GetButtonDown("Jump")){
//			Damaging (25);
//		}
//		if(Input.GetButtonDown("ActivatePack")){
//			Healing (25);
//		}
//	}

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
		//Speel hier de death/gameover screen af
		transform.position = respawnPoint.position;
		transform.rotation = respawnPoint.rotation;
		health = startHealth;
	}
}
