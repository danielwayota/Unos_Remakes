using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float followSpeed = 2f;

    public Vector3 offset;

    void Update()
    {
        if (this.target == null)
            return;

        this.transform.position = Vector3.Lerp(
            this.transform.position,
            this.target.position + this.offset,
            Time.deltaTime * this.followSpeed
        );
    }
}
