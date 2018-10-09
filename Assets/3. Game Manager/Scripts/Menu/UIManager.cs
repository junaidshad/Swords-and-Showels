using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {
	[SerializeField] MainMenu _mainMenu;
	[SerializeField] Camera _dummyCamera;
	void Update(){
		if(GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME){
			return;
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			GameManager.Instance.StartGame();
		}
	}

	public void SetDummyCameraActive(bool active){
		_dummyCamera.gameObject.SetActive(active);
	}
}
