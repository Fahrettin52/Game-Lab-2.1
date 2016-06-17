using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EyesTest : MonoBehaviour {

	public Image[] eyes;
	public int current;

	private float nextPicture;
	public float pictureRate;

	public bool mayOpen;

	public GameObject top;
	public GameObject bot;

//	public void Start (){
//		current = 0;
//	}

	void Update () {
		if(mayOpen == true){
			top.GetComponent<Animator> ().SetBool ("Open", true);
			bot.GetComponent<Animator> ().SetBool ("Open1", true);
		}
//
//		if (current < eyes.Length-1) {
//			if (Time.time > nextPicture && mayOpen == true) {
//				EyesAnimation (current);
//				current++;
//				nextPicture = Time.time + pictureRate;
//			}
//		}
//	}
//
//	public void EyesAnimation(int number){
//		eyes [number].GetComponent<Image> ().enabled = false;
//		number++;
//		eyes [number].GetComponent<Image> ().enabled = true;
	}
}