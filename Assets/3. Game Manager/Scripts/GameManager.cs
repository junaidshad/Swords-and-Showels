using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    // keep track of the game state
    
    public GameObject[] SystemPrefabs;

    private List<GameObject> _instancedSystemPrefabs = new List<GameObject>();

    private string _currentLevelName = string.Empty;

    List<AsyncOperation> _loadOperations;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        LoadLevel("Main");
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if(_loadOperations.Contains(ao)){
            _loadOperations.Remove(ao);
            // dispatch message
            // transition between scenes
        }
        Debug.Log("Loading Complete");
    }
    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unloading Complete");
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
}
