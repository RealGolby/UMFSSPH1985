using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroController : MonoBehaviour
{
    [SerializeField]AudioSource clickSound;
    [SerializeField] TextWriter textWriter;
    [SerializeField] LevelLoader ll;

    [SerializeField]Text text;

    [SerializeField]Animator anim;


    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite[] golbySprites;


    [SerializeField] string message1, message2, message3, message4, message5;

    private void Start()
    {
        Cursor.visible = false;
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText()
    {
        yield return new WaitForSeconds(1f);
        textWriter.AddWriter(text, message1, 0.1f,true, clickSound);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Blink());
        yield return new WaitForSeconds(3f);
        textWriter.AddWriter(text, message2, 0.1f,true, clickSound);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Blink());
        yield return new WaitForSeconds(7f);
        StartCoroutine(Blink());
        yield return new WaitForSeconds(1f);
        textWriter.AddWriter(text, message3, 0.1f, true, clickSound);
        yield return new WaitForSeconds(5f);
        textWriter.AddWriter(text, message4, 0.1f, true, clickSound);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Blink());
        yield return new WaitForSeconds(4f);
        textWriter.AddWriter(text, message5, 0.1f, true, clickSound);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Blink());
        anim.enabled = false;
        yield return new WaitForSeconds(1f);
        sr.sprite = golbySprites[3];
        yield return new WaitForSeconds(1f);
        text.text = "";
        ll.LoadScene("MainScene",2f);
        yield return new WaitForSeconds(.2f);
        sr.sprite = golbySprites[0];
    }

    IEnumerator Blink()
    {
        anim.SetBool("Blink", true);
        yield return new WaitForSeconds(.5f);
        anim.SetBool("Blink", false);
    }

}
