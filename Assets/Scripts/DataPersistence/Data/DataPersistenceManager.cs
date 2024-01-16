using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;

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
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //TODO - add in loading data from a file once saving to file has been added.
        if (this.gameData == null)
        {
            Debug.Log("No save data. Using default values.");
            NewGame();
        }
        //TODO take that loaded data you just grabbed and send it everywhere.
    }

    public void SaveGame()
    {
        //TODO - pass data to other scripts so they can update

        //TODO - save data to a file using the data handler
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
