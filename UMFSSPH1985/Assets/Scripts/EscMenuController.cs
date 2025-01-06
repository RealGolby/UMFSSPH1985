using UnityEngine;

public class EscMenuController : MonoBehaviour
{
    [SerializeField] GameObject EscMenu;
    [SerializeField] GameObject Slovacik;
    [SerializeField] AudioSource Music;

    [SerializeField] AudioSource ClickSound;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        ClickSound.Play();
        EscMenu.SetActive(true);
        Time.timeScale = 0;
        Music.Pause();
    }

    public void CloseMenu()
    {
        ClickSound.Play();
        EscMenu.SetActive(false);
        Time.timeScale = 1;
        Music.UnPause();
    }

    public void RespawnPlayer()
    {
        ClickSound.Play();
        Slovacik.transform.position = new Vector3(-20,-10,0);
        CloseMenu();
    }

    public void QuitGame()
    {
        ClickSound.Play();
        Application.Quit();
    }
}
