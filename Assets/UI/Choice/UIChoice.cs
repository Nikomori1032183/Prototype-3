using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class UIChoice : DebugToggleComponent
{
    [Tab("UIChoice")]
    LayoutGroup layoutGroup;
    public List<UIButton> buttons = new List<UIButton>();

    [SerializeField] bool destroy = false;

    private void Start()
    {
        layoutGroup = GetComponent<LayoutGroup>();

        foreach (UIButton button in GetComponentsInChildren<UIButton>())
        {
            buttons.Add(button);
        }
    }

    public void ChoicePicked()
    {
        DebugMessage("Choice Picked");

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
