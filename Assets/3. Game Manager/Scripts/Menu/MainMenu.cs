using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	
	// Function that can recieve animation event
	// Functions to play fade in/out

	[SerializeField] Animation _mainMenuAnimator;
	[SerializeField] AnimationClip _fadeOutAnimation;
	[SerializeField] AnimationClip _fadeInAnimation;
	
	public Events.EventFadeComplete OnMainMenuFadeComplete;

	void Start(){
		GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
	}

	public void OnFadeOutComplete(){
		OnMainMenuFadeComplete.Invoke(true);
	}
	public void OnFadeInComplete(){
		UIManager.Instance.SetDummyCameraActive(true);
		OnMainMenuFadeComplete.Invoke(false);
	}

	void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState){
		if(previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING){
			FadeOut();
		}
		if(previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME){
			FadeIn();
		}
	}

	public void FadeIn(){
		_mainMenuAnimator.Stop();
		_mainMenuAnimator.clip = _fadeInAnimation;
		_mainMenuAnimator.Play();
	}
	public void FadeOut(){
		UIManager.Instance.SetDummyCameraActive(false);
		_mainMenuAnimator.Stop();
		_mainMenuAnimator.clip = _fadeOutAnimation;
		_mainMenuAnimator.Play();
	}
}
