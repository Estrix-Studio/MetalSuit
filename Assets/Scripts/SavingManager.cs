using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SavingManager
{
    public static void SaveGame(PlayerData playerData)
    {
        var bf = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/MySaveData.dat");

        bf.Serialize(file, playerData);
        file.Close();
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

    public static void SaveGame()
    {
    }
}