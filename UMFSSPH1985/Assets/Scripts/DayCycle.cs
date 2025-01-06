using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public enum Cycle { day, sunset, night, sunrise };

    [SerializeField] GameObject daynight;

    public float dayLenght = 800f;

    public Cycle cycle;

    bool rotate;

    [SerializeField] Light2D globalLight;
    float currentIntensity = 1.2f;

    void Start()
    {
        rotate = true;
        cycle = Cycle.day;
        StartCoroutine(Day());
    }
    private void Update()
    {
        if (rotate)
        {
            StartCoroutine(rotateIndicator());
        }

    }

    IEnumerator Day()
    {
        cycle = Cycle.day;
        Debug.Log(cycle);
        StartCoroutine(ChangeLight(1.2f));
        yield return new WaitForSeconds(dayLenght/2.5f);

        cycle = Cycle.sunset;
        Debug.Log(cycle);
        StartCoroutine(ChangeLight(.8f));
        yield return new WaitForSeconds(dayLenght/10f);

        cycle = Cycle.night;
        Debug.Log(cycle);
        StartCoroutine(ChangeLight(.3f));
        yield return new WaitForSeconds(dayLenght / 2.5f);

        cycle = Cycle.sunrise;
        Debug.Log(cycle);
        StartCoroutine(ChangeLight(.8f));
        yield return new WaitForSeconds(dayLenght / 10f);
        StartCoroutine(Day());
    }

    IEnumerator rotateIndicator()
    {
        rotate = false;
        Debug.Log("Start rotate");
        Vector3 startRotation = daynight.transform.eulerAngles;
        float endRotation = startRotation.z - 360.0f;
        float t = 0.0f;
        while (t < dayLenght)
        {
                t += Time.deltaTime;
                float zRotation = Mathf.Lerp(startRotation.z, endRotation, t / dayLenght) % 360.0f;
                daynight.transform.eulerAngles = new Vector3(startRotation.x, startRotation.y, zRotation);
            //Debug.Log(Mathf.Abs(zRotation));
                yield return null;
        }
        Debug.Log("End rotate");
        rotate = true;
    }

    IEnumerator ChangeLight(float targetIntensity)
    {
        float duration = 20f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            globalLight.intensity = Mathf.Lerp(currentIntensity, targetIntensity, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        currentIntensity = targetIntensity;
        yield break;
    }
}
