using UnityEngine;
using System.Collections;

public class TP_EnemyController : MonoBehaviour
{
    public TP_BulletTypeEnum type;

    [Header("Movement")]
    public float forwardMovementSpeed = 4f;

    [Header("Shooting")]
    public float shootTimer = .75f;

    public GameObject bulletPrfb;
    public Transform[] shootPoints;

    [Header("AutoCleanUp")]
    public float autoCleanUpTimer = 2f;
    public float maxAutoCleanUpDistance = 8;
    public Transform autoCleanUpReferencePoint;

    private Rigidbody2D body;

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();

        this.body.velocity = this.transform.up * this.forwardMovementSpeed;

        StartCoroutine(this.ShootingRutine());
        StartCoroutine(this.AutoCleanUpRutine());
    }

    IEnumerator ShootingRutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.shootTimer);

            foreach (var point in this.shootPoints)
            {
                Instantiate(this.bulletPrfb, point.position, point.rotation);
            }
        }
    }

    IEnumerator AutoCleanUpRutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.autoCleanUpTimer);

            float currentDistance = Vector3.Distance(this.autoCleanUpReferencePoint.position, this.transform.position);
            if (currentDistance > this.maxAutoCleanUpDistance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Die()
    {
        // TODO: Partículas o algo
        Destroy(this.gameObject);
    }
}