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

    private UIChoice currentChoice;

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

    Color[] originalColors = new Color[3];

    private void Awake()
    {
        current = this;
        UIButton.onPickDialogue += DialogueStart;
        UIChoice.onChoiceTrigger += ChoiceTrigger;
    }

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        musicSource = audioSources[0];
        sfxSource = audioSources[1];

        DelayedStartStart();
    }

    public void NextDialogue()
    {
 
    }

    private void StopAudio(AudioSource source)
    {
        source.Stop();
    }

    public void PlayAudio(AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void ChoiceTrigger(string choiceName)
    {
        switch (choiceName)
        {
            case "3A(Clone)":
                SetScrollInterval(0.07f);
                break;

            case "4A(Clone)":
                SetScrollInterval(0.05f);
                break;

            case "3B(Clone)":
                SetScrollInterval(0.07f);
                break;

            case "4B(Clone)":
                SetScrollInterval(0.05f);
                break;

            case "6A(Clone)":
                SetScrollInterval(0.03f);
                PlayAudio(musicSource, panicAttack);
                Tint();
                break;

            case "6B(Clone)":
                SetScrollInterval(0.03f);
                PlayAudio(musicSource, panicAttack);
                Tint();
                break;

            case "8A(Clone)":
                SetScrollInterval(0.05f);
                break;

            case "8B(Clone)":
                SetScrollInterval(0.05f);
                break;

            case "9A(Clone)":
                PlayAudio(musicSource, eyeDrone);
                SetFontSize(64);
                SetScrollInterval(0.02f);
                StartEyes();
                break;

            case "9B(Clone)":
                PlayAudio(musicSource, eyeDrone);
                SetFontSize(48);
                SetScrollInterval(0.02f);
                StartEyes();
                break;

            case "10A(Clone)":
                SetFontSize(32);
                SetScrollInterval(0.03f);
                break;

            case "10B(Clone)":
                SetFontSize(32);
                SetScrollInterval(0.03f);
                break;

            case "12A(Clone)":
                StopEyes();
                Close();
                break;

            case "12B(Clone)":
                StopEyes();
                Close();
                break;

            default:
                break;
        }
    }

    public void DialogueStart(Dialogue dialogue)
    {

    }

    public void DialogueEnd(Dialogue dialogue)
    {
        if (dialogue.choice != null)
        {
            currentChoice = Instantiate(dialogue.choice, GameObject.FindWithTag("Canvas").transform);
        }
    }

    private IEnumerator Close()
    {
        yield return new WaitForSeconds(3);
        Application.Quit();
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
        originalColors[0] = background.color;
        originalColors[1] = textbox.image.color;
        originalColors[2] = textbox.textUI.textComponent.color;

        background.color = backgroundTint;
        textbox.image.color = textboxTint;
        textbox.textUI.textComponent.color = textTint;
    }

    private void UndoTint()
    {
        background.color = originalColors[0];
        textbox.image.color = originalColors[1];
        textbox.textUI.textComponent.color = originalColors[2];
    }

    [Button]
    public void DelayedStartStart()
    {
        StartCoroutine(DelayedStart());
    }

    public IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.5f);
        textbox.UpdateText();
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