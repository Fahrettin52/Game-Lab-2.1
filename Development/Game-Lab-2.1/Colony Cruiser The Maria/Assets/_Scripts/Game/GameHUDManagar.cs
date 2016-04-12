using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class GameHUDManagar : MonoBehaviour {

	public enum MenuType { start, load, option, credit }

	public MenuType myMenuType;

	public void StateChecker(){
		switch(myMenuType){
		case MenuType.start:
			StartButton();
			break;

		case MenuType.load:
			LoadButton();
			break;

		case MenuType.option:
			OptionButton();
			break;

		case MenuType.credit:
			CreditButton();
			break;
		}
	}	

	public void StartButton(){
		EditorSceneManager.LoadScene ("Verdieping 3"); 
	}

	public void LoadButton(){
	}

	public void OptionButton(){
	}

	public void QuitButton(){
		Application.Quit();
	}

	public void CreditButton(){
	}
}