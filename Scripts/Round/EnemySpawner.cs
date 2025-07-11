using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawner")]
    [SerializeField] private GameObject[] enemyPrefabs;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SpawnEnemy(Transform spawnPoint)
    {
        GameObject enemyPrefab = GetEnemyPrefab();
        if (enemyPrefab == null)
        {
            Debug.LogError("No enemy prefab assigned for the current wave.");
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Spawn and initialize the enemy
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = enemyInstance.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.Initialize(spawnPoint); // Assuming spawnPoint is the target tower
        }
        else
        {
            Debug.LogError("Enemy script not found on the spawned enemy prefab.");
        }

    }

    private GameObject GetEnemyPrefab()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            return null;

        int index = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[index];
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Get screen boundaries
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        // Randomly choose a side to spawn from (left, right, top, bottom)
        int side = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0: // Left
                spawnPosition = new Vector3(screenBottomLeft.x - 1, Random.Range(screenBottomLeft.y, screenTopRight.y), 0);
                break;
            case 1: // Right
                spawnPosition = new Vector3(screenTopRight.x + 1, Random.Range(screenBottomLeft.y, screenTopRight.y), 0);
                break;
            case 2: // Top
                spawnPosition = new Vector3(Random.Range(screenBottomLeft.x, screenTopRight.x), screenTopRight.y + 1, 0);
                break;
            case 3: // Bottom
                spawnPosition = new Vector3(Random.Range(screenBottomLeft.x, screenTopRight.x), screenBottomLeft.y - 1, 0);
                break;
        }

        return spawnPosition;
    }
}
