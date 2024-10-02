using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class RRController : MonoBehaviour
{
    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject prefPill;    
    

    private void Start()
    {
        Joystick.Instance.DisableControl();
        //cameraFollow.EnableCamera(CameraVirtualType.Main);
        MoveCamera();
    }

    private void MoveCamera()
    {
        cameraFollow.Run(() =>
        {
            gameUIController.ShowView(TypeViewUI.E_TapView);
        });
    }

    private void Update()
    {
        Debug.Log(Joystick.Instance.FormatInput);
    }
}
