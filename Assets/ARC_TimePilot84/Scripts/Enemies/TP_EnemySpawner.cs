using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct TP_EnemyConfg
{
    public GameObject prefb;
    public int chanceNumber;
}

public class TP_EnemySpawner : MonoBehaviour
{
    public TP_EnemyConfg[] enemyConfs;

    public float spawnTimeMin = 0.1f;
    public float spawnTimeMax = 1f;

    private List<int> indexPool;

    void Start()
    {
        this.indexPool = new List<int>();

        for (int enemyConfIndex = 0; enemyConfIndex < this.enemyConfs.Length; enemyConfIndex++)
        {
            var conf = this.enemyConfs[enemyConfIndex];

            for (int j = 0; j < conf.chanceNumber; j++)
            {
                this.indexPool.Add(enemyConfIndex);
            }
        }

        // Randomizamos la pool de enemigos.
        int lastIndex = this.indexPool.Count - 1;

        for (int i = 0; i < lastIndex; i++)
        {
            int other = Random.Range(i, lastIndex);

            // Swap
            var tmp = this.indexPool[other];
            this.indexPool[other] = this.indexPool[i];
            this.indexPool[i] = tmp;
        }

        StartCoroutine(SpawnRutine());
    }

    IEnumerator SpawnRutine()
    {
        while (true)
        {
            float waitTime = Random.Range(this.spawnTimeMin, this.spawnTimeMax);
            yield return new WaitForSeconds(waitTime);

            // Spawn
            // Los puntos de spawn son los hijos del objeto Spawner.
            int spawnPointIndex = Random.Range(0, this.transform.childCount);
            Transform spawnPoint = this.transform.GetChild(spawnPointIndex);

            int poolIndex = Random.Range(0, this.indexPool.Count);
            int prefabIndex = this.indexPool[poolIndex];

            GameObject prefab = this.enemyConfs[prefabIndex].prefb;

            var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            enemy.GetComponent<TP_EnemyController>().autoCleanUpReferencePoint = this.transform;
        }
    }
}
