using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

	public void GameQuality(int myQuality){
		QualitySettings.SetQualityLevel (myQuality);
	}	
}
