using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;


    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance {get; private set;} //Singleton

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //loads saved data from a file using data handler
        this.gameData = dataHandler.Load();

        //if no data can be loaded, start a new game
        if (this.gameData == null)
        {
            Debug.Log("No save data. Using default values.");
            NewGame();
        }

        //push the loaded data to all scripts that require it
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        
        //pass data to other scripts so they can update
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref gameData);
        }

        //save data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //create a list of all MonoBehaviour objects that also extend IDataPersistence
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
