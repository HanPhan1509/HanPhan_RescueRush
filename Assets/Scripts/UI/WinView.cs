using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinView : UIView
{
    public void BTN_Next()
    {
        DataGameSave.SetLevel(DataGameSave.GetLevel() + 1);
        SceneManager.LoadScene(TypeScene.Home.ToString());
    }
}
