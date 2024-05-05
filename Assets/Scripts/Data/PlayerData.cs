using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public static int Level
    {
        get => PlayerPrefs.GetInt("Level", 1);
        set => PlayerPrefs.SetInt("Level", value);
    }

    public static int Coins
    {
        get => PlayerPrefs.GetInt("Coins", 0);
        set => PlayerPrefs.SetInt("Coins", value);
    }

    public static int Gems
    {
        get => PlayerPrefs.GetInt("Gems", 0);
        set => PlayerPrefs.SetInt("Gems", value);
    }

    public static int Data
    {
        get => PlayerPrefs.GetInt("Data", 0);
        set => PlayerPrefs.SetInt("Data", value);
    }

    public static int MaxHealth
    {
        get => PlayerPrefs.GetInt("MaxHealth", 100);
        set => PlayerPrefs.SetInt("MaxHealth", value);
    }
}