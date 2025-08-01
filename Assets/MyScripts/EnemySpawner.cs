using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �����
    public Transform[] spawnPoints; // ������ ����� ��� ������ ������
    public int numberOfEnemiesToSpawn = 5; // ���������� ������ ��� ��������
    public float spawnInterval = 1f; // �������� ����� ��������� ������

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
            // �������� ��������� ����� �� ������� ����� ������
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // ������� ����� � ��������� �����
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // ���� �������� �������� ����� ��������� ���������� �����
            yield return new WaitForSeconds(spawnInterval);
        }

        spawningEnemies = false;
        Destroy(gameObject);
    }
}
