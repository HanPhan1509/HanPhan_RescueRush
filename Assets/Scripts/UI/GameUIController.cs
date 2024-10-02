using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public enum TypeViewUI
{
    E_GameView,
    E_TapView,
}

[Serializable]
public class ViewUI
{
    public TypeViewUI type;
    public GameObject view;
}

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<ViewUI> views = new();
    [SerializeField] private GameView gameView;
    [SerializeField] private TapView tapView;

    private void Start()
    {
        gameView.Initialise(canvas);
        Joystick.Instance.DisableControl();
    }

    private void ShowView(TypeViewUI typeViewUI)
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
