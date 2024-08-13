using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VInspector;

public class UITextbox : UIImage
{
    public UIText textUI;

    [Tab("Textbox")]
    public Dialogue currentDialogue;
    [SerializeField] bool scroll = true;
    public float scrollInterval = 0.05f;
    [EndTab]

    protected override void Start()
    {
        base.Start();
        textUI = GetComponentInChildren<UIText>();
        UIButton.onPickDialogue += LoadDialogue;
        //UpdateText();
    }

    public void UpdateText()
    {
        textUI.textComponent.text = "";

        Prototype3.current.DialogueStart(currentDialogue);


        if (currentDialogue != null)
        {
            if (scroll)
            {
                StartCoroutine(ScrollingText(currentDialogue.text));
            }

            else
            {
                textUI.textComponent.text = currentDialogue.text;
                Prototype3.current.DialogueEnd(currentDialogue);
            }
        }
    }

    private IEnumerator ScrollingText(string text)
    {
        foreach (char character in text)
        {
            textUI.textComponent.text += character;
            yield return new WaitForSeconds(scrollInterval);
        }

        Prototype3.current.DialogueEnd(currentDialogue);
    }

    [Button]
    public void LoadDialogue(Dialogue dialogue)
    {
        if (dialogue != null)
        {
            this.currentDialogue = dialogue;
            UpdateText();
        }

        else
        {
            this.currentDialogue = currentDialogue.nextDialogue;
            UpdateText();
        }
    }

    [Button]
    public void NextDialogue()
    {
        if (currentDialogue.nextDialogue != null)
        {
            currentDialogue = currentDialogue.nextDialogue;
            UpdateText();
        }
    }

    [Button]
    public void PreviousDialogue()
    {
        if (currentDialogue.previousDialogue != null)
        {
            currentDialogue = currentDialogue.previousDialogue;
            UpdateText();
        }
    }
}