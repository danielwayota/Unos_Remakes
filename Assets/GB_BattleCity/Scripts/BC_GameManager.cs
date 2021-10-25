using System.Collections.Generic;
using UnityEngine;

public class BC_GameManager : MonoBehaviour
{
    // Estas variables son estáticas para que se compartan entre niveles.
    public static int lifes = 3;
    public static int currentPlayerTankLevel = 0;

    // Esta es estática para que sea accesible por todos los demás scripts.
    public static BC_GameManager current;

    public GameObject[] playerTankLevelPrefabs;
    public GameObject[] powerUpPrefabs;

    public Transform playerSpawnPoint;

    protected int currentEnemyCount = 0;

    public List<BC_EnemyTank> enemies { get; protected set; }

    private GameObject currentPlayerGO;

    void Awake()
    {
        current = this;
        this.enemies = new List<BC_EnemyTank>();
    }

    void Start()
    {
        BC_HUD.current.SetLifes(lifes);
        Invoke("Respawn", 1f);
    }

    void Respawn()
    {
        var playerTankPrefab = this.playerTankLevelPrefabs[currentPlayerTankLevel];
        this.currentPlayerGO = Instantiate(playerTankPrefab, this.playerSpawnPoint.position, Quaternion.identity);
    }

    public void RegisterEnemy(BC_EnemyTank enemy)
    {
        this.enemies.Add(enemy);
    }

    public void OnPlayerDeath()
    {
        lifes --;
        BC_HUD.current.SetLifes(lifes);

        currentPlayerTankLevel = 0;

        if (lifes > 0)
        {
            Invoke("Respawn", 1f);
        }
        else
        {
            // TODO: GameOver screen.
            Debug.Log("Game Over");
        }
    }

    public void OnEnemyDeath(BC_EnemyTank enemy)
    {
        this.SetTargetEnemyCount(this.currentEnemyCount - 1);
        this.enemies.Remove(enemy);
    }

    public void SetTargetEnemyCount(int count)
    {
        this.currentEnemyCount = count;
        BC_HUD.current.SetEnemyCount(count);
    }

    public void OneUp()
    {
        lifes++;
        BC_HUD.current.SetLifes(lifes);
    }

    public void GetNextPlayerTank()
    {
        currentPlayerTankLevel++;

        if (currentPlayerTankLevel >= this.playerTankLevelPrefabs.Length)
        {
            // Ya estamos en el máximo nivel.
            currentPlayerTankLevel = this.playerTankLevelPrefabs.Length - 1;
            return;
        }

        // Subimos de nivel, por lo tanto, obtenemos un tanque más poderoso.

        // Intercambiamos el nuevo tanque con el anterior.
        var playerTankPrefab = this.playerTankLevelPrefabs[currentPlayerTankLevel];
        var tmp = Instantiate(playerTankPrefab, this.currentPlayerGO.transform.position, this.currentPlayerGO.transform.rotation);

        Destroy(this.currentPlayerGO);

        this.currentPlayerGO = tmp;
    }

    public void SpawnRandomPowerUp(Vector3 location)
    {
        int index = Random.Range(0, this.powerUpPrefabs.Length);
        Instantiate(this.powerUpPrefabs[index], location, Quaternion.identity);
    }

}
