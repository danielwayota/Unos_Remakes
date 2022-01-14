using UnityEngine;

public class TP_PlayerTargetReticule : MonoBehaviour
{
    public Transform target { get; protected set; }

    private float timer = 0;
    private float timeOut = 3;

    public void Follow(Transform newTarget)
    {
        this.target = newTarget;
        this.gameObject.SetActive(true);

        this.timer = this.timeOut;
    }

    void Update()
    {
        this.timer -= Time.deltaTime;

        if (this.target == null || this.timer <= 0)
        {
            this.gameObject.SetActive(false);
            return;
        }

        this.transform.position = this.target.position;
    }
}