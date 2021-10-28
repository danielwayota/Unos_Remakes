using UnityEngine;

public class BC_PlayerTank : BC_BaseTank, BC_IBulletTarget
{
    [Header("Player")]
    public GameObject shieldSprite;

    private float shootCoolDownTimer = 0;
    private TileBasedMovement movement;
    private BC_Animation tankAnimator;

    private bool shieldActive;
    private float shieldTimer;

    void Start()
    {
        this.movement = this.GetComponent<TileBasedMovement>();
        this.tankAnimator = this.GetComponent<BC_Animation>();

        this.shootCoolDownTimer = this.shootCoolDownTimeOut;

        this.ActivateShield(2f);
    }

    void Update()
    {
        if (this.shieldActive)
        {
            this.shieldTimer -= Time.deltaTime;

            if (this.shieldTimer <= 0)
            {
                this.shieldActive = false;
                this.shieldSprite.SetActive(false);
            }
        }

        this.shootCoolDownTimer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            if (this.shootCoolDownTimer >= this.shootCoolDownTimeOut)
            {
                this.shootCoolDownTimer = 0;
                Instantiate(this.bulletPrfb, this.shootPoint.position, this.shootPoint.rotation);
            }
        }

        if (this.movement.isMoving)
        {
            this.tankAnimator.Run();
            return;
        }

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (v != 0)
        {
            if (v > 0)
                this.FaceTo(BC_TankDirection.UP);
            else
                this.FaceTo(BC_TankDirection.DOWN);

            this.movement.MoveToDirection(new Vector2(0, v));
        }
        else if (h != 0)
        {
            if (h > 0)
                this.FaceTo(BC_TankDirection.RIGHT);
            else
                this.FaceTo(BC_TankDirection.LEFT);

            this.movement.MoveToDirection(new Vector2(h, 0));
        }
    }

    public void ActivateShield(float time)
    {
        this.shieldActive = true;
        this.shieldTimer = time;
        this.shieldSprite.SetActive(true);
    }

    public void PerdoneUstedLeHeGolpeado(Vector3 position)
    {
        if (this.shieldActive)
            return;

        BC_GameManager.current.OnPlayerDeath();
        this.ExplodeTank();
    }
}
