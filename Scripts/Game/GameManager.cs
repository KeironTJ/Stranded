using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StrandedDefence.Player;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TowerData inGameTowerDataBackingField;

    public TowerData inGameTowerData
    {
        get => inGameTowerDataBackingField;
        private set => inGameTowerDataBackingField = value;
    }


    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayerProfileManager.LoadProfile();
    }

    public void StartRound()
    {
        // Assign the player's masterTower to inGameTowerData before loading the round scene
        var profile = PlayerProfileManager.CurrentProfile;
        if (profile != null)
        {
            inGameTowerData = profile.masterTower;
        }
        else
        {
            return;
        }

        // Load the round scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoundScene");
    }


}