using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject ui;
    private float timeCatch = 3.0f;
    private float timer = 0f;
    private float heightCat = 1.35f;
    public float HeightCat => heightCat;

    private void Start()
    {
        ui.SetActive(false);
        slider.value = 0.0f;
        slider.maxValue = timeCatch;
    }
    public void Caught(Action<CatController> OnSuccess)
    {
        ui.SetActive(true);
        timer += Time.deltaTime;
        if(timer > slider.maxValue)
        {
            ui.SetActive(false);
            timer = slider.maxValue;
            OnSuccess?.Invoke(this);
        }
        slider.value = timer;
    }    
}
