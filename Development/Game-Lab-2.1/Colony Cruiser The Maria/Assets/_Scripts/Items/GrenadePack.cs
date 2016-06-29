using UnityEngine;
using System.Collections;

public class GrenadePack : MonoBehaviour {
	public GameObject player;
	private int randomGrenade;

	public void AddGrenade(){
		randomGrenade = Random.Range (0, player.GetComponent<WeaponManager>().grenadesCount.Length);
		if(player.GetComponent<WeaponManager>().grenadesCount[randomGrenade] < player.GetComponent<WeaponManager>().maxGrenadesCount){
			player.GetComponent<WeaponManager> ().grenadesCount [randomGrenade]++;
		}
	}
}
