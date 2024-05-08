using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour, IDataPersistence
{
    public static SceneControl Instance;

    string _sceneToLoad;


    private void Awake()
    {
        Instance = this;
    }
    public string SceneToLoad { get => _sceneToLoad; set => _sceneToLoad = value; }

    void IDataPersistence.SaveData(ref GameData data)
    {
        if(SceneManager.GetActiveScene().name != "MainMenu")
            data.sceneToLoad = PlayerController.Instance.GetSceneToLoad();
    }

    void IDataPersistence.LoadData(GameData data)
    {
        _sceneToLoad = data.sceneToLoad;
    }
    
}
