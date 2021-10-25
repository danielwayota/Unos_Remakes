using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BC_Bullet : MonoBehaviour
{
    public float speed = 2f;

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

        Destroy(this.gameObject);
    }
}