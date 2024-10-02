using Imba.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public enum TypeViewUI
{
    None,
    E_GameView,
    E_TapView,
}

[Serializable]
public class ViewUI
{
    public TypeViewUI type;
    public GameObject view;
}

public class GameUIController : ManualSingletonMono<GameUIController>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<ViewUI> views = new();

    public void Init()
    {
        Joystick.Instance.Initialise(canvas);
        //Joystick.Instance.DisableControl();
    }

    public void ShowView(TypeViewUI typeViewUI)
    {
        foreach(var view in views)
        {
            if (view.type == typeViewUI)
                view.view.SetActive(true);
            else
                view.view.SetActive(false);
        }
    }
}
