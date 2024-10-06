using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class GameView : UIView
{
    [SerializeField] private Joystick joystick;
    public Joystick Joystick => joystick;

    public void Initialise(Canvas canvas)
    {
        joystick.Initialise(canvas);
    }
}
