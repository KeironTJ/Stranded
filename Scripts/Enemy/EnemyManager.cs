using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoundManager roundManager; // Reference to the RoundManager
    
    [Header("Enemy Manager Settings")]
    [SerializeField] private int maxEnemies = 20; // Maximum number of enemies allowed

    private List<Enemy> activeEnemies = new List<Enemy>();

    private void Start()
    {
        roundManager.OnTowerDestroyed += ClearAllEnemies;
    }

    public bool CanSpawn()
    {
        return activeEnemies.Count < maxEnemies;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
    }

    // Call this on game over or restart to remove all enemies from the scene
    public void ClearAllEnemies()
    {
        foreach (var enemy in new List<Enemy>(activeEnemies))
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        activeEnemies.Clear();
    }
}