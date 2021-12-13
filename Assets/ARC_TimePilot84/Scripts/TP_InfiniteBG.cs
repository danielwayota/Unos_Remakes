using UnityEngine;
using System.Collections;

public class TP_InfiniteBG : MonoBehaviour
{
    public Transform target;

    public float verticalMargin;
    public float horizontalMargin;

    void Start()
    {
        StartCoroutine(this.UpdateRutine());
    }

    IEnumerator UpdateRutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            var center = this.transform.position;
            var targetPos = this.target.position;

            // Checkeo vertical
            float upMargin = center.y + this.verticalMargin;
            float downMargin = center.y - this.verticalMargin;
            if (targetPos.y > upMargin)
            {
                center.y += this.verticalMargin * 2f;
            }
            else if (targetPos.y < downMargin)
            {
                center.y -= this.verticalMargin * 2f;
            }

            // Checkeo horizontal
            float rightMargin = center.x + this.horizontalMargin;
            float leftMargin = center.x - this.horizontalMargin;

            if (targetPos.x > rightMargin)
            {
                center.x += this.horizontalMargin * 2f;
            }
            else if (targetPos.x < leftMargin)
            {
                center.x -= this.horizontalMargin * 2f;
            }

            // Se aplica el movimiento final
            this.transform.position = center;
            yield return null;

        }
    }

    private void OnDrawGizmos()
    {
        var center = this.transform.position;
        Gizmos.DrawLine(
            center + Vector3.up * this.verticalMargin,
            center + Vector3.down * this.verticalMargin
        );

        Gizmos.DrawLine(
            center + Vector3.right * this.horizontalMargin,
            center + Vector3.left * this.horizontalMargin
        );
    }
}
