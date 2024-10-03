using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class RRController : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        GameUIController.Instance.Init();
        GameUIController.Instance.ShowView(TypeViewUI.None);
        MoveCamera();
    }

    private void MoveCamera()
    {
        cameraFollow.Run(() =>
        {
            GameUIController.Instance.ShowView(TypeViewUI.E_TapView);
        });
    }

    private void Update()
    {
        playerController.Moving(Joystick.Instance.FormatInput);
    }
}
