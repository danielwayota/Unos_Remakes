using UnityEngine;

public class TP_EnemyBullet : TP_Bullet
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<TP_PlayerShip>();
        if (player != null)
        {
            player.Die();
        }

        Destroy(this.gameObject);
    }
}
