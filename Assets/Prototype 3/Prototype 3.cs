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

    private int answers = 0;

    [SerializeField] AudioClip panicAttack;
    [SerializeField] AudioClip eyeDrone;
    [SerializeField] AudioClip wrongButtonPress;

    [SerializeField] UITextbox textbox;
    [SerializeField] UITextButton button;
    [SerializeField] UIChoice choice;
    [SerializeField] Image background;
    [SerializeField] List<GameObject> eyes;

    [SerializeField] Dialogue goodEnd;
    [SerializeField] Dialogue badEnd;

    [SerializeField] Color backgroundTint;
    [SerializeField] Color textboxTint;
    [SerializeField] Color textTint;

    Color[] originalColors = new Color[3];

    private void IncrementAnswers()
    {
        answers++;
    }

    private void Awake()
    {
        current = this;
        UIButton.onPickDialogue += DialogueStart;
        UIButton.onButtonClickInfo += PlayButtonSound;
        UIChoice.onChoiceTrigger += ChoiceTrigger;
    }

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        musicSource = audioSources[0];
        sfxSource = audioSources[1];

        DelayedStartStart();
    }

    private void PlayButtonSound(string buttonID)
    {
        switch (buttonID)
        {
            case "1":
                Debug.Log("Wrong Button Press");
                PlayAudio(sfxSource, wrongButtonPress);
                break;

            case "2":
                Debug.Log("Answer Press");
                // good noise
                IncrementAnswers();
                break;

            case "3":
                Debug.Log("Check Answers - " + answers);
                if (answers > 2)
                {
                    textbox.LoadDialogue(goodEnd);
                }

                else
                {
                    textbox.LoadDialogue(badEnd);
                }
                break;

            default:
                break;
        }
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
        //switch (choiceName)
        //{
        //    case "Choice 13(Clone)":
        //        PlayAudio(sfxSource, wrongButtonPress);
        //        break;
        //    case "Choice 14Clone)":
        //        PlayAudio(sfxSource, wrongButtonPress);
        //        break;
        //}
    }

    public void DialogueStart(Dialogue dialogue)
    {
        if (dialogue != null)
        {
            switch (dialogue.name)
            {
                case "Dialogue 6":
                    PlayAudio(musicSource, eyeDrone);
                    eyes[0].SetActive(true);
                    break;

                case "Dialogue 7":
                    eyes[1].SetActive(true);
                    break;

                case "Dialogue 8":
                    eyes[2].SetActive(true);
                    break;

                case "Dialogue 10":
                    eyes[0].SetActive(false);
                    eyes[1].SetActive(false);
                    eyes[2].SetActive(false);
                    StopAudio(musicSource);
                    break;

                case "Dialogue 13":
                    Tint();
                    StartEyes();
                    PlayAudio(musicSource, panicAttack);
                    break;

                case "Dialogue 20":
                    StopEyes();
                    break;

                case "Dialogue 20B":
                    UndoTint();
                    StopEyes();
                    break;
                case "Dialogue 21":
                    UndoTint();
                    StopAudio(musicSource);
                    break;
                case "Dialogue 22":
                    Close();
                    break;
            }
        }
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