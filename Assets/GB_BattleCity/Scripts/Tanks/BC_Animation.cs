using UnityEngine;

public class BC_Animation : MonoBehaviour
{
    public Sprite[] frames;
    public float frameTime = 0.25f;

    public bool autoRun = false;

    private SpriteRenderer gfx;

    private int index = 0;
    private float time = 0;

    void Start()
    {
        this.gfx = this.GetComponent<SpriteRenderer>();
    }

    public void Run()
    {
        this.time += Time.deltaTime;

        if (this.time > this.frameTime)
        {
            this.time = 0;
            this.index = (this.index + 1) % this.frames.Length;

            this.gfx.sprite = this.frames[this.index];
        }
    }

    void Update()
    {
        if (this.autoRun)
        {
            this.Run();
        }
    }
}