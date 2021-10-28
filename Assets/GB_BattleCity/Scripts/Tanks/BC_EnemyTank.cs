using UnityEngine;
using System.Collections;

public class BC_EnemyTank : BC_BaseTank, BC_IBulletTarget
{
    [Header("Enemy Tank")]
    public float health;

    public float powerUpDropChance = 0;

    private TileBasedMovement movement;
    private float pauseTimer;

    private BC_Animation tankAnimator;

    // Start is called before the first frame update
    void Start()
    {
        this.pauseTimer = 0;

        this.movement = this.GetComponent<TileBasedMovement>();
        this.tankAnimator = this.GetComponent<BC_Animation>();

        StartCoroutine(this.ShootingRoutine());

        BC_GameManager.current.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.pauseTimer > 0)
        {
            this.pauseTimer -= Time.deltaTime;
            return;
        }

        if (this.movement.isMoving)
        {
            this.tankAnimator.Run();
            return;
        }

        if (this.movement.isColliding)
        {
            // Cambiar a dirección aleatoria.
            int directionNumber = Random.Range(0, 4);

            switch (directionNumber)
            {
                case 0:
                    this.FaceTo(BC_TankDirection.UP);
                    break;
                case 1:
                    this.FaceTo(BC_TankDirection.DOWN);
                    break;
                case 2:
                    this.FaceTo(BC_TankDirection.LEFT);
                    break;
                case 3:
                    this.FaceTo(BC_TankDirection.RIGHT);
                    break;
            }
        }

        this.movement.MoveToDirection(this.transform.up);
    }

    public void Pause(float time)
    {
        this.pauseTimer = time;
    }

    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.shootCoolDownTimeOut);
            Instantiate(this.bulletPrfb, this.shootPoint.position, this.shootPoint.rotation);
        }
    }

    public void PerdoneUstedLeHeGolpeado(Vector3 position)
    {
        this.health --;

        if (this.health <= 0)
        {
            float dice = Random.Range(0f, 1f);
            if (dice < this.powerUpDropChance)
            {
                BC_GameManager.current.SpawnRandomPowerUp(this.transform.position);
            }

            BC_GameManager.current.OnEnemyDeath(this);

            this.ExplodeTank();
        }
    }
}
