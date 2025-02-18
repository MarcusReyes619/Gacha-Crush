using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;
    public static DataPersistenceManager instance { get; private set; }

    private GameData gameData;
    private List<IDataPersistence> dataPersistencesObjs;
    private FileDataHandler dataHandler;
    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More then one Data Persistence Manager in scene.");
        }
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            NewGame();
        }

        foreach(IDataPersistence dataPerObj in dataPersistencesObjs)
        {
            dataPerObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPerObj in dataPersistencesObjs)
        {
            dataPerObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistencesObjs = FindAllDataPersistenceObj();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObj()
    {
        IEnumerable<IDataPersistence> dataPerObj = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistence>();


        return new List<IDataPersistence>(dataPerObj);
    }



}
