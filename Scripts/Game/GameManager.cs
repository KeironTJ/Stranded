using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StrandedDefence.Player;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TowerData inGameTowerData { get; private set; }

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

        // Clone the master tower for this round
        inGameTowerData = PlayerProfileManager.CurrentProfile.masterTower.Clone();
        
        // Load the round scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("RoundScene");
    }


}