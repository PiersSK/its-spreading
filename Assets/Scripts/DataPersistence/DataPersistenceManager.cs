using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.ComponentModel.Design;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;


    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance {get; private set;} //Singleton

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Manager in the scene.");
        }
        Instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        TimeController.Instance.DayOver += OnDayComplete;
        LoadGame();
    }

    private void OnDayComplete(object sender, System.EventArgs e)
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void SaveNewDayToFile()
    {
        this.gameData = new GameData(gameData.spreadEventsTriggered, gameData.daysComplete);
        gameData.dayIsComplete = true;
        dataHandler.Save(gameData);
    }

    private void NewDay(GameData oldData)
    {
        this.gameData = new GameData(oldData.spreadEventsTriggered, oldData.daysComplete);
    }

    public void LoadGame()
    {
        //loads saved data from a file using data handler
        this.gameData = dataHandler.Load();
        bool doIntroAnimation = false;

        //if no data can be loaded, start a new game
        if (this.gameData == null)
        {
            Debug.Log("No save data. Using default values.");
            doIntroAnimation = true;
            NewGame();
        } else if (gameData.dayIsComplete)
        {
            Debug.Log("Complete save data loaded, resetting non-historical values");
            doIntroAnimation = true;
            NewDay(gameData);
        }

        //push the loaded data to all scripts that require it
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }

        if (doIntroAnimation)
        {
            StartGame.Instance.StartDayFresh();
        } else
        {
            Player.Instance.ForcePlayerToPosition(gameData.playerPosition);
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
