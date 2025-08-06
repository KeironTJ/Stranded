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
        //ebug.Log("Loading player profile from: " + SavePath);
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            CurrentProfile = JsonUtility.FromJson<PlayerProfile>(json);
        }
        else
        {
            CurrentProfile = new PlayerProfile();
        }

        if (CurrentProfile.masterTower == null)
        {
            //Debug.LogWarning("masterTower was null after loading profile. Rebuilding...");
            CurrentProfile.masterTower = CurrentProfile.BuildTowerDataFromResources();
        }

        CurrentProfile.UpgradeTowerDataToLatest(CurrentProfile.masterTower);

        // Log important fields for validation
        //Debug.Log($"Profile loaded. PlayerName: {CurrentProfile.playerName}, masterTower null? {CurrentProfile.masterTower == null}");

        SaveProfile(); // Ensure we save the profile after loading or creating it

    }

    public static void SaveProfile()
    {
        string json = JsonUtility.ToJson(CurrentProfile, true);
        File.WriteAllText(SavePath, json);
        //Debug.Log("Player profile saved to: " + SavePath);
    }
}