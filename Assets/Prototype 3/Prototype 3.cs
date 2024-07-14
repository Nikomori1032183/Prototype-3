using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Prototype3 : MonoBehaviour
{
    public static Prototype3 current;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    [SerializeField] AudioClip panicAttack;
    [SerializeField] AudioClip eyeDrone;
    [SerializeField] AudioClip wrongButtonPress;

    [SerializeField] UITextButton button;
    [SerializeField] UIChoice choice;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        musicSource = audioSources[0];
        sfxSource = audioSources[1];

        choice.gameObject.SetActive(false);
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
                button.Hide();
                PlayAudio(musicSource, panicAttack);
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
            default:
                break;
        }
    }

    public void SetScrollInterval(float interval)
    {
        // change textbox scroll speed
    }

    public void SetFontSize(int size)
    {
        // change textbox font size
    }

    public void RedTint()
    {
        // tint background and choice buttons
    }

    public void StartEyes()
    {
        // get every object with eye component in scene
        // make them visible
    }

    public void UnavailableOption()
    {
        PlayAudio(sfxSource, wrongButtonPress);
    }
}