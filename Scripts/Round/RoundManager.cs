using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField] private bool roundActive = true;


    [Header("Tower Spawner")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Wave Settings")]
    [SerializeField] private int currentWave = 1;
    [SerializeField] private float waveDuration = 30f; // Duration of each wave
    [SerializeField] private float waveInterval = 5f; // Time between waves.
    private float waveEndTime;
    private float waveStartTime;

    [Header("Enemy Spawner")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float enemySpawnRateMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the tower at the start of the round
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRound()
    {
        SpawnTower();
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        if (!roundActive) yield break;

        // Start the wave
        roundActive = true;
        waveEndTime = Time.time + waveDuration;
        waveStartTime = Time.time;

        // Update spawn ratios for the current wave
        // UpdateSpawnRatios(); TODO: Implement this method to adjust spawn ratios based on wave number
        //SpawnBoss(); // TODO: Spawns boss at start of round

        while (Time.time < waveEndTime)
        {
            if (!roundActive) yield break;
            enemySpawner.SpawnEnemy(spawnPoint);
            yield return new WaitForSeconds(1f / EnemiesPerSecond());
        }

        // End the wave
        waveStartTime = Time.time; // Set waveStartTime for the between-waves period
        yield return new WaitForSeconds(waveInterval);

        // Start the next wave
        currentWave++;
        StartCoroutine(StartWave());
    }

    public void SpawnTower()
    {
        if (towerPrefab == null)
        {
            Debug.LogError("Tower Prefab is not assigned in the TowerSpawner script.");
            return;
        }

        Instantiate(towerPrefab, spawnPoint.position, Quaternion.identity);
        currentWave = 1;
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Max(1, currentWave * enemySpawnRateMultiplier); // Adjust this formula as needed
    }


}
