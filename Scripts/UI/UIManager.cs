using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Wave UI")]
    [SerializeField] private GameObject waveUI;
    [SerializeField] private TMPro.TextMeshProUGUI waveText;
    [SerializeField] private Slider waveslider;
    [SerializeField] private Image fillAreaImage;

    [Header("Tower UI")]
    [SerializeField] private GameObject towerUI;
    [SerializeField] private TMPro.TextMeshProUGUI towerHealthText;
    [SerializeField] private Slider towerHealthSlider;

    [Header("Managers")]
    [SerializeField] private RoundManager roundManager;

    private void Update()
    {
        if (roundManager != null)
        {
            UpdateWaveUI();
        }
    }


    public void UpdateWaveUI()
    {
        // Update the wave text with the current wave number
        int currentWave = roundManager.GetCurrentWave();
        waveText.text = "Wave: " + currentWave;

        if (roundManager.IsActiveWave())
        {
            float timeRemaining = roundManager.GetWaveTimeRemaining();
            //waveTimeRemainingUI.text = $"Time Left: {timeRemaining:F1}s";
            waveslider.value = 1 - (timeRemaining / roundManager.GetWaveDuration());
            fillAreaImage.color = Color.cyan;
        }
        else
        {
            float timeRemaining = roundManager.GetTimeBetweenWavesRemaining();
            //waveTimeRemainingUI.text = $"Next Wave In: {timeRemaining:F1}s";
            waveslider.value = 1 - (timeRemaining / roundManager.GetWaveInterval());
            fillAreaImage.color = Color.red;
        }
    }
    
    public void UpdateTowerUI(Tower tower)
    {
        towerHealthText.text = "Health: " + NumberManager.FormatLargeNumber(tower.currentHealth);
        towerHealthSlider.value = tower.currentHealth / tower.maxHealth; // Assuming maxHealth is defined in Tower
    }
}
