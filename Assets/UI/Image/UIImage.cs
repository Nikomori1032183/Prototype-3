using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

[RequireComponent(typeof(Image))]
public class UIImage : UIElement
{
    protected Image image;

    protected virtual void Start()
    {
        image = GetComponent<Image>();
    }

    public override void Display()
    {
        base.Display();

        image.enabled = true;
    }

    public override void Hide()
    {
        base.Hide();

        image.enabled = false;
    }
}
