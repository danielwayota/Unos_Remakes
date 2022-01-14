using UnityEngine;

public class TP_PlayerMissile : TP_PlayerBullet
{
    public float turnSpeed;

    public Transform target;

    private Rigidbody2D body;

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();
        this.body.velocity = this.transform.up * this.speed;
    }

    void Update()
    {
        if (this.target == null)
            return;

        Vector3 targetDirection = this.target.position - this.transform.position;
        this.DoMovement(targetDirection.normalized);
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

        this.body.velocity = this.transform.up * this.speed;
    }
}