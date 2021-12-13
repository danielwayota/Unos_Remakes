using UnityEngine;

public class TP_PlayerShip : MonoBehaviour
{
    public float turnSpeed = 2f;
    public float forwardSpeed = 2f;

    public GameObject primaryBulletPrfb;

    public Transform shootPoint;

    private Rigidbody2D body;

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();

        this.body.velocity = this.transform.up * this.forwardSpeed;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(this.primaryBulletPrfb, this.shootPoint.position, this.shootPoint.rotation);

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

    public void Die()
    {
        // TODO: Partículas o algo
        Destroy(this.gameObject);
    }
}
