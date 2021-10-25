using UnityEngine;

public class BC_BombPower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        int index = BC_GameManager.current.enemies.Count - 1;

        while (index >= 0)
        {
            var enemy = BC_GameManager.current.enemies[index];
            enemy.PerdoneUstedLeHeGolpeado(Vector3.zero);

            index--;
        }

        Destroy(this.gameObject);
    }
}
