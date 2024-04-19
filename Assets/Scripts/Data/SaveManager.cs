using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void SaveGame(PlayerData playerData)
    {
        var bf = new BinaryFormatter();
        playerData = new PlayerData();

        // Checking if the file exists. If it does, it will delete it.
        if (CheckSaveData())
            ClearSaveData(Application.persistentDataPath + "/MySaveData.dat");
        
        var file = File.Create(Application.persistentDataPath + "/MySaveData.dat");

        bf.Serialize(file, playerData);
        file.Close();
        
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    // Save Game function overload that saves data into a default file.
    public static void SaveGame()
    {
        var bf = new BinaryFormatter();

        // Checking if the file exists. If it does, it will delete it.
        if (CheckSaveData()) ClearSaveData(Application.persistentDataPath + "/MySaveData.dat");
        var file = File.Create(Application.persistentDataPath + "/MySaveData.dat");

        var playerData = new PlayerData();
        bf.Serialize(file, playerData);
        file.Close();
        
        PlayerPrefs.Save();
        
        Debug.Log("Game data saved!");
    }

    public static PlayerData LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            var data = (PlayerData)bf.Deserialize(file);
            file.Close();

            Debug.Log("Game data loaded!");
            return data;
        }

        Debug.LogError("There is no save data!");
        return null;
    }

    public static void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
        {
            Debug.LogError("No save data to delete.");
        }
    }

    private static bool CheckSaveData()
    {
        if (!File.Exists(Application.persistentDataPath + "/MySaveData.dat")) return false;
        Debug.Log("Save data already exists!");
        return true;
    }

    private static bool ClearSaveData(string path)
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
            return true;
        }

        Debug.LogError("No save data to delete.");
        return false;
    }
}