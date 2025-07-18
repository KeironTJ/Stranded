using System;
using System.IO;
using UnityEngine;
using StrandedDefence.Player;

public static class PlayerProfileManager
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "player_profile.json");
    public static PlayerProfile CurrentProfile { get; private set; }

    public static void LoadProfile()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            CurrentProfile = JsonUtility.FromJson<PlayerProfile>(json);
        }
        else
        {
            CurrentProfile = new PlayerProfile();
        }
    }

    public static void SaveProfile()
    {
        string json = JsonUtility.ToJson(CurrentProfile, true);
        File.WriteAllText(SavePath, json);
    }
}