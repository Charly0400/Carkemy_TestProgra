using System.Collections.Generic;
using UnityEngine;

//Gestiona la generaci�n y eliminaci�n de enemigos en un �rea espec�fica.
public class EnemySpawner : MonoBehaviour
{
    #region Variables

    [Header("SPAWNER")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;     
    public int maxEnemies = 5;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    #endregion

    #region Unity Methods
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

    #endregion

    #region Spawn Methods

    //Instancia enemigos en puntos de spawn aleatorios hasta alcanzar un n�mero m�ximo.
    private void SpawnEnemies()
    {
        for (int i = spawnedEnemies.Count; i < maxEnemies; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }
    //Destruye todos los enemigos generados cuando el jugador sale del �rea.
    private void ClearEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();
    }

    #endregion
}
