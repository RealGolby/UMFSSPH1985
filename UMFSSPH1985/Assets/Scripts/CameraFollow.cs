using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]Transform playerPosition;

    [SerializeField] MetroController metroController;

    private Camera cam;

    float targetZoom = 15;
    float zoomFactor = 20f;
    float zoomLerpSpeed = 10f;

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = Mathf.Lerp(15, 10, Time.deltaTime);
    }

    private void Update()
    {
        if (!metroController.isInMetro)
        {
            Zoom();
        }

    }

    void Zoom()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 4f, 20f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }

    void FixedUpdate()
    {
        if (!metroController.isInMetro)
        {
            transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, -10);
        }
        else if (metroController.isInMetro)
        {
            transform.position = new Vector3(-200,0,-10);
            Camera.main.orthographicSize = 15;
        }

    }
}
