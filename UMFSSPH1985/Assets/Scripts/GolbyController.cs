using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GolbyController : MonoBehaviour
{
    [SerializeField] TextWriter textWriter;
    [SerializeField] AudioSource clickSound;

    [SerializeField] Sprite[] golbyImages;

    [SerializeField] Text textGO;



    public string[] WelcomeMsg, PotatoExplanationMsg, CrazyFlowerMsg, BebeMsg, MetroMsg, MusicPieceMsg;


    [SerializeField] CanvasGroup cGroup;
    [SerializeField] GameObject GolbyGO;

    [SerializeField] Image GolbyImg;


    public bool isGolbyTalking;

    private void Start()
    {
        StartCoroutine(Welcome(WelcomeMsg));
    }

    public void Write (string message)
    {
        textWriter.AddWriter(textGO, message, 0.1f, true, clickSound);
    }

    public IEnumerator Welcome(string[] messages)
    {
        isGolbyTalking = true;
        yield return new WaitForSeconds(3f);
        GolbyFadeIn();
        yield return new WaitForSeconds(2f);
        Blink();
        Write(messages[0]);
        yield return new WaitForSeconds(2f);
        Write(messages[1]);
        yield return new WaitForSeconds(1f);
        Blink();
        yield return new WaitForSeconds(1f);
        Write(messages[2]);
        yield return new WaitForSeconds(3f);
        Write(messages[3]);
        yield return new WaitForSeconds(2f);
        Blink();
        yield return new WaitForSeconds(4f);
        Write(messages[4]);
        yield return new WaitForSeconds(2f);
        CuteFace();
        yield return new WaitForSeconds(1f);
        GolbyFadeOut();
        yield return new WaitForSeconds(5f);
        ResetText();
        isGolbyTalking = false;
        StartCoroutine(PotatoExplanation(PotatoExplanationMsg));
    }

    public IEnumerator PotatoExplanation(string[] messages)
    {
        isGolbyTalking = true;
        GolbyFadeIn();
        Blink();
        yield return new WaitForSeconds(2f);
        Write(messages[0]);
        yield return new WaitForSeconds(5f);
        Write(messages[1]);
        yield return new WaitForSeconds(7f);
        Blink();
        yield return new WaitForSeconds(4f);
        Write(messages[2]);
        Blink();
        yield return new WaitForSeconds(6f);
        Write(messages[3]);
        CuteFace();
        yield return new WaitForSeconds(1f);
        GolbyFadeOut();
        yield return new WaitForSeconds(2f);
        ResetText();
        isGolbyTalking = false;
    }

    public IEnumerator CrazyFlower(string[] messages)
    {
        isGolbyTalking = true;
        GolbyFadeIn();
        yield return new WaitForSeconds(2f);
        Write(messages[0]);
        yield return new WaitForSeconds(1f);
        CuteFace();
        yield return new WaitForSeconds(3f);
        Write(messages[1]);
        yield return new WaitForSeconds(2f);
        Blink();
        yield return new WaitForSeconds(5f);
        Write(messages[2]);
        yield return new WaitForSeconds(4f);
        Blink();
        yield return new WaitForSeconds(3f);
        Write(messages[3]);
        yield return new WaitForSeconds(5f);
        GolbyFadeOut();
        isGolbyTalking = false;
        yield return new WaitForSeconds(2f);
        ResetText();
    }

    public IEnumerator Bebe(string[] messages)
    {
        isGolbyTalking = true;
        GolbyFadeIn();
        yield return new WaitForSeconds(1f);
        Blink();
        yield return new WaitForSeconds(1f);
        Write(messages[0]);
        yield return new WaitForSeconds(4);
        Blink();
        Write(messages[1]);
        yield return new WaitForSeconds(2f);
        Blink();
        yield return new WaitForSeconds(2f);
        Write(messages[2]);
        yield return new WaitForSeconds(6f);
        Blink();
        yield return new WaitForSeconds(1f);
        GolbyFadeOut();
        isGolbyTalking = false;
        yield return new WaitForSeconds(1f);
        Blink();
        yield return new WaitForSeconds(1f);
        ResetText();
    }

    public IEnumerator Metro(string[] messages)
    {
        yield return new WaitForSeconds(2f);
        isGolbyTalking = true;
        GolbyFadeIn();
        yield return new WaitForSeconds(2f);
        Write(messages[0]);
        yield return new WaitForSeconds(1f);
        Blink();
        yield return new WaitForSeconds(3f);
        Write(messages[1]);
        yield return new WaitForSeconds(4);
        Blink();
        Write(messages[2]);
        yield return new WaitForSeconds(5f);
        Blink();
        yield return new WaitForSeconds(2f);
        GolbyFadeOut();
        isGolbyTalking = false;
        yield return new WaitForSeconds(2f);
        ResetText();
    }

    public IEnumerator MusicPiece(string[] messages)
    {
        yield return new WaitForSeconds(2f);
        isGolbyTalking = true;
        GolbyFadeIn();
        Write(messages[0]);
        yield return new WaitForSeconds(1f);
        Blink();
        yield return new WaitForSeconds(6f);
        Write(messages[1]);
        yield return new WaitForSeconds(4f);
        Blink();
        yield return new WaitForSeconds(3f);
        GolbyFadeOut();
        isGolbyTalking = false;
        yield return new WaitForSeconds(2f);
        ResetText();

    }


    public void ResetText()
    {
        textGO.text = "";
    }

    public void GolbyFadeIn() { StartCoroutine(golbyFadeIn()); }
    public void GolbyFadeOut() { StartCoroutine(golbyFadeOut()); }
    public void Blink() { StartCoroutine(blink()); }
    public void CuteFace() { StartCoroutine(cuteFace()); }



    IEnumerator golbyFadeIn()
    {
        GolbyGO.SetActive(true);
        float duration = 1f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            cGroup.alpha = Mathf.Lerp(0, 1, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    IEnumerator golbyFadeOut()
    {
        float duration = 1f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            cGroup.alpha = Mathf.Lerp(1, 0, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        GolbyGO.SetActive(false);
        yield return null;
    }

    IEnumerator blink()
    {
        GolbyImg.sprite = golbyImages[1];
        yield return new WaitForSeconds(.15f);
        GolbyImg.sprite = golbyImages[2];
        yield return new WaitForSeconds(.15f);
        GolbyImg.sprite = golbyImages[0];
    }

    IEnumerator cuteFace()
    {
        GolbyImg.sprite = golbyImages[3];
        yield return new WaitForSeconds(1.5f);
        GolbyImg.sprite = golbyImages[0];
    }
}
