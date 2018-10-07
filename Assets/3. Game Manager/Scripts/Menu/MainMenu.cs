using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	
	// Function that can recieve animation event
	// Functions to play fade in/out

	[SerializeField] Animation _mainMenuAnimator;
	[SerializeField] AnimationClip _fadeOutAnimation;
	[SerializeField] AnimationClip _fadeInAnimation;
	

	public void OnFadeOutComplete(){
		Debug.LogWarning("Fade out Complete");

	}
	public void OnFadeInComplete(){
		Debug.LogWarning("Fade in Complete");
		UIManager.Instance.SetDummyCameraActive(true);

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
