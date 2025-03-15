using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 5;
    public Transform[] spawnPoints;     

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnEnemies();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ClearEnemies();
        }
    }

    private void SpawnEnemies()
    {
        for (int i = spawnedEnemies.Count; i < maxEnemies; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    private void ClearEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();
    }
}
