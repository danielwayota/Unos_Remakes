using UnityEngine;

public class TP_PlayerBullet : TP_Bullet
{
    public TP_BulletTypeEnum type;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.gameObject.GetComponent<TP_EnemyController>();
        if (enemy != null)
        {
            if (enemy.type == this.type)
            {
                enemy.Die();
            }
        }

        Destroy(this.gameObject);
    }
}