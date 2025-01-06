using UnityEngine;
using System.Collections;

public class TrainController : MonoBehaviour
{
    [SerializeField] Animator anim;

    MetroController metroController;

    public bool trainMoving;
    public bool trainCountdown;
    void Start()
    {
        //transform.position = Vector2.MoveTowards(new Vector2(-150, -9), new Vector2(-300, -9), -10 * Time.deltaTime);

        //Time.timeScale = 3f;
        metroController = FindObjectOfType<MetroController>();
        anim.SetBool("IdleHidden",true);
        anim.SetBool("IdleVisible",true);
    }

    void Update()
    {

        if (!metroController.outro)
        {
            if (!trainMoving && !trainCountdown)
            {
                trainCountdown = true;
                StartCoroutine(trainTimer());
            }
        }
        else if(metroController.outro)
        {
            anim.SetBool("IdleHidden", false);
            StartCoroutine(MoveTrain());
        }
    }

    IEnumerator MoveTrain()
    {
        anim.SetBool("IdleHidden",false);
        yield return new WaitForSeconds(7);
        anim.SetBool("IdleVisible",false);
        /*yield return new WaitForSeconds(Random.Range(10,15));
        anim.SetBool("IdleVisible",true);
        transform.position = new Vector3(-150, -9, 0);
        anim.SetBool("IdleHidden", true);
        anim.SetBool("IdleVisible", true);
        yield return new WaitForSeconds(Random.Range(3,5));*/
    }

    IEnumerator IdleTrain()
    {
        anim.SetBool("Move",true);
        yield return new WaitForSeconds(2.5f);
        transform.position = new Vector2(-150, -9);
        anim.SetBool("Move", false);

        trainMoving = false;
        /*Debug.Log("test");
        float duration = 4f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            transform.position = new Vector2( Mathf.Lerp(-150, -300, currentTime / duration),-9);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
        //trainMoving = false;*/
    }

    IEnumerator trainTimer()
    {
        yield return new WaitForSeconds(Random.Range(15,20));
        trainMoving = true;
        trainCountdown = false;
        StartCoroutine(IdleTrain());
    }
}
