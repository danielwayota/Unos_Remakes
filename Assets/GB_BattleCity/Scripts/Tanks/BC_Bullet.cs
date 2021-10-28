using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BC_Bullet : MonoBehaviour
{
    public float speed = 2f;
    public GameObject explosionPrefb;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = this.transform.up * this.speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var target = other.gameObject.GetComponent<BC_IBulletTarget>();

        if (target != null)
        {
            target.PerdoneUstedLeHeGolpeado(this.transform.position);
        }

        if (this.explosionPrefb != null)
        {
            Instantiate(this.explosionPrefb, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}