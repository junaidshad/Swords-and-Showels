using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {
	[SerializeField] MainMenu _mainMenu;
	[SerializeField] Camera _dummyCamera;
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			_mainMenu.FadeOut();
		}
	}

	public void SetDummyCameraActive(bool active){
		_dummyCamera.gameObject.SetActive(active);
	}
}
