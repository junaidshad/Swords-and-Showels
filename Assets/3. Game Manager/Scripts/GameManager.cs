using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager> {
    // keep track of the game state

    // PREGAME, RUNNING, PAUSE

    public enum GameState{
        PREGAME,
        RUNNING,
        PAUSED
    }
    
    public GameObject[] SystemPrefabs;
    public Events.EventGameState OnGameStateChanged;

    private List<GameObject> _instancedSystemPrefabs = new List<GameObject>();

    private string _currentLevelName = string.Empty;

    List<AsyncOperation> _loadOperations;
    GameState _currentGameState = GameState.PREGAME;
    public GameState CurrentGameState{
        get{return _currentGameState;}
        private set{_currentGameState = value;}
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();
        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    void Update(){
		if(_currentGameState == GameManager.GameState.PREGAME){
			return;
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			TogglePause();
		}
	}

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if(_loadOperations.Contains(ao)){
            _loadOperations.Remove(ao);

            if(_loadOperations.Count == 0){
                UpdateState(GameState.RUNNING);
            }
        }
        Debug.Log("Loading Complete");
    }
    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unloading Complete");
    }

    void HandleMainMenuFadeComplete(bool fadeOut){
        if(!fadeOut){
            UnloadLevel(_currentLevelName);
        }
        
    }

    void UpdateState(GameState state){
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch(state){
            case GameState.PREGAME:{
                Time.timeScale = 1f;
            }break;
            case GameState.RUNNING:{
                Time.timeScale = 1f;
            }break;
            case GameState.PAUSED:{
                Time.timeScale = 0f;
            }break;
            default:{

            }break;
        }
        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
            // dispatch message
            // transition between scenes
    }

    void InstantiateSystemPrefabs(){
        GameObject prefabInstance;
        for(int i = 0; i< SystemPrefabs.Length; ++i){
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if(ao == null){
            Debug.LogError("[GameManager] Unable to load level");
            return;
        }
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }
    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if(ao == null){
            Debug.LogError("[GameManager] Unable to unload level");
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    protected override void OnDestroy(){
        base.OnDestroy();
        for(int i = 0; i < _instancedSystemPrefabs.Count; ++i){
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();

    }

    public void StartGame(){
        LoadLevel("Main");
    }

    public void TogglePause(){
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame(){
        UpdateState(GameState.PREGAME);
    }
    public void QuitGame(){
        // implement features for quitting.

        Application.Quit();
    }
}
