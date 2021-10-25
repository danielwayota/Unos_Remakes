using UnityEngine;
using UnityEngine.UI;

public class BC_HUD : MonoBehaviour
{
    public static BC_HUD current;

    public Text lifesLabel;
    public Text enemyCountLabel;

    void Awake()
    {
        current = this;
    }

    public void SetLifes(int count)
    {
        this.lifesLabel.text = count.ToString();
    }

    public void SetEnemyCount(int count)
    {
        this.enemyCountLabel.text = count.ToString();
    }
}
