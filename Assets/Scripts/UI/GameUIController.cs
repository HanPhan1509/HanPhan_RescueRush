using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameView gameView;

    private void Start()
    {
        gameView.Initialise(canvas);
    }
}
