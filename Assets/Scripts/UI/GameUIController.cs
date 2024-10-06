using Imba.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public enum TypeScene
{
    Home,
    RescueRush,
    LevelEditor,
}

public enum TypeViewUI
{
    None,
    E_GameView,
    E_TapView,
    E_GameoverView,
    E_WinView,
}

[Serializable]
public class ViewUI
{
    public UIView uiView;
    public TypeViewUI type;
}

public class GameUIController : ManualSingletonMono<GameUIController>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<ViewUI> views = new();

    public void Init()
    {
        Joystick.Instance.Initialise(canvas);
        ShowView(TypeViewUI.None);
        //Joystick.Instance.DisableControl();
    }

    public UIView GetView(TypeViewUI typeViewUI)
    {
        return views[(int)typeViewUI - 1].uiView;
    }

    public void ShowView(TypeViewUI typeViewUI)
    {
        DisableAllView();
        if (typeViewUI == TypeViewUI.None) return;
        else views.Find(x => x.type == typeViewUI).uiView.gameObject.SetActive(true);
    }

    private void DisableAllView()
    {
        foreach (var view in views)
        {
            view.uiView.gameObject.SetActive(false);
        }
    }

    public void ShowPill(Vector3 pos, float speedUp)
    {
        var view = GetView(TypeViewUI.E_TapView);
        if (view is TapView tapview)
        {
            tapview.ShowPill(pos, speedUp);
        } else
        {
            Debug.Log("Failed.");
        }
    }
}
