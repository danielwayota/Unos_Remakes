using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BC_EnemyWave
{
    public GameObject prefab;
    public int count;
}

public class BC_EnemySpawner : MonoBehaviour
{
    public GameObject preSpawnPrefab;

    public BC_EnemyWave[] waves;
    public Transform[] spawnPoints;

    public float minWaitTime = 1f;
    public float maxWaitTime = 2f;

    public float obstacleDetectionRadius = 1f;

    private int waveIndex;

    void Start()
    {
        this.waveIndex = 0;

        int enemyTankSum = 0;
        foreach (var wave in this.waves)
        {
            enemyTankSum += wave.count;
        }
        BC_GameManager.current.SetTargetEnemyCount(enemyTankSum);

        StartCoroutine(this.SpawnRutine());
    }

    IEnumerator SpawnRutine()
    {
        bool done = false;

        while (!done)
        {
            if (this.waveIndex >= this.waves.Length)
            {
                done = true;
                continue;
            }

            var currentWave = this.waves[this.waveIndex];

            if (currentWave.count == 0)
            {
                this.waveIndex++;
                continue;
            }

            if (BC_GameManager.current.enemies.Count > 3)
            {
                yield return new WaitForSeconds(this.minWaitTime);
                continue;
            }

            var point = this.spawnPoints[Random.Range(0, this.spawnPoints.Length)];

            // Se comprueba si hay un obstáculo en este punto
            var collider = Physics2D.OverlapCircle(point.position, this.obstacleDetectionRadius);

            // No hay un obstáculo.
            if (collider == null)
            {
                var preSpawner = Instantiate(this.preSpawnPrefab, point.position, Quaternion.identity);
                preSpawner.GetComponent<BC_SingleTankPreSpawner>().tankPrefab = currentWave.prefab;

                currentWave.count--;
            }

            float time = Random.Range(this.minWaitTime, this.maxWaitTime);
            yield return new WaitForSeconds(time);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var point in this.spawnPoints)
        {
            Gizmos.DrawWireSphere(point.position, this.obstacleDetectionRadius);
        }
    }
}
