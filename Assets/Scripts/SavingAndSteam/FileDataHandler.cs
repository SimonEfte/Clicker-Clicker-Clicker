using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private readonly string backUpExtension = ".bak"; //(New)

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileId, bool allowRestoreFromBackup = true) //(New) profileId is new, bool is new
    {
        // Use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);//(NEW) ProfileId is new
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e) //(New)
            {
                if (allowRestoreFromBackup)
                {
                    Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess)
                    {
                        loadedData = Load(profileId, false);
                    }
                }
                else
                {
                    Debug.Log("Error occured when trying to load file at path: " + fullPath + " and backup did not work.\n" + e);
                }
            }
        }
        return loadedData;
    }

    public void Save(GameData data, string profileId) //(New) profileId is new
    {
        // Use Path.Combine to account for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName); //(New) profileId is new
        string backupFilePath = fullPath + backUpExtension; //(New)
        try
        {
            if (DataPersistenceManager.saveIncrement == 1)
            {
                int increment = 0;
                increment += 1;

                // create the directory the file will be written to if it doesnt already exist
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                //Serialize the C# game data object into Json
                string dataToStore = JsonUtility.ToJson(data, true);

                // write the serialized data to the file
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            if (DataPersistenceManager.saveIncrement == 2)
            {
                GameData verifiedGameData = Load(profileId); //(New)
                if (verifiedGameData != null)
                {
                    File.Copy(fullPath, backupFilePath, true);
                }
                else
                {
                    throw new Exception("Save file could not be verified and backup could not be created.");
                }

                DataPersistenceManager.saveIncrement = 0;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public static bool hadToBackup;

    private bool AttemptRollback(string fullPath) //(New) all is new
    {
        bool success = false;
        string backUpFilePath = fullPath + backUpExtension;
        try
        {
            if (File.Exists(backUpFilePath))
            {
                hadToBackup = true;
                File.Copy(backUpFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Had to roll back to backup file at: " + backUpFilePath);
            }
            else
            {
                throw new Exception("Tried to roll back, but to backup file exists to roll back to");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when tryinh to roll back to backup file at: " + backUpFilePath + "\n" + e);
        }

        return success;
    }
}
