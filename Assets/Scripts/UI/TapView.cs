using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapView : UIView
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

    public void ShowPill(Vector3 pos, float speedUp)
    {
        //ShowSlider();
        //GameObject pill = Instantiate(pill, pos, Quaternion.identity, parent);
        GameObject newPill = SimplePool.Spawn(pill, pos, Quaternion.identity);
        newPill.GetComponent<Pill>().SetUp(speedUp);
        newPill.transform.SetParent(parent);
        newPill.transform.localScale = Vector3.zero;
        newPill.transform.DOScale(Vector2.one, 0.5f).OnComplete(() =>
        {
            newPill.transform.DOMove(new Vector3(newPill.transform.position.x, newPill.transform.position.y + 100, newPill.transform.position.z), 1.0f).OnComplete(() =>
            {
                newPill.transform.DOScale(Vector2.zero, 1.5f).OnComplete(() => SimplePool.Despawn(newPill));
            });
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
        if (value <= 0.0f)
            GameUIController.Instance.ShowView(TypeViewUI.E_GameView);
    }
}
