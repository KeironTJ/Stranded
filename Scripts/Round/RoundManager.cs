using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StrandedDefence.Player;

public class RoundManager : MonoBehaviour
{
    public event System.Action OnTowerDestroyed;

    [Header("Round Settings")]
    [SerializeField] private bool roundActive = true;

    [Header("Tower Spawner")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Wave Settings")]
    [SerializeField] private int currentWave = 1;
    [SerializeField] private float waveDuration = 30f; // Duration of each wave
    [SerializeField] private float waveInterval = 5f; // Time between waves.
    [SerializeField] private float waveEndTime;
    [SerializeField] private float waveStartTime;
    [SerializeField] private bool activeWave = false;

    [Header("Enemy Spawner")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float enemySpawnRateMultiplier = 0.5f;

    // Tower Instance
    private Tower tower;
    private TowerData currentTowerData;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the tower at the start of the round
        currentTowerData = GameManager.Instance.inGameTowerData;
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {

    }


    // ROUND MANAGEMENT
    public void StartRound()
    {
        SpawnTower();
        StartCoroutine(StartWave());
    }

    public void StopRound()
    {
        roundActive = false;
        activeWave = false;
        if (tower != null)
        {
            Destroy(tower.gameObject);
        }

        // Fire the event for any listeners (UI, audio, etc.)
        OnTowerDestroyed?.Invoke();
    }

    // WAVE MANAGEMENT
    private IEnumerator StartWave()
    {
        if (!roundActive) yield break;

        // Start the wave
        roundActive = true;
        waveEndTime = Time.time + waveDuration;
        waveStartTime = Time.time;

        while (Time.time < waveEndTime)
        {
            if (!roundActive) yield break;
            activeWave = true;
            enemySpawner.SpawnEnemy(spawnPoint);
            yield return new WaitForSeconds(1f / EnemiesPerSecond());
        }

        // End the wave
        activeWave = false;
        waveStartTime = Time.time; // Set waveStartTime for the interval period
        yield return new WaitForSeconds(waveInterval);

        // Start the next wave
        currentWave++;
        StartCoroutine(StartWave());
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Max(1, currentWave * enemySpawnRateMultiplier); // Adjust this formula as needed
    }



    // Method to spawn the tower

    public void SpawnTower()
    {
        if (towerPrefab == null)
        {
            Debug.LogError("Tower Prefab is not assigned in the TowerSpawner script.");
            return;
        }

        GameObject towerObj = Instantiate(towerPrefab, spawnPoint.position, Quaternion.identity);
        tower = towerObj.GetComponent<Tower>();
        tower.uiManager = FindObjectOfType<UIManager>(); // Or pass the reference directly if you have it
        tower.roundManager = this; // Pass the RoundManager reference to the tower

        currentWave = 1;
    }



    // GETTERS
    public int GetCurrentWave()
    {
        return currentWave;
    }

    public bool IsActiveWave()
    {
        return activeWave;
    }

    public float GetTimeBetweenWavesRemaining()
    {
        return Mathf.Max(0, waveStartTime + waveInterval - Time.time);
    }

    public float GetWaveInterval()
    {
        return waveInterval;
    }

    public float GetWaveDuration()
    {
        return waveDuration;
    }

    public float GetWaveTimeRemaining()
    {
        return Mathf.Max(0, waveEndTime - Time.time);
    }


}
