using UnityEngine;
using System.Collections;

public class TP_PlayerShip : MonoBehaviour
{
    public GameObject destroyEffect;

    public float turnSpeed = 2f;
    public float forwardSpeed = 2f;

    public GameObject primaryBulletPrfb;
    public GameObject secondaryBulletPrfb;

    public Transform shootPoint;

    public TP_PlayerTargetReticule targetReticule;
    public float autoTargetRadius = 1f;
    public LayerMask whatIsTarget;

    private Rigidbody2D body;

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();

        this.body.velocity = this.transform.up * this.forwardSpeed;

        StartCoroutine(this.AutoTargetRutine());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(this.primaryBulletPrfb, this.shootPoint.position, this.shootPoint.rotation);

        if (Input.GetButtonDown("Fire2"))
        {
            var go = Instantiate(this.secondaryBulletPrfb, this.shootPoint.position, this.shootPoint.rotation);
            var misile = go.GetComponent<TP_PlayerMissile>();

            misile.target = this.targetReticule.target;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            Vector2 inputDirection = (new Vector2(h, v)).normalized;
            this.DoMovement(inputDirection);
        }
    }

    void DoMovement(Vector2 inputDirection)
    {
        Vector2 currentDirection = this.transform.up;

        float angleDifference = Vector3.Angle(currentDirection, inputDirection);

        Vector2 finalDirection;
        if (angleDifference > 5f)
            finalDirection = (currentDirection + inputDirection * this.turnSpeed * Time.deltaTime).normalized;
        else
            finalDirection = inputDirection;

        float angle = Mathf.Atan2(finalDirection.y, finalDirection.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        this.body.velocity = this.transform.up * this.forwardSpeed;
    }

    IEnumerator AutoTargetRutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);

            Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, this.autoTargetRadius, this.whatIsTarget);
            TP_EnemyController nearest = null;
            float lastDistance = 9999;

            foreach (var e in enemies)
            {
                var tmp = e.GetComponent<TP_EnemyController>();

                if (tmp.type == TP_BulletTypeEnum.PRIMARY)
                {
                    continue;
                }

                float distance = (tmp.transform.position - this.transform.position).sqrMagnitude;
                if (distance < lastDistance)
                {
                    nearest = tmp;
                    lastDistance = distance;
                }
            }

            if (nearest != null)
            {
                this.targetReticule.Follow(nearest.transform);
            }
        }
    }

    public void Die()
    {
        Instantiate(this.destroyEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);

        TP_Manager.current.Respawn();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.autoTargetRadius);
    }
}
