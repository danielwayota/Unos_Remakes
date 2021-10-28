using UnityEngine;

public class BC_BaseTank : MonoBehaviour
{
    [Header("Base Tank")]
    public GameObject explosionPrfb;

    public GameObject bulletPrfb;

    public Transform shootPoint;

    public float shootCoolDownTimeOut = 1f;

    protected BC_TankDirection currentDirection = BC_TankDirection.UP;

    protected void FaceTo(BC_TankDirection newDirection)
    {
        if (this.currentDirection == newDirection) { return; }

        this.currentDirection = newDirection;

        switch (this.currentDirection)
        {
            case BC_TankDirection.UP:
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case BC_TankDirection.DOWN:
                this.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case BC_TankDirection.LEFT:
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case BC_TankDirection.RIGHT:
                this.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
    }

    protected void ExplodeTank()
    {
        if (this.explosionPrfb != null)
        {
            Instantiate(this.explosionPrfb, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}