using UnityEngine;

public class BC_TankUpPower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BC_GameManager.current.GetNextPlayerTank();
        Destroy(this.gameObject);
    }
}
