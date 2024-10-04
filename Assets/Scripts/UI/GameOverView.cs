using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : UIView
{
    public void Revive()
    {
        SceneManager.LoadScene(TypeScene.RescueRush.ToString());
    }    
}
