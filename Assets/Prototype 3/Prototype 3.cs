using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VInspector;

public class Prototype3 : MonoBehaviour
{
    public static Prototype3 current;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    [SerializeField] AudioClip panicAttack;
    [SerializeField] AudioClip eyeDrone;
    [SerializeField] AudioClip wrongButtonPress;

    [SerializeField] UITextbox textbox;
    [SerializeField] UITextButton button;
    [SerializeField] UIChoice choice;
    [SerializeField] Image background;
    [SerializeField] List<GameObject> eyes;

    [SerializeField] Color backgroundTint;
    [SerializeField] Color textboxTint;
    [SerializeField] Color textTint;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        musicSource = audioSources[0];
        sfxSource = audioSources[1];
    }

    public void PlayAudio(AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void DialogueStart(Dialogue dialogue)
    {
        switch (dialogue.name)
        {
            case "Dialogue 1":
                button.Hide();
                break;
            case "Dialogue 2":
                button.Hide();
                button.SetText("Explore");
                break;
            case "Dialogue 3":
                button.Hide();
                button.SetText("Walk");
                break;
            case "Dialogue 4":
                button.Hide();
                break;
            case "Dialogue 5":
                choice.gameObject.SetActive(false);
                PlayAudio(musicSource, panicAttack);
                Tint();
                break;
            case "Dialogue 6":

                break;
            case "Dialogue 7":

                break;
            case "Dialogue 8":

                break;
            case "Dialogue 9":

                break;
            case "Dialogue 10":

                break;
            default:
                break;
        }
    }

    public void DialogueEnd(Dialogue dialogue)
    {
        switch (dialogue.name)
        {
            case "Dialogue 1":
                button.Display();
                break;
            case "Dialogue 2":
                button.Display();
                break;
            case "Dialogue 3":
                button.Display();
                break;
            case "Dialogue 4":
                choice.gameObject.SetActive(true);
                break;
            case "Dialogue 5":
                choice.gameObject.SetActive(true);
                break;
            case "Dialogue 6":
                
                break;
            case "Dialogue 7":
                
                break;
            case "Dialogue 8":
                
                break;
            case "Dialogue 9":
                
                break;
            case "Dialogue 10":

                break;
            default:
                break;
        }
    }

    public void SetScrollInterval(float interval)
    {
        textbox.scrollInterval = interval;
    }

    public void SetFontSize(int size)
    {
        textbox.textUI.textComponent.fontSize = size;
    }

    public void Tint()
    {
        background.color = backgroundTint;
        textbox.image.color = textboxTint;
        textbox.textUI.textComponent.color = textTint;
    }

    [Button]
    public void StartEyes()
    {
        StartCoroutine(DisplayEyes());
    }

    public IEnumerator DisplayEyes()
    {
        foreach (GameObject eye in eyes)
        {
            yield return new WaitForSeconds(0.5f);

            eye.SetActive(true);
        }
    }

    [Button]
    public void StopEyes()
    {
        foreach (GameObject eye in eyes)
        {
            eye.SetActive(false);
        }
    }

    public void UnavailableOption()
    {
        PlayAudio(sfxSource, wrongButtonPress);
    }
}