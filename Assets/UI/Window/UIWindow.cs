using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VInspector;

public class UIWindow : UIImage
{
    private LayoutGroup layoutGroup;

    new protected void Start()
    {
        base.Start();
        layoutGroup = GetComponent<LayoutGroup>();
    }
}