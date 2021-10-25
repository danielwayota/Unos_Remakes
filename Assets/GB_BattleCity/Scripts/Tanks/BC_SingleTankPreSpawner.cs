using UnityEngine;

public class BC_SingleTankPreSpawner : MonoBehaviour
{
    public GameObject tankPrefab;

    void Start()
    {
        Invoke("SpawnAndDestroy", 1.5f);
    }

    private void SpawnAndDestroy()
    {
        Instantiate(this.tankPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
