using System.Collections;
using UnityEngine;

public class TileBasedMovement : MonoBehaviour
{
    public Transform[] bumpSensorPoints;
    public float bumpSensorDistance = 0.1f;

    public float tileSize = 1f;
    public float speed = 2f;

    public bool isMoving { get; protected set; }
    public bool isColliding { get; protected set; }

    public LayerMask obstaclesMask;

    void Start()
    {
        this.isMoving = false;
        this.isColliding = false;

        // Redondea al tile más cercano para evitar decimales incorrectos al spawnear.
        var position = this.transform.position;
        position.x = Mathf.Round(position.x / this.tileSize) * this.tileSize;
        position.y = Mathf.Round(position.y / this.tileSize) * this.tileSize;

        this.transform.position = position;
    }

    public void MoveToDirection(Vector2 direction)
    {
        if (this.isMoving)
            return;

        StartCoroutine(this.MovementRutine(direction));
    }

    IEnumerator MovementRutine(Vector3 direction)
    {
        // Check for wall collision.
        foreach (var point in this.bumpSensorPoints)
        {
            var hit = Physics2D.Raycast(point.position, point.up, this.bumpSensorDistance, this.obstaclesMask);

            if (hit.collider != null)
            {
                this.isColliding = true;
                // Exit coroutine.
                yield break;
            }
        }

        this.isColliding = false;
        this.isMoving = true;

        Vector3 origin = this.transform.position;
        Vector3 target = this.transform.position + direction * this.tileSize;

        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * this.speed;

            this.transform.position = Vector3.Lerp(origin, target, percent);
            yield return null;
        }

        this.transform.position = target;

        this.isMoving = false;
    }

    private void OnDrawGizmos()
    {
        foreach (var point in this.bumpSensorPoints)
        {
            Gizmos.DrawLine(point.position, point.position + point.up * this.bumpSensorDistance);
        }
    }
}
