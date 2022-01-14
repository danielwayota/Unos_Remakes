using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TP_Manager : MonoBehaviour
{
    public static TP_Manager current
    {
        get => GameObject.FindObjectOfType<TP_Manager>();
    }

    public void Respawn()
    {
        StartCoroutine(this.RespawnRutine());
    }

    IEnumerator RespawnRutine()
    {
        Time.timeScale = .25f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(.5f);

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (sceneLoad.isDone == false)
        {
            yield return null;
        }
    }
}
