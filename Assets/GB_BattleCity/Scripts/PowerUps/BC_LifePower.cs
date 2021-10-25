using UnityEngine;

public class BC_LifePower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BC_GameManager.current.OneUp();
        Destroy(this.gameObject);
    }
}