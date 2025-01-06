using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroController : MonoBehaviour
{
    public AudioSource music;

    public Image fadeImg;
    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(fadeMusicOut());
        StartCoroutine(fadeScreen());
        StartCoroutine(endGame());
    }

    IEnumerator fadeMusicOut()
    {
        yield return new WaitForSeconds(25f);
        float duration = 3f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            music.volume = Mathf.Lerp(1, 0, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    IEnumerator fadeScreen()
    {
        yield return new WaitForSeconds(23f);
        float duration = 3f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            fadeImg.color = new Color(0,0,0, Mathf.Lerp(0, 1, currentTime / duration)) ;
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene("MainMenu");
    }
}
