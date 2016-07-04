using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HUDManagar : MonoBehaviour {

	public GameObject mainMenu;
	//public GameObject load;
	public GameObject option;
	public GameObject credit;
	public GameObject controls;

	public void StartButton(){
		SceneManager.LoadScene ("Verdieping 3"); 
	}

	public void MainMenuButton(){
		SceneManager.LoadScene ("Start"); 
	}

//	public void LoadButton(){
//		mainMenu.SetActive (false);
//		load.SetActive (true);
//	}

	public void OptionButton(){
		mainMenu.SetActive (false);
		option.SetActive (true);
	}

	public void ControlsButton(){
		mainMenu.SetActive (false);
		option.SetActive (true);
	}

	public void QuitButton(){
		mainMenu.SetActive (false);
		Application.Quit();
	}

	public void CreditButton(){
		controls.SetActive (false);
	}

	public void ReturnButton(){
		mainMenu.SetActive (true);
		//load.SetActive (false);
		option.SetActive (false);
		credit.SetActive (false);
	}
}