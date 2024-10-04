using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    [SerializeField] private TMP_Text txtLevel;

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        int level = DataGameSave.GetLevel();
        txtLevel.text = "Level " + level.ToString();
    }  
    
    public void BTN_Race()
    {
        SceneManager.LoadScene(TypeScene.RescueRush.ToString());
    }
}
