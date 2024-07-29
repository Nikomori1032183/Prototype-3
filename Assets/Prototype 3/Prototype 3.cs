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
    }

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        musicSource = audioSources[0];
        sfxSource = audioSources[1];
        UIButton.onPickDialogue += DialogueStart;
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

    // show dialogue
    // 

    public void DialogueStart(Dialogue dialogue)
    {
        Destroy(currentChoice);
        //switch (dialogue.name)
        //{
        //    case "Dialogue 1": // Start
        //        button.Hide();
        //        break;
        //    case "Dialogue 2":
        //        button.Hide();
        //        button.SetText("Explore");
        //        break;
        //    case "Dialogue 3":
        //        button.Hide();
        //        button.SetText("Walk");
        //        break;
        //    case "Dialogue 4":
        //        button.Hide();
        //        SetScrollInterval(0.1f);
        //        break;
        //    case "Dialogue 5": // Panic
        //        choice.gameObject.SetActive(false);
        //        SetScrollInterval(0.03f);
        //        PlayAudio(musicSource, panicAttack);
        //        Tint();
        //        UITextButton option1 = (UITextButton)choice.buttons[0];
        //        option1.SetText("Calm Down");
        //        UITextButton option2 = (UITextButton)choice.buttons[1];
        //        option2.SetText("PANIC");
        //        break;
        //    case "Dialogue 6":
        //        choice.gameObject.SetActive(false);
        //        break;
        //    case "Dialogue 7":
        //        choice.gameObject.SetActive(false);
        //        SetScrollInterval(0.05f);
        //        break;
        //    case "Dialogue 8":
        //        choice.gameObject.SetActive(false); // Eyes
        //        PlayAudio(musicSource, eyeDrone);
        //        SetFontSize(64);
        //        SetScrollInterval(0.02f);
        //        StartEyes();
        //        break;
        //    case "Dialogue 9":
        //        choice.gameObject.SetActive(false);
        //        SetFontSize(32);
        //        SetScrollInterval(0.03f);
        //        break;
        //    case "Dialogue 10":
        //        choice.gameObject.SetActive(false);
        //        break;
        //    case "Dialogue 11": // End
        //        choice.gameObject.SetActive(false);
        //        StopEyes();
        //        UndoTint();
        //        break;
        //    default:
        //        break;
        //}
    }

    public void DialogueEnd(Dialogue dialogue)
    {
        currentChoice = Instantiate(dialogue.choice, GameObject.FindWithTag("Canvas").transform);
        
        //switch (dialogue.name)
        //{
        //    case "Dialogue 1":
        //        button.Display();
        //        break;
        //    case "Dialogue 2":
        //        button.Display();
        //        break;
        //    case "Dialogue 3":
        //        button.Display();
        //        break;
        //    case "Dialogue 4":
        //        choice.gameObject.SetActive(true);
        //        break;
        //    case "Dialogue 5":
        //        choice.gameObject.SetActive(true);
        //        break;
        //    case "Dialogue 6":
        //        choice.gameObject.SetActive(true);
        //        break;
        //    case "Dialogue 7":
        //        choice.gameObject.SetActive(true);
        //        break;
        //    case "Dialogue 8":
        //        choice.gameObject.SetActive(true);
        //        break;
        //    case "Dialogue 9":
        //        choice.gameObject.SetActive(true);
        //        break;
        //    case "Dialogue 10":
        //        choice.gameObject.SetActive(true);
        //        break;
        //    case "Dialogue 11":
        //        StartCoroutine(Close());
        //        break;
        //    default:
        //        break;
        //}
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