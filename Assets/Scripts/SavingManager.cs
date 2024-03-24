using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.InputSystem.EnhancedTouch;

[Serializable]
public class SaveData
{
    public int level;
    public int coins;
    public int gem;
    public int da;
}

public static class SavingManager
{
    public static void SaveGame(int level, int coins, int gem, int da)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat"); 
        SaveData data = new SaveData();
        data.level = level;
        data.coins = coins;
        data.gem = gem;
        data.da = da;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game datasaved!");
    }

    static SaveData LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData) bf.Deserialize(file);
            file.Close();
            return data;

            Debug.Log("Game dataloaded!");
        }
        else
            Debug.LogError("There is no save data!");

        return null;
    }

    public static int LoadLevel()
    {
        SaveData data = LoadGame();
        return data.level;
    }

    public static int LoadCoins()
    {
        SaveData data = LoadGame();
        return data.coins;
    }

    public static int LoadGem()
    {
        SaveData data = LoadGame();
        return data.gem;
    }

    public static int LoadData()
    {
        SaveData data = LoadGame();
        return data.da;
    }


    static void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat"); 
            
            Debug.Log("Datareset complete!");
        }
        else
            Debug.LogError("No save data todelete.");
    }

}
