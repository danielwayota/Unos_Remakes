using UnityEngine;

public class BC_ShieldPower : MonoBehaviour
{
    public float time = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerTank = other.GetComponent<BC_PlayerTank>();
        if (playerTank != null)
        {
            playerTank.ActivateShield(this.time);
        }

        Destroy(this.gameObject);
    }
}
