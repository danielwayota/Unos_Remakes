using UnityEngine;

public class TP_Bullet : MonoBehaviour
{
    public float speed = 4f;

    void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * this.speed;
    }
}