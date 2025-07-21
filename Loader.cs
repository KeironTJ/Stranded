using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerPrefab;

    private void Awake()
    {
        // Ensure GameManager exists and is persistent
        if (GameManager.Instance == null)
        {
            Instantiate(gameManagerPrefab);
        }

        PlayerProfileManager.LoadProfile();

        // Load player profile (GameManager will handle this in Start)
        // Optionally, you can call PlayerProfileManager.LoadProfile() here if needed

        // Load the Main Menu after setup
        SceneManager.LoadScene("MainMenuScene");
    }


}
