using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EyesTest : MonoBehaviour {

	public Image[] eyes;
	public int current;

	private float nextPicture;
	public float pictureRate;

	public void Start (){
		current = 0;
	}

	void Update () {
		if(Time.time > nextPicture){
			EyesAnimation(current);
			current++;
			nextPicture = Time.time + pictureRate;
		}
		if (current >= eyes.Length) {
			current = 0;
		}
	}

	public void EyesAnimation(int number){
		eyes [number].GetComponent<Image> ().enabled = false;
		number++;
		eyes [number].GetComponent<Image> ().enabled = true;
	}
}