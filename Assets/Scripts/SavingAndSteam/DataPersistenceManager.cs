using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameDataJSON;
    private List<IDataPersistence> dataPersistenceObjects;
    //private string selectedProfileId = "test"; //(New)

    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Update()
    {
        if (FileDataHandler.hadToBackup == true)
        {
            //testingObject.SetActive(true);
            FileDataHandler.hadToBackup = false;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameDataJSON = new GameData();
    }

    public GameObject testingObject;
    public Animation saveAnim;

    public void LoadGame()
    {
        this.gameDataJSON = dataHandler.Load("", true);  //(NEW) string

        if (this.gameDataJSON == null)
        {
            Debug.Log("No data was found. Initialzing data to defaults");
            NewGame();
        }

        foreach (IDataPersistence dataPersistanceObj in dataPersistenceObjects)
        {
            dataPersistanceObj.LoadData(gameDataJSON);
        }

        //Debug.Log("Loaded all variables");
    }

    public GameObject saveGamePopUp;
    public static int saveIncrement;

    public void SaveGame()
    {
        PlayerPrefs.SetString("OfflineProgression", DateTime.Now.ToString());
        saveIncrement += 1;
        if (clickSave == true) { clickSave = false; saveIncrement = 1; }

        saveAnim.Play();

        foreach (IDataPersistence dataPErsistenceObj in dataPersistenceObjects)
        {
            dataPErsistenceObj.SaveData(ref gameDataJSON);
        }

        MainCursorClick.didSave = true;
        dataHandler.Save(gameDataJSON, ""); //(NEW) string
    }

    private void OnApplicationQuit()
    {
        clickSave = true;
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool clickSave;

    public void SaveTheGameData()
    {
        clickSave = true;
        SaveGame();
    }
}
