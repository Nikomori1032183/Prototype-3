using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIText : UIElement
{
    public Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }
}
