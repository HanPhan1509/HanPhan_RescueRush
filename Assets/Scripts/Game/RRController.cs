using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class RRController : MonoBehaviour
{
    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform player;
    private bool isPlay = false;

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
            isPlay = true;
            Debug.Log("Playyyyyyyyy");
        });
    }

    private void Update()
    {
        Debug.Log(Joystick.Instance.FormatInput);
    }
}
