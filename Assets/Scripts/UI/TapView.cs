using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class TapView : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject pill;

    void Update()
    {
        TapOnScreen();
    }

    public void TapOnScreen()
    {
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
        GameObject newPill = Instantiate(pill, pos, Quaternion.identity, parent);
        newPill.transform.localScale = Vector3.zero;
        newPill.transform.DOScale(Vector2.one, 0.5f).OnComplete(() =>
        {
            newPill.transform.DOScale(Vector2.zero, 0.5f).OnComplete(() => Destroy(newPill));
        });
    }    
}
