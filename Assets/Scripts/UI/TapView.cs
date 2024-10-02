using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;

public class TapView : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject pill;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text txtNumberEnery;
    private int maxFill = 96;
    private int numberFill = 96;
    private Action OnPlay;

    public void Init(int maxFill, Action OnPlay)
    {
        this.OnPlay = OnPlay;
        this.maxFill = maxFill;
        this.numberFill = maxFill;
        txtNumberEnery.text = $"{numberFill}/{maxFill}";
        slider.value = 1.0f;
    }

    void Update()
    {
        TapOnScreen();
    }

    public void TapOnScreen()
    {
        if (numberFill <= 0)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = touch.position;
                ShowPill(touchPosition);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            ShowPill(mousePosition);
        }
    }

    private void ShowPill(Vector3 pos)
    {
        ShowSlider();
        GameObject newPill = Instantiate(pill, pos, Quaternion.identity, parent);
        newPill.transform.localScale = Vector3.zero;
        newPill.transform.DOScale(Vector2.one, 0.5f).OnComplete(() =>
        {
            newPill.transform.DOScale(Vector2.zero, 0.5f).OnComplete(() => Destroy(newPill));
        });
    }

    private void ShowSlider()
    {
        float value = 0.0f;
        if (numberFill >= 16)
        {
            numberFill -= 16;
            value = (float)numberFill / (float)maxFill;
        }    
        slider.value = value;
        txtNumberEnery.text = $"{numberFill}/{maxFill}";
        if(value <= 0.0f)
            GameUIController.Instance.ShowView(TypeViewUI.E_GameView);
    }
}
