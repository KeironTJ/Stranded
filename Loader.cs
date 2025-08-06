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

        // Load the Main Menu after setup
        SceneManager.LoadScene("MainMenuScene");
    }


}
