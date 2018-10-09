using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	
	// Function that can recieve animation event
	// Functions to play fade in/out

	[SerializeField] Animation _mainMenuAnimator;
	[SerializeField] AnimationClip _fadeOutAnimation;
	[SerializeField] AnimationClip _fadeInAnimation;
	

	void Start(){
		GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
	}

	public void OnFadeOutComplete(){
		Debug.LogWarning("Fade out Complete");

	}
	public void OnFadeInComplete(){
		Debug.LogWarning("Fade in Complete");
		UIManager.Instance.SetDummyCameraActive(true);

	}

	void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState){
		if(previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING){
			FadeOut();
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
