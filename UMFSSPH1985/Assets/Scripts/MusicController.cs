using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioSource musicHolder;

    [SerializeField] AudioClip[] clips;

    [SerializeField] GameObject cd1, cd2;

    PotatoManager potatoManager;

    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] AudioSource audioSource;

    [SerializeField] GameObject musicButton;

    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    [SerializeField] TextMeshProUGUI currentlyPlaying;

    [SerializeField] bool boost;

    [SerializeField] Image boostBtnImg;

    [SerializeField] AudioSource MusicGainSound;

    [SerializeField] AudioSource ClickSound;
    void Start()
    {
        addMusic(0);
        PlayMusic(0);
        audioSource.Play();
        potatoManager = FindObjectOfType<PotatoManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            toggleMusicUI();
        }
    }

    public void addMusic(int musicIndex)
    {

        buttons[musicIndex].GetComponent<Image>().color = Color.white;
        buttons[musicIndex].GetComponent<Button>().interactable = true;
        if (musicIndex != 0)
        {
            MusicGainSound.Play();
        }
    }

    public void PlayMusic(int musicIndex)
    {
            audioSource.clip = clips[musicIndex];
            currentlyPlaying.text = clips[musicIndex].name;
            audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        if (volume != -40)
        {
            boost = false;
            boostBtnImg.color = Color.white;
            audioMixer.SetFloat("volume", volume);
        }
        else
        {
            boost = false;
            boostBtnImg.color = Color.white;
            audioMixer.SetFloat("volume", -80);
        }
        //AudioListener.volume = volume; - //global volume
    }

    public void StartPlaying()
    {
        ClickSound.Play();
        audioSource.UnPause();
    }

    public void StopPlaying()
    {
        ClickSound.Play();
        audioSource.Pause();
    }

    public void BoostMusic()
    {
        if (!boost)
        {
            ClickSound.Play();
            musicVolumeSlider.value = 0;
            boostBtnImg.color = Color.red;
            boost = true;
            audioMixer.SetFloat("volume", 20);
        }
        else if (boost)
        {
            ClickSound.Play();
            boostBtnImg.color = Color.white;
            boost = false;
            audioMixer.SetFloat("volume", 0);
        }
    }

    //cd shop
    public void buyCD1()
    {
        if (potatoManager.Money >= 350)
        {
            ClickSound.Play();
            PlayMusic(1);
            potatoManager.Money -= 350;
            addMusic(1);
            cd1.SetActive(false);
        }
        else
        {
            //play error sound
        }
    }

    public void buyCD2()
    {
        if (potatoManager.Money >= 250)
        {
            ClickSound.Play();
            PlayMusic(3);
            potatoManager.Money -= 250;
            addMusic(3);
            cd2.SetActive(false);
        }
        else
        {
            //play error sound
        }
    }


    //Music UI
    public void toggleMusicUI()
    {
        if (musicButton.activeInHierarchy)
        {
            ClickSound.Play();
            musicButton.SetActive(false);
        }
        else
        {
            ClickSound.Play();
            musicButton.SetActive(true);
        }
    }

}
