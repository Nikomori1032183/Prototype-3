using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VInspector;

public class UITextbox : DebugToggleComponent
{
    UIText textUI;

    [Tab("Textbox")]
    [SerializeField] Dialogue dialogue;
    [SerializeField] bool scroll = true;
    [SerializeField] float scrollInterval = 0.05f;
    [EndTab]

    void Start()
    {
        textUI = GetComponentInChildren<UIText>();
        UpdateText();
    }

    private void UpdateText()
    {
        textUI.textComponent.text = "";

        Prototype3.current.DialogueStart(dialogue);

        if (scroll)
        {
            StartCoroutine(ScrollingText(dialogue.text));
        }

        else
        {
            textUI.textComponent.text = dialogue.text;
            Prototype3.current.DialogueEnd(dialogue);
        }
    }

    private IEnumerator ScrollingText(string text)
    {
        foreach (char character in text)
        {
            textUI.textComponent.text += character;
            yield return new WaitForSeconds(scrollInterval);
        }

        Prototype3.current.DialogueEnd(dialogue);
    }

    [Button]
    public void NextDialogue()
    {
        if (dialogue.nextDialogue != null)
        {
            dialogue = dialogue.nextDialogue;
            UpdateText();
        }
    }

    [Button]
    public void PreviousDialogue()
    {
        if (dialogue.previousDialogue != null)
        {
            dialogue = dialogue.previousDialogue;
            UpdateText();
        }
    }
}