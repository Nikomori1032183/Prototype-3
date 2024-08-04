using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class UIChoice : DebugToggleComponent
{
    [Tab("UIChoice")]
    public List<UIButton> buttons = new List<UIButton>();
    [SerializeField] bool destroy = false;
    [EndTab]

    LayoutGroup layoutGroup;

    private void Start()
    {
        layoutGroup = GetComponent<LayoutGroup>();

        foreach (UIButton button in GetComponentsInChildren<UIButton>())
        {
            buttons.Add(button);
        }
    }

    public static event Action<string> onChoiceTrigger;
    public void ChoiceTrigger()
    {
        onChoiceTrigger(gameObject.name);
    }

    public void ChoicePicked()
    {
        DebugMessage("Choice Picked");

        onChoiceTrigger(gameObject.name);

        if (destroy)
        {
            Destroy(gameObject);
        }

        else
        {
            gameObject.SetActive(false);
        }
    }
}
