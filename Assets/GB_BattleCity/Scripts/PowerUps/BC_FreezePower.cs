using UnityEngine;

public class BC_FreezePower : MonoBehaviour
{
    public float freezeTime = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var enemy in BC_GameManager.current.enemies)
        {
            enemy.Pause(this.freezeTime);
        }

        Destroy(this.gameObject);
    }

}