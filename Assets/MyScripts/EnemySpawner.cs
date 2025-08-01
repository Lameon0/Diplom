using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Префаб врага
    public Transform[] spawnPoints; // Массив точек для спауна врагов
    public int numberOfEnemiesToSpawn = 5; // Количество врагов для создания
    public float spawnInterval = 1f; // Интервал между созданием врагов

    private bool spawningEnemies = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !spawningEnemies)
        {
            StartCoroutine(SpawnEnemiesWithInterval());
        }
    }

    private IEnumerator SpawnEnemiesWithInterval()
    {
        spawningEnemies = true;

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            // Выбираем случайную точку из массива точек спауна
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Создаем врага в выбранной точке
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Ждем заданный интервал перед созданием следующего врага
            yield return new WaitForSeconds(spawnInterval);
        }

        spawningEnemies = false;
        Destroy(gameObject);
    }
}
