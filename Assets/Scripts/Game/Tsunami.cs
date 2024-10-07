using DG.Tweening;
using System;
using UnityEngine;

public class Tsunami : MonoBehaviour
{
    private Action endGame;
    public void Init(Action endGame)
    {
        this.endGame = endGame;
    }

    public void Moving(float timeMoving, Vector3 destination, Action callback)
    {
        this.transform.DOMove(destination, timeMoving).OnComplete(() => callback?.Invoke());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                endGame?.Invoke();
            }
        }
    }
}
