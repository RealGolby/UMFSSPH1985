using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{

    Text uiText;
    string textToWrite;
    float timePerCharacter;
    bool invisibleCharacters;
    AudioSource sound;

    float timer;
    int characterIndex;

    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, AudioSource sound)
    {
        this.invisibleCharacters = invisibleCharacters;
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.sound = sound;
        characterIndex = 0;
    }
    private void Update()
    {
        if (uiText!=null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>"+ textToWrite.Substring(characterIndex) + "</color>";
                }
                uiText.text = text;
                sound.Play();
                if (characterIndex >= textToWrite.Length)
                {
                    uiText = null;
                    return;
                }
            }
        }

    }
}
