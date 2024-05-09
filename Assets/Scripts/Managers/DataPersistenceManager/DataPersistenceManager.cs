using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{    
    [Header("Debugging")]
    [SerializeField] bool _initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] string _fileName;
    [SerializeField] bool _useEncryption;

    GameData _gameData;
    List<IDataPersistence> _dataPersistenceObjects;
    FileDataHandler _dataHandler;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {        
        if (Instance != null)
        {           
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption); //Tiene que existir previo a la ejecución de OnSceneLoaded.
               
    }
    public GameData GameData { get => _gameData; set => _gameData = value; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this._dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }
        
    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        //Cargar cualquier informacion guardada desde un archivo usando el data handler.
        this._gameData = _dataHandler.Load();

        if(this._gameData == null && _initializeDataIfNull)
        {
            NewGame();
        }

        if (this._gameData == null) return;
        
        foreach(IDataPersistence dataPersistenceObj in _dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(_gameData);
        }
    }

    public void EraseGame()
    {
        this._gameData = null;
    }

    public void SaveGame()
    {
        if (this._gameData == null) return;

        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref _gameData);
        }
       
        //Guardar la información a un archivo usando el data handler.
        _dataHandler.Save(_gameData);
    }

    public string CurrentSceneName()
    {
        return _gameData.sceneToLoad;
    }

    List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();       //Esto lo permite System.Linq. Aquí encontramos todos los objetos que implementen la interfaz "IDataPersistence".
        return new List<IDataPersistence>(dataPersistenceObjects); // Devolvemos la lista de objectos encontrados.
    }

    public bool HasGameData()
    {
        return _gameData != null;
    }
}

/*
OnEnable se ejecuta antes que Start, y OnSceneLoaded se ejecuta justo después de OnEnable, por ello nos suscribimos a los eventos en OnEnable.

*/ 
 