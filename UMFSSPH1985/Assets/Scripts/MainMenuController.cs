using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Animator buttonAnim;
    [SerializeField] Animator AudioAnim;
    [SerializeField]LevelLoader ll;
    [SerializeField] AudioSource buttonClick;

    [SerializeField] Button playButton, settingsButton, exitButton;

    [SerializeField] Texture2D mouseCursor;

    [SerializeField] AudioMixer audioMixer;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void Exit()
    {
        exitButton.enabled = false;
        buttonClick.Play();
        Application.Quit();
        Debug.Log("Game Exit");
    }

    public void Settings()
    {
        settingsButton.enabled = false;   
        buttonClick.Play();
        Debug.Log("Settings");
    }

    public void Play()
    {
        playButton.enabled = false;
        buttonClick.Play();
        AudioAnim.SetTrigger("FadeOut");
        buttonAnim.SetTrigger("FadeOut");
        ll.LoadScene("Intro",2f);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        //AudioListener.volume = volume;
    }
}
