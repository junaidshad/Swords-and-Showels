using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {
	[SerializeField] MainMenu _mainMenu;
	[SerializeField] PauseMenu _pauseMenu;
	[SerializeField] Camera _dummyCamera;

public Events.EventFadeComplete OnMainMenuFadeComplete;

	void Start(){
		_mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
		GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
	}
	void HandleMainMenuFadeComplete(bool fadeOut){
		OnMainMenuFadeComplete.Invoke(fadeOut);
	}

	void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState){
		_pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
	}


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
