using UnityEngine;

public class BC_SingleTankPreSpawner : MonoBehaviour
{
    public GameObject tankPrefab;
    public float time = 1.5f;

    void Start()
    {
        Invoke("SpawnAndDestroy", this.time);
    }

    private void SpawnAndDestroy()
    {
        Instantiate(this.tankPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
