using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class GameView : UIView
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject petHand;
    public Joystick Joystick => joystick;

    public void Initialise(Canvas canvas)
    {
        joystick.Initialise(canvas);
    }

    public void ShowPetHand()
    {

    }    
}
