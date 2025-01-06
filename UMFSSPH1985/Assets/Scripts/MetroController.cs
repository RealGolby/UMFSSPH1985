using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;

public class MetroController : MonoBehaviour
{
    public bool isInMetro;

    [SerializeField] GameObject TicketMenu;

    [SerializeField] Animator anim;

    [SerializeField] GameObject MetroDarkScreen;

    [SerializeField] GameObject metroObjects;

    [SerializeField] AudioSource music;

    PotatoManager potatoManager;
    GolbyController golbyController;

    [SerializeField] AudioSource ClickSound;

    public bool outro;
    void Start()
    {
        golbyController = FindObjectOfType<GolbyController>();
        potatoManager = FindObjectOfType<PotatoManager>();
    }

    void Update()
    {
        metroDarkScreen();
        hideObjects();


    }
    public void metroFadeIn()
    {
        anim.SetBool("Fade",true);
    }
    public void metroFadeOut()
    {
        anim.SetBool("Fade", false);
    }


    void metroDarkScreen()
    {
        if (isInMetro)
        {
            MetroDarkScreen.SetActive(true);
        }
        else
        {
            MetroDarkScreen.SetActive(false);
        }
    }
    void hideObjects()
    {
        if (isInMetro)
        {
            metroObjects.SetActive(true);
        }
        else
        {
            metroObjects.SetActive(false);
        }
    }

    public void YesBtn()
    {
        if (potatoManager.Money>=1000)
        {
            ClickSound.Play();
            golbyController.isGolbyTalking = true;
            outro = true;
            TicketMenu.SetActive(false);

            StartCoroutine(fadeOutMusic());
            StartCoroutine(loadSceneDelay());
        }
    }

    IEnumerator loadSceneDelay()
    {
        yield return new WaitForSeconds(5f);
        metroFadeIn();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("outro");
    }

    IEnumerator fadeOutMusic()
    {
        float duration = 1f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            music.volume = Mathf.Lerp(1, 0, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public void NoBtn()
    {
        ClickSound.Play();
        TicketMenu.SetActive(false);
    }

}
