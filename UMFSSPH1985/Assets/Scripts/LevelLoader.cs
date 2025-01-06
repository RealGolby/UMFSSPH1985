using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;

    public void LoadScene(string sceneName, float duration)
    {
        StartCoroutine(LoadLevel(sceneName, duration));
    }

    IEnumerator LoadLevel(string sceneName, float duration)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(sceneName);
    }
}
